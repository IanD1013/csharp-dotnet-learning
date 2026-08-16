# Note document structure

The layout is fixed: a header, a lesson index, then one section per lesson in course
order. Nothing else.

This is short on purpose. There are no judgement calls about structure here, because the
structure is the chapter's.

> The existing notes in `src/mastering-csharp/notes/` were written under an earlier
> version of this skill that reorganized lessons by concept and added authored sections.
> Do not use them as a model. If one of them is being redone, it gets rebuilt to the
> layout below.

---

## Header

```markdown
# <Chapter Title>

> Course: [<Course Title>](<course url>) · Chapter <N>
> <M> lessons · ~<duration>
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.
```

Chapter title, course title, chapter number, lesson count and duration all come from the
`get_course` tree.

## Lesson index

Every lesson in the chapter, in course order.

```markdown
| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Section Overview](<deep link>) | 1:04 | [↓](#1-section-overview) |
| 2 | [Overview](<deep link>) | 0:27 | [↓](#2-overview) |
| 3 | [Argument Validation and Nullable Reference Types](<deep link>) | 1:38 | [↓](#3-argument-validation-and-nullable-reference-types) |
```

This is the only table you build yourself, and it is built from the course tree rather
than from reading the lessons. It is a table of contents and a completeness check: every
lesson the tree lists appears here, including any with no document.

## Lesson sections

One `##` per lesson, numbered by its position in the chapter, titled exactly as the
course titles it. Inside, the lesson document's own sections drop one level to `###`.

Given this from `get_lesson`:

````
<!-- Mastering: C# — Recreating ThrowIfNull Manually -->
<!-- Lesson: https://dometrain.com/take/course/... -->

# Recreating ThrowIfNull Manually

## Summary
This lesson demonstrates how to manually implement a ThrowIfNull guard method, ...

## Key concepts
- [CallerArgumentExpression]: Automatically captures the expression passed to a parameter as a string literal.
- ...

## Lesson notes
A basic implementation of a null-check guard method involves checking if an argument is null and throwing an ArgumentNullException. However, in a naive implementation, the ParamName property of the exception remains null unless the caller explicitly passes the name of the variable being checked.

```csharp
...
```
[Watch in the lesson](https://dometrain.com/take/course/...?t=10)

[Open this lesson on Dometrain](https://dometrain.com/take/course/...)
````

the section is:

````markdown
## 6. Recreating ThrowIfNull Manually

> [Watch the lesson](https://dometrain.com/take/course/...) · 2:21

### Summary

This lesson demonstrates how to manually implement a ThrowIfNull guard method, ...

### Key concepts

- [CallerArgumentExpression]: Automatically captures the expression passed to a parameter as a string literal.
- ...

### Lesson notes

A basic implementation of a null-check guard method involves checking if an argument is null and throwing an ArgumentNullException.
However, in a naive implementation, the ParamName property of the exception remains null unless the caller explicitly passes the name of the variable being checked.

```csharp
...
```

[▶ Watch](https://dometrain.com/take/course/...?t=10)
````

What changed: the two provenance comments and the trailing "Open this lesson" line came
off, `#` became `##` with the lesson number prefixed, the deep link and duration moved
into a blockquote under the heading, `##` became `###`, the two-sentence paragraph was
split one sentence per line, and `[Watch in the lesson]` became `[▶ Watch]`.

What did not change: a single word of prose, the code block, or the `?t=` value.

Where a lesson document has sections other than these three, keep them, at `###`, in
their original order and under their original names.

## Lessons with no document

The section still exists, so the file matches the chapter:

```markdown
## 4. Configuring the Pipeline

> [Watch the lesson](<deep link>) · 3:12

No lesson document is available for this lesson - watch it directly.
```

If `search_code` recovered on-screen code for it, put the blocks underneath with their
own deep links, introduced by nothing more than:

```markdown
Code shown on screen, from `search_code`:
```

## Demo, only if one was requested

If Ian asked for a demo project, it gets one section at the end of the file, and that
section is the only place in the document with content that is not from the course:

````markdown
---

## Running the demo

```bash
cd src/<course-slug>/<NN>-<chapter-slug>/<Project>
dotnet run -c Release
```
````

Real output, pasted as produced, goes here and nowhere else in the file. Note that
benchmarks need Release and take minutes.

Absent that request, the file ends with its last lesson section.

---

## Mechanics

- English. The lesson documents are English and they are being copied, not translated.
- One full sentence per line, so a revised chapter produces a readable diff. This is a line-break change only; it never merges, splits, or reorders sentences.
- Hyphens rather than em dashes. The lessons sometimes use em dashes: those are inside quoted course text, so leave them alone.
- Anchors in the lesson index must match the generated heading ids, including the number prefix.
