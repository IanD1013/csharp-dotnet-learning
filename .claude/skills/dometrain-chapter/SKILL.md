---
name: dometrain-chapter
description: >-
  Compile one chapter - or an entire course - of a Dometrain video course into
  Simplified Chinese markdown study notes, translated faithfully from the dometrain MCP
  lesson documents. The notes are a translated transcription of the course rather
  than a summary of it, so reach for this skill even when the topic is one you already
  know well.
when_to_use: >-
  Use whenever the user names a Dometrain course and asks for notes, a write-up, or to
  add content to the repo - phrasings like "make notes for chapter 5 of Mastering C#",
  "write up the Mastering Structs module", "add the EF Core querying chapter to the
  repo", "do the whole Deep Dive into LINQ course", or just "next chapter". Also use it
  when the user asks to redo, extend, or fix notes this skill produced earlier.
argument-hint: "[course] [chapter (omit for the whole course)] [notes-folder]"
allowed-tools:
  - mcp__dometrain__list_courses
  - mcp__dometrain__get_course
  - mcp__dometrain__get_lesson
  - mcp__dometrain__search_code
  - mcp__dometrain__search_dometrain
  - mcp__dometrain__get_usage
---

# Dometrain chapter to notes

Ian studies .NET through Dometrain courses and keeps the results in this repo as a personal reference he rereads months later.
What he wants in these files is the course itself: offline, searchable, in Simplified Chinese, and linked back to the video.
Not a write-up of the course.

So this skill is a translator-compiler, not an author.
The lesson documents `get_lesson` returns are the content.
Fetch all of them, translate them into Simplified Chinese, assemble them into one file per chapter, and stay out of the way.

## Required input: the notes folder

The user must tell you which folder the generated notes go in.
If the request does not name a folder, **stop and ask for it before fetching anything** - before `get_course`, before any lesson call.
Do not guess a folder from the repo layout or from where earlier notes landed.

Each chapter's file is `<notes-folder>/<NN>-<chapter-slug>.md`, where `NN` is the chapter's position in the course tree, zero-padded.

## The one rule

**Every sentence in the notes is a Simplified Chinese translation of a sentence from the MCP tools.**

Not "is faithful to the lessons" in spirit.
Sentence for sentence: each sentence in the file corresponds to exactly one sentence in a lesson document, in the same order, saying the same thing.
If a sentence has no source in a lesson document, it does not go in the file.

You almost certainly know this topic and could explain parts of it better than the instructor did.
That is not what the file is for.
When Ian reads a line in these notes he needs to know it is what *this instructor said in this lesson*, so the deep link beside it takes him to the exact moment.
A better sentence the instructor never said breaks that guarantee, and so does a true one.

This holds even when a lesson is wrong, out of date, or contradicted by something you measured.
Translate it as the lesson has it.
If a correction is genuinely worth having, say it to Ian in chat and let him decide; do not put it in the file.

### Translation rules

- Translate prose into natural Simplified Chinese, one sentence per source sentence: no merging, no splitting, no summarising, no embellishing.
- Code blocks, inline code, identifiers, type and API names, commands, file paths, and package names stay exactly as returned, untranslated.
- Course titles, chapter titles, and lesson titles stay in their original English.
  They identify the video, and keeping them keeps the heading anchors stable.
- The standard lesson-document section names translate consistently: `Summary` becomes `总结`, `Key concepts` becomes `核心概念`, `Lesson notes` becomes `课程笔记`.
  Any other section name is translated faithfully.
- A widely used technical term with no settled Chinese rendering may keep the English term, or carry it in parentheses after the Chinese on first use.

### What you may change

Mechanical transformations only, the kind that would survive a bilingual diff review as "no content changed":

- Heading levels, so lesson headings nest under the document.
- The `<!-- ... -->` provenance comments and the trailing "Open this lesson on Dometrain" line that `get_lesson` wraps its output in.
  Drop them; the section's own deep link replaces both.
- Literal `\n` escape sequences.
  Some lesson documents come back with escaped newlines rather than real ones.
  Unescape them, including inside code blocks.
- Line breaks in prose, to put each full sentence on its own line.
- `[Watch in the lesson](url?t=NN)` rewritten as `[▶ 观看](url?t=NN)`.
  The URL and the timestamp are untouched.

### Repairing obvious MCP mistakes

Occasionally the MCP output is visibly mis-assembled: a document whose content clearly belongs to a different lesson in the chapter, or sections returned in an order that breaks the document's own logic mid-flow.
When the mistake is easy to spot and the correct arrangement is obvious, fix it: move the content under the lesson it actually belongs to, or restore the order the logic demands.

This is repair, not editing.
The sentences themselves stay a faithful translation of what the MCP returned; only their placement moves.
It is never a license to summarise, restructure, regroup by concept, or improve the material.
When you make such a repair, tell Ian in chat what you moved and why.

### What you may not change

- Meaning.
  The translation carries exactly what the source sentence says: no rephrasing beyond what translation requires, no tightening, no glossing a term, no fixing factual errors.
- Order, except for the repairs described above.
  Lessons appear in course order, and each lesson's own sections stay in the order the document has them.
- Grouping.
  Do not merge two lessons into one section or split one lesson across two.
- Code.
  Paste each block exactly as returned, including `using` lines, `namespace` declarations, surrounding scaffolding, and comments.
  Where a lesson shows the same code at several stages, keep every stage; the intermediate versions are the lesson.
