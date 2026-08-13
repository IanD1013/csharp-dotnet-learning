---
name: dometrain-chapter
description: >-
  Turn one chapter of a Dometrain video course into English markdown study notes
  under src/, plus a runnable demo project when the chapter contains real code.
  Use this whenever the user names a Dometrain course and a chapter or module and
  asks for notes, a summary, a write-up, a demo, or a practice project - phrasings
  like "make notes for chapter 5 of Mastering C#", "write up the Mastering Structs
  module", "add the EF Core querying chapter to the repo", or just "next chapter".
  Also use it when the user asks to redo, extend, or fix notes this skill produced
  earlier. The notes must be grounded in the dometrain MCP tools rather than in
  general knowledge, so reach for this skill even when the topic is one you already
  know well.
---

# Dometrain chapter to notes and demo

Ian studies .NET through Dometrain courses and keeps the results in this repo as a
personal reference he rereads months later. Two things make these notes worth more
than a generic article on the same topic: every claim traces back to a specific
lesson he can rewatch, and the code actually runs on his machine.

Protect both of those properties and the rest is ordinary technical writing.

## Ground the notes in the course, not in your own knowledge

You almost certainly know the topic already. Write from the lesson documents anyway.

The value here is fidelity: when Ian reads "structs use value-based equality by
default" he needs to know that is what *this instructor said in this lesson*, so a
deep link takes him to the exact moment. Notes assembled from your own understanding
break that guarantee even when every sentence is true, and they quietly drop the
instructor's framing, worked examples, and deliberate teaching traps.

Where your own knowledge does earn its place: flagging when the course simplifies
something, or when a measured result on this machine differs from the number quoted
on screen. Add those as clearly marked asides, never as silent corrections.

## Workflow

### 1. Resolve the course and chapter

`list_courses` with a topic filter, then `get_course` with the slug or id. The course
tree gives you the chapter list, each chapter's lessons, `lesson_id`, duration, deep
link, and a `has_document` flag.

Chapter names the user gives you are usually approximate. Match loosely, and when the
user says "chapter 3" prefer counting chapters in the returned tree over guessing from
the title. If two chapters could plausibly match, ask before spending a dozen lesson
fetches on the wrong one.

Note the lessons where `has_document` is false. Those have no text to pull, so say so
in the notes rather than leaving a silent hole.

### 2. Pull every lesson in the chapter

`get_lesson` for each `lesson_id`, several calls in parallel. Read all of them before
writing anything.

Do not sample. A 12-lesson chapter often hides its best material in one 3-minute
lesson that walks back a claim made earlier, and skipping it produces notes that are
confidently wrong. Quota is generous (thousands of calls a month, `get_usage` to
check), so completeness costs nothing that matters.

`search_code` is for later: use it when you want a second example of an API the chapter
mentions only in passing. It is not a substitute for reading the lessons.

### 3. Write the notes

Path: `src/<course-slug>/notes/<NN>-<chapter-slug>.md`, where `NN` is the chapter's
position in the course, zero-padded.

Read `references/note-template.md` for the full structure and a worked example of each
section. The short version: open with the mental model the chapter is trying to
install, give a lesson index table so any section can be traced back to video, organize
the body by concept rather than by lesson order, and close with self-test questions
whose answers are folded away.

Organizing by concept is the part people get wrong. A lesson-by-lesson transcript is
easy to produce and nearly useless to reread, because the instructor's pacing is built
for a first viewing, not for the person who six months later needs to remember how `in`
differs from `ref readonly`. Chapters usually announce their own better structure -
often in the summary lesson - so look there before inventing one.

Write in English. Keep every full sentence on its own line: it keeps diffs readable
when a chapter gets revised.

### 4. Decide whether a demo project is warranted

Build one when the chapter shows code Ian could step through, benchmark, or break on
purpose. Skip it when the chapter is conceptual, when the code is three lines that the
notes already show in full, or when it needs infrastructure the repo does not have.

Say which way you went and why. "No demo: the chapter is a decision framework with no
runnable code" is a fine outcome and better than a project that prints three lines.

When the chapter demonstrates something *measurable* - allocations, layout, timing -
building it is close to mandatory, because the measured number on Ian's machine is
worth more than the number he watched someone else get.

### 5. Build and verify

Read `references/project-conventions.md` before creating any project. It covers the
`src/` layout, central package management, the analyzer settings that are on, and the
naming pattern.

Then actually run it. A demo project that has never been executed is a liability: it
looks authoritative and may not even compile. Build clean with no warnings, run every
entry point, and paste the real output into the notes.

If a package the course used is abandoned or broken on current .NET, find out by trying
it before you design around it, and record what you learned in the notes.

### 6. Close the loop between notes and code

The notes should tell Ian how to run the project, and the project's comments should
point back at what the notes explain. Include the exact commands with `-c Release`
where it matters.

Where you have measured output, put it in the notes next to the course's claimed
figures. Small divergences are normal and interesting - the runtime changes between
recordings - and seeing both numbers teaches more than either alone.

## Quality bar

Before you call it done:

- Every section traces to a lesson deep link, and code blocks carry `?t=` timestamps where the lesson provided them.
- The lesson index table lists every lesson in the chapter, including any with no document.
- The notes' claims match the lesson documents, with your own additions clearly marked as asides.
- Any project builds with zero warnings and has been run, with real output pasted into the notes.
- New projects are in `DotNetLearning.slnx`.
- You told the user what you skipped and why.
