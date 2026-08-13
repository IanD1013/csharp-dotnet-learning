# Note document structure

The shape below is a starting point, not a form to fill in. Chapters differ, and a
chapter that resists this structure is telling you something - follow the chapter.
What should survive every variation: the reader can get from any claim back to the
video, and the document is organized for the fifth reading rather than the first.

Examples throughout are drawn from `src/mastering-csharp/notes/03-value-types-vs-reference-types.md`,
which is the reference implementation of this template.

---

## Header

```markdown
# <Chapter Title>

> Course: [<Course Title>](<course url>) · Chapter <N>
> <M> lessons · ~<duration>
> Source: Dometrain. Every heading links back to the lesson it came from.
```

## The mental model, up front

Before any detail, state the one idea the chapter exists to install, and where
possible the misconception it is trying to displace. Chapters usually say this
themselves in their overview or summary lesson - quote their framing rather than
inventing your own.

A small comparison table earns its place here when the chapter is built around a
contrast:

```markdown
| | value type (struct) | reference type (class) |
| --- | --- | --- |
| Storage | inline | indirection |
| Copy | copies the data | copies the reference |
| Equality | value based | referential |
```

This section is what Ian rereads when he has two minutes, so it should stand alone.

## Lesson index

Every lesson in the chapter, in course order, with a link into the video and a link
into the section of this document that covers it. This table is what makes the notes
auditable: it proves nothing was skipped, and it is the fastest route back to source.

```markdown
| # | Lesson | Length | Covered in |
| --- | --- | --- | --- |
| 3 | [Storage Model: Inline vs. Indirection](<deep link>) | 2:49 | [1.1](#11-inline-vs-indirection) |
```

Mark lessons with no document explicitly, for example `no document - watch directly`,
so a gap reads as a known gap rather than an oversight.

## Body, organized by concept

Group the lessons into the two to four ideas the chapter actually teaches. Several
lessons often collapse into one section, and one dense lesson sometimes deserves two.

Each section opens with a blockquote linking the lessons behind it:

```markdown
### 1.5 Access performance: the cost of indirection

> [Exploring Access Patterns with Benchmarks](<link>) · [Analyzing Access Patterns Benchmark Results](<link>)
```

Keep the course's code verbatim inside fenced blocks. Verbatim matters because Ian
will pause the video with the notes open beside it, and silently modernized code
breaks that comparison. Put a timestamped link under each block when the lesson
provided one:

```markdown
[▶ Watch](<deep link>?t=115)
```

Preserve the instructor's teaching devices. When a chapter deliberately shows a
misleading result and then corrects it, keep both halves and keep them in order - the
correction is worthless without the setup, and that arc is usually the most valuable
thing in the chapter.

## Measured results

Where the chapter measures something and a demo project reproduces it, show the real
numbers from this machine next to the ones quoted on screen. Note divergences plainly
and without alarm; runtimes change between recording and rewatching.

## Common misconception

Most chapters spend time dismantling something the audience already half-believes.
Give it its own section near the end. It is the highest-value part of the document
for a rereader, who is likely to have drifted back toward the wrong model.

## Self-test

Six to eight questions that force recall rather than recognition, followed by answers
in a collapsed block so the page can be read as a quiz:

```markdown
<details>
<summary>Answer key</summary>

1. ...

</details>
```

Good questions ask why something behaves as it does, or ask for a prediction. Questions
answerable by skimming the page above teach nothing.

## Running the demo

When a project exists, the exact commands, with the working directory:

```markdown
cd src/<course-slug>/<NN>-<chapter-slug>/<Project>
dotnet run -c Release
```

Call out anything non-obvious, especially that benchmarks need Release and take time.

## Threads into later chapters

Chapters routinely defer topics. A short table of what was deferred and where it lands
turns a set of chapter notes into a course-wide index as more chapters get written.

```markdown
| Deferred here | Picked up in |
| --- | --- |
| Defensive copies with mutable structs | Mastering Structs |
```

---

## Style

- English prose, technical terms unchanged.
- One full sentence per line, so revisions produce readable diffs.
- Hyphens rather than em dashes, matching the repo's writing conventions.
- Explain why something is true, not only that it is. The reason is what survives.
- Trust the reader; he is a working .NET developer, not a beginner.
