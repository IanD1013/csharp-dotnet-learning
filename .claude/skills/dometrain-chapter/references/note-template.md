# Note document structure

The layout is fixed: a header, a lesson index, then one section per lesson in course order.
Nothing else.
All prose is Simplified Chinese, translated from the lesson documents; titles and code stay in their original English.

This is short on purpose.
There are no judgement calls about structure here, because the structure is the chapter's.

> The existing notes in `src/mastering-csharp/notes/` were written under earlier versions of this skill: some reorganized lessons by concept and added authored sections, and all of them are in English.
> Do not use them as a model.
> If one of them is being redone, it gets rebuilt to the layout below, in Simplified Chinese.

---

## Header

```markdown
# <Chapter Title>

> 课程:[<Course Title>](<course url>) · 第 <N> 章
> 共 <M> 课 · 约 <duration>
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。
```

Chapter title, course title, chapter number, lesson count and duration all come from the `get_course` tree.
The chapter title and course title stay in English.

## Lesson index

Every lesson in the chapter, in course order.

```markdown
| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Section Overview](<deep link>) | 1:04 | [↓](#1-section-overview) |
| 2 | [Overview](<deep link>) | 0:27 | [↓](#2-overview) |
| 3 | [Argument Validation and Nullable Reference Types](<deep link>) | 1:38 | [↓](#3-argument-validation-and-nullable-reference-types) |
```

This is the only table you build yourself, and it is built from the course tree rather than from reading the lessons.
It is a table of contents and a completeness check: every lesson the tree lists appears here, including any with no document.

## Lesson sections

One `##` per lesson, numbered by its position in the chapter, titled exactly as the course titles it, in English.
Inside, the lesson document's own sections drop one level to `###`, with their names translated.

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

> [观看本课](https://dometrain.com/take/course/...) · 2:21

### 总结

本课演示如何手动实现一个 ThrowIfNull 守卫方法,……

### 核心概念

- [CallerArgumentExpression]:自动把传给参数的表达式捕获为字符串字面量。
- ……

### 课程笔记

空值检查守卫方法的一个基础实现是:检查参数是否为 null,若是则抛出 ArgumentNullException。
但在这种朴素实现中,除非调用方显式传入被检查变量的名称,否则异常的 ParamName 属性会保持为 null。

```csharp
...
```

[▶ 观看](https://dometrain.com/take/course/...?t=10)
````

What changed: the two provenance comments and the trailing "Open this lesson" line came off, `#` became `##` with the lesson number prefixed, the deep link and duration moved into a blockquote under the heading, `##` became `###` with the section names translated, the prose was translated sentence for sentence into Simplified Chinese, the two-sentence paragraph was split one sentence per line, and `[Watch in the lesson]` became `[▶ 观看]`.

What did not change: the meaning or order of a single sentence, the code block, or the `?t=` value.

Where a lesson document has sections other than these three, keep them, at `###`, in their original order, with their names translated faithfully.

## Lessons with no document

The section still exists, so the file matches the chapter:

```markdown
## 4. Configuring the Pipeline

> [观看本课](<deep link>) · 3:12

本课没有课程文档,请直接观看视频。
```

If `search_code` recovered on-screen code for it, put the blocks underneath with their own deep links, introduced by nothing more than:

```markdown
以下是通过 `search_code` 恢复的屏幕代码:
```

## Demo, only if one was requested

If Ian asked for a demo project, it gets one section at the end of the file, and that section is the only place in the document with content that is not from the course:

````markdown
---

## 运行 Demo

```bash
cd src/<course-slug>/<NN>-<chapter-slug>/<Project>
dotnet run -c Release
```
````

Real output, pasted as produced, goes here and nowhere else in the file.
Note that benchmarks need Release and take minutes.

Absent that request, the file ends with its last lesson section.

---

## Mechanics

- Simplified Chinese prose, translated sentence for sentence from the English lesson documents.
  Code, identifiers, API names, commands, file paths, and all course, chapter, and lesson titles stay in English.
- One full sentence per line, so a revised chapter produces a readable diff.
  This is a line-break change only; it never merges, splits, or reorders sentences.
- Chinese prose uses Chinese punctuation (,。:;""), as normal translated text would.
- Hyphens rather than em dashes in anything you write yourself.
  Em dashes inside quoted course text (code comments, titles) are course content: leave them alone.
- Anchors in the lesson index must match the generated heading ids, including the number prefix.
  Because lesson headings keep their English titles, the anchors are the same as they would be for English notes.
