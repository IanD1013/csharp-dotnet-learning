---
name: dometrain-chapter
description: >-
  Compile one chapter of a Dometrain video course into a markdown study note under
  src/, assembled verbatim from the dometrain MCP lesson documents. Use this whenever
  the user names a Dometrain course and a chapter or module and asks for notes, a
  write-up, or to add a chapter to the repo - phrasings like "make notes for chapter 5
  of Mastering C#", "write up the Mastering Structs module", "add the EF Core querying
  chapter to the repo", or just "next chapter". Also use it when the user asks to redo,
  extend, or fix notes this skill produced earlier. The notes are a transcription of
  the course rather than a summary of it, so reach for this skill even when the topic
  is one you already know well.
---

# Dometrain chapter to notes

Ian studies .NET through Dometrain courses and keeps the results in this repo as a
personal reference he rereads months later. What he wants in these files is the course
itself: offline, searchable, and linked back to the video. Not a write-up of the course.

So this skill is a compiler, not an author. The lesson documents `get_lesson` returns
are the content. Fetch all of them, assemble them into one file per chapter, and stay
out of the way.

## The one rule

**Every sentence in the notes comes from the MCP tools.**

Not "is faithful to the lessons". Not "is grounded in the lessons". Comes from them. If
a sentence is not in a lesson document, it does not go in the file.

You almost certainly know this topic and could explain parts of it better than the
instructor did. That is not what the file is for. When Ian reads a line in these notes
he needs to know it is what *this instructor said in this lesson*, so the deep link
beside it takes him to the exact moment. A better sentence the instructor never said
breaks that guarantee, and so does a true one.

This holds even when a lesson is wrong, out of date, or contradicted by something you
measured. Leave it as the lesson has it. If a correction is genuinely worth having, say
it to Ian in chat and let him decide; do not put it in the file.

### What you may change

Mechanical transformations only, the kind that would survive a diff review as "no
content changed":

- Heading levels, so lesson headings nest under the document.
- The `<!-- ... -->` provenance comments and the trailing "Open this lesson on Dometrain" line that `get_lesson` wraps its output in. Drop them; the section's own deep link replaces both.
- Literal `\n` escape sequences. Some lesson documents come back with escaped newlines rather than real ones. Unescape them, including inside code blocks.
- Line breaks in prose, to put each full sentence on its own line.
- `[Watch in the lesson](url?t=NN)` rewritten as `[▶ Watch](url?t=NN)`. The URL and the timestamp are untouched.

### What you may not change

- Wording. No rephrasing, no tightening, no glossing a term, no fixing grammar.
- Order. Lessons appear in course order, and each lesson's own sections stay in the order the document has them.
- Grouping. Do not merge two lessons into one section or split one lesson across two.
- Code. Paste each block exactly as returned, including `using` lines, `namespace` declarations, surrounding scaffolding, and comments. Where a lesson shows the same code at several stages, keep every stage; the intermediate versions are the lesson.
- Emphasis. Do not bold, italicise, or pull a sentence into a blockquote for effect.

### What must not be added

No mental-model opener, no comparison tables you invented, no "common misconceptions",
no self-test, no measured results, no asides, no cross-references to other chapters, no
commentary of any kind.

A lesson document's own `## Summary` and `## Key concepts` sections are course content
and stay. Those are the only summaries in the file.

## Workflow

### 1. Resolve the course and chapter

`list_courses` with a topic filter, then `get_course` with the slug or id. The course
tree gives you the chapter list, each chapter's lessons, `lesson_id`, duration, deep
link, and a `has_document` flag.

Chapter names the user gives you are usually approximate. Match loosely, and when the
user says "chapter 3" prefer counting chapters in the returned tree over guessing from
the title. If two chapters could plausibly match, ask before spending a dozen lesson
fetches on the wrong one.

### 2. Pull every lesson in the chapter

`get_lesson` for each `lesson_id`, several calls in parallel.

Do not sample and do not stop early. The file is the chapter, so a missing lesson is a
missing section. Quota is generous (thousands of calls a month, `get_usage` to check),
so completeness costs nothing that matters.

Working through a long chapter a few lessons at a time is fine, appending sections as
you go. Just say which lessons are in the file so far and which are still to come.

### 3. Fill code gaps with `search_code`

`search_code` returns the code shown on screen, transcribed from the video, with its own
deep link. Two uses:

- A lesson where `has_document` is false. Search for identifiers the chapter works with and see whether the on-screen code is recoverable. Whatever comes back goes in that lesson's section under the note that the lesson has no document.
- A lesson document that refers to code it does not include ("as shown earlier", a type used but never declared). Search for the identifier and add the block it returns.

Results carry their own lesson attribution. When a block comes from a different lesson
than the section it lands in, link it to the lesson it actually came from.

Do not use `search_code` to go looking for better examples than the chapter gave.

### 4. Write the file

Path: `src/<course-slug>/notes/<NN>-<chapter-slug>.md`, where `NN` is the chapter's
position in the course, zero-padded.

`references/note-template.md` has the exact layout: header block, lesson index table,
then one section per lesson in course order. It is a short document because the shape is
fixed.

The lesson index is the one table you assemble yourself, and it comes from the course
tree rather than from your reading: lesson number, title, deep link, duration, and a
link to the section below. It stays because it is a table of contents, and because it is
what proves no lesson was skipped.

### 5. Demo projects, only when asked

Do not create a project. Do not run anything. Do not put measured output in the notes.

When Ian explicitly asks for a demo alongside the notes, read
`references/project-conventions.md` first, then build it, run it, and paste the real
output into the demo section it asks for. Absent that request, the chapter's notes are
the whole deliverable.

## Quality bar

Before you call it done:

- Every lesson in the chapter has a section, in course order, and every section carries the lesson's deep link.
- Every lesson with a document was fetched with `get_lesson`. None were summarised from the course tree or from memory.
- Spot-check three paragraphs against the tool output. They should match word for word.
- Code blocks are complete, in their original order, with `using` lines and namespaces intact.
- Lessons with no document are marked as such, with whatever `search_code` recovered.
- No section exists that does not correspond to a lesson.
- You told the user anything you could not retrieve.