- Emphasis.
  Do not bold, italicise, or pull a sentence into a blockquote for effect.

### What must not be added

No mental-model opener, no comparison tables you invented, no "common misconceptions", no self-test, no measured results, no asides, no cross-references to other chapters, no translator's notes, no commentary of any kind.

A lesson document's own `## Summary` and `## Key concepts` sections are course content and stay, translated.
Those are the only summaries in the file.

## Workflow: a single chapter

### 1. Resolve the course and chapter

`list_courses` with a topic filter, then `get_course` with the slug or id.
The course tree gives you the chapter list, each chapter's lessons, `lesson_id`, duration, deep link, and a `has_document` flag.

Chapter names the user gives you are usually approximate.
Match loosely, and when the user says "chapter 3" prefer counting chapters in the returned tree over guessing from the title.
If two chapters could plausibly match, ask before spending a dozen lesson fetches on the wrong one.

### 2. Pull every lesson in the chapter

`get_lesson` for each `lesson_id`, several calls in parallel.

Do not sample and do not stop early.
The file is the chapter, so a missing lesson is a missing section.
Quota is generous (thousands of calls a month, `get_usage` to check), so completeness costs nothing that matters.

Working through a long chapter a few lessons at a time is fine, appending sections as you go.
Just say which lessons are in the file so far and which are still to come.

### 3. Fill code gaps with `search_code`

`search_code` returns the code shown on screen, transcribed from the video, with its own deep link.
Two uses:

- A lesson where `has_document` is false.
  Search for identifiers the chapter works with and see whether the on-screen code is recoverable.
  Whatever comes back goes in that lesson's section under the note that the lesson has no document.
- A lesson document that refers to code it does not include ("as shown earlier", a type used but never declared).
  Search for the identifier and add the block it returns.

Results carry their own lesson attribution.
When a block comes from a different lesson than the section it lands in, link it to the lesson it actually came from.

Do not use `search_code` to go looking for better examples than the chapter gave.

### 4. Write the file

Path: `<notes-folder>/<NN>-<chapter-slug>.md`, using the folder the user provided.

`references/note-template.md` has the exact layout: header block, lesson index table, then one section per lesson in course order.
It is a short document because the shape is fixed.

The lesson index is the one table you assemble yourself, and it comes from the course tree rather than from your reading: lesson number, title, deep link, duration, and a link to the section below.
It stays because it is a table of contents, and because it is what proves no lesson was skipped.

### 5. Demo projects, only when asked

Do not create a project.
Do not run anything.
Do not put measured output in the notes.

When Ian explicitly asks for a demo alongside the notes, read `references/project-conventions.md` first, then build it, run it, and paste the real output into the demo section it asks for.
Absent that request, the chapter's notes are the whole deliverable.

## Workflow: an entire course

When the user names a course without naming a chapter, they want every chapter.
Confirm the notes folder first, like always.

### 1. Resolve the course once

`get_course` yourself, so the chapter list and every chapter's lesson subtree are in hand before any agent starts.

### 2. Skip the bookend chapters

Chapters whose title marks them as the course-level overview or the course-level conclusion (titles like "Course Overview", "Welcome", "Conclusion", "Wrapping Up") get no notes file.
They exist to open and close the video course, not to teach.
Skip them silently in whole-course mode; only produce them if the user explicitly names one.

`NN` numbering still follows each chapter's position in the full course tree, so skipping a bookend leaves a gap in the file numbers.
That gap is correct; do not renumber.

### 3. One subagent per chapter

Spawn one subagent per remaining chapter with the Agent tool, launching them in parallel in a single message so they run concurrently.
Each chapter is independent - its lessons, its file - so this is a clean split that keeps any one context from holding a whole course.

Each subagent's prompt must contain everything it needs, because it starts with no context:

- An instruction to first read this skill's `SKILL.md` and `references/note-template.md` (give absolute paths) and follow them exactly.
- The course title, slug, and course URL.
- The chapter's number, title, and slug.
- The chapter's full lesson subtree from `get_course`: every lesson's number, title, `lesson_id`, duration, deep link, and `has_document` flag, so the agent does not refetch the course.
- The exact output file path.
- A note that the deliverable is the notes file alone: no demo projects, no commits.

### 4. Verify the assembly

When the agents return, check the result yourself:

- One file exists for every non-bookend chapter, at the expected path.
- Each file's lesson index matches the chapter's subtree in the course tree: same lessons, same order, none missing.
- Spot-check one lesson section per file against a fresh `get_lesson` call: faithful translation, nothing added or dropped.

Fix anything an agent got wrong or left unfinished yourself, refetching lessons as needed.
Then report per chapter: written, skipped as bookend, or repaired.

## Quality bar

Before you call it done:

- Every lesson in the chapter has a section, in course order, and every section carries the lesson's deep link.
- Every lesson with a document was fetched with `get_lesson`.
  None were summarised from the course tree or from memory.
- Spot-check three paragraphs against the tool output.
  Sentence for sentence, each Chinese sentence must be a faithful translation of its source sentence, with nothing added, dropped, or reordered.
- Code blocks are complete, untranslated, in their original order, with `using` lines and namespaces intact.
- Lessons with no document are marked as such, with whatever `search_code` recovered.
- No section exists that does not correspond to a lesson.
- Any mismatch repair you made was reported to the user in chat.
- You told the user anything you could not retrieve.
