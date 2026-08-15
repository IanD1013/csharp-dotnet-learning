# Note document structure

The shape below is a starting point, not a form to fill in. Chapters differ, and a
chapter that resists this structure is telling you something - follow the chapter.
What should survive every variation: the reader can get from any claim back to the
video, and the document is organized for the fifth reading rather than the first.

Examples throughout are drawn from `src/mastering-csharp/notes/03-value-types-vs-reference-types.md`,
which is the reference implementation of this template.

Follow it for **structure**: section shapes, the lesson index, how every claim is traced
back to a deep link. Do not copy its **voice**. The notes written before the Style
section below existed run considerably denser than that section now asks for, so where
the two disagree, Style wins.

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

### Who you are writing for

Someone with a solid general computer-science background who writes C#, but who is not
a runtime or compiler specialist. They know what a class, a heap, a hash table and a
stack frame are. They do not necessarily know what boxing costs, what "blittable"
means, why a JIT would elide an allocation, or what "nominal" and "structural" typing
are called.

That cuts both ways. Do not explain what a `for` loop is. Equally, do not drop a term
like "non-blittable", "escape analysis", "defensive copy" or "sum type" as though it
needs no introduction. Gloss it the first time it appears, in a clause or a short
sentence, then use it freely for the rest of the document.

### Write it the way you would explain it out loud

The most common failure of these notes is a register that is too compressed: every
sentence correct, dense, and quietly exhausting to read. Aim for a colleague
explaining the chapter at a whiteboard, not for a specification.

Concrete habits, in rough order of how much they help:

**Prefer a plain sentence to an aphorism.** Compressed one-liners feel authoritative
while writing and are much harder to decode on a reread.

> Closedness is the feature.

becomes

> The useful part is that the set of cases is closed. The compiler knows every case the
> union can hold, so it can tell you when a `switch` has missed one.

**Name the thing rather than gesturing at it.** Figures like "coming back to collect",
"load-bearing", "the pivot", "earns its place" read as clever on the first pass and as
vague on the fifth.

> This is [1.2](#12-the-names-are-not-real) coming back to collect.

becomes

> This is the name-erasure rule from [1.2](#12-the-names-are-not-real) showing its cost.

**Break a long sentence at its joint.** Two clauses joined by "which", "and exactly",
or a semicolon are usually two sentences.

> Structural identity is exactly what makes a tuple cheap, and exactly what makes it
> unable to carry meaning.

becomes

> A tuple is identified only by its shape. That is what makes it cheap to use, and it
> is also why it cannot carry any meaning of its own.

**Use concrete subjects and active verbs.** "The compiler boxes the `int`" beats
"boxing occurs". "This costs 24 bytes" beats "a 24-byte allocation is incurred".

**Introduce jargon with a gloss instead of assuming it.**

> The `int` case boxes.

becomes

> The `int` case gets boxed: the runtime wraps the value in a small heap object so it
> fits in a field typed `object`. That is 24 bytes per instance.

**Keep paragraphs to two or three sentences.** Let a blank line do the work a
subordinate clause was doing.

**Say the plain version first, then the precise one.** A section can open with the
one-sentence summary a reader could repeat from memory, and follow with the exact
mechanism. Leading with the precise version makes readers work before they know why
they are working.

### What must not change

Reader-friendly refers to the wording, never the substance. Do not soften a claim to
make it flow better, do not drop a caveat because it interrupts a paragraph, and do not
round a measured number. Where a passage is hard to read because the idea is genuinely
subtle, keep the idea and spend *more* words on it, not fewer.

Also unchanged by any of the above:

- Course code stays verbatim, comments and dated style included.
- Deep links, `?t=` timestamps, and the lesson index table stay exactly as specified above.
- Measured output is pasted as produced, never tidied or abbreviated.
- Asides that mark your own additions or corrections stay clearly marked as asides.

### Mechanics

- English prose. Technical terms and identifiers keep their original form.
- One full sentence per line, so revisions produce readable diffs.
- Hyphens rather than em dashes, matching the repo's writing conventions.
- Explain why something is true, not only that it is. The reason is what survives.
