# Mastering Structs

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 5
> 8 lessons · ~8.8 minutes
> Source: Dometrain. Every section links back to the lesson it came from.
> No companion project for this chapter, by request. See [No companion project](#no-companion-project).
> Picks up the defensive-copy thread deferred in [Value Types vs. Reference Types](03-value-types-vs-reference-types.md).

---

## The mental model

Chapter 3 established that assignment copies the value, and for a struct the value is the data.
This chapter is the consequence of that rule meeting real code, and it has exactly one failure mode:

> **You think you are mutating the instance. You are mutating a copy, and the copy is discarded.**

The chapter's own framing is that **immutable structs are easy and mutable structs are where the bodies are buried**.
Everything difficult here traces back to a struct that can change its own state.

Copies arrive from three directions, and they get progressively harder to see:

| Source of the copy | Visible in the source? | Example |
| --- | --- | --- |
| **Boxing** | yes, if you know a cast to an interface is a copy | `((IIncrementable)counter).Increment()` |
| **Return by value** | no, looks like member access | `list[0].Increment()` |
| **Compiler defensive copy** | no, nothing in the source at all | `_readonlyField.TryAdvance()` |

The third is the chapter's real subject, and it inverts the usual expectation: **`readonly` is what causes the bug**.
Removing `readonly` makes the code work.

The fix, for all of it, is also one word.
Mark the struct `readonly struct`, or mark the individual members `readonly`, and the compiler stops needing to defend itself.

---

## Lesson index

| # | Lesson | Length | Covered in |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958855/) | 1:06 | [The mental model](#the-mental-model) |
| 2 | [Boxing and Indexers](https://dometrain.com/take/course/mastering-csharp-3256129/boxing-and-indexers-69958856/) | 0:27 | [1.1](#11-boxing-a-cast-is-an-allocation) · [1.2](#12-indexers-why-list-and-array-disagree) |
| 3 | [Exploring Different Kind of Copies](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-different-kind-of-copies-69958857/) | 1:42 | [1.1](#11-boxing-a-cast-is-an-allocation) · [1.2](#12-indexers-why-list-and-array-disagree) |
| 4 | [The Readonly Field Trap](https://dometrain.com/take/course/mastering-csharp-3256129/the-readonly-field-trap-69958858/) | 0:24 | [2.1](#21-the-trap-a-reader-that-never-advances) |
| 5 | [Why the Compiler Makes a Copy?](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/) | 1:25 | [2.2](#22-why-mutable-this) |
| 6 | [How to Avoid Defensive Copies for Structs?](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/) | 1:54 | [2.4](#24-the-fix-readonly-as-a-promise-to-the-compiler) |
| 7 | [Defensive Copy Overview](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/) | 1:09 | [2.3](#23-the-four-readonly-contexts) |
| 8 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958862/) | 0:41 | [Common misconceptions](#common-misconceptions) |

Every lesson in this chapter has a document; nothing was skipped.

Note the course order is slightly out of sequence: lesson 7, "Defensive Copy Overview", is the systematic treatment and lands *after* the hands-on lesson 6 that uses it.
These notes put the overview first, as [2.3](#23-the-four-readonly-contexts) before [2.4](#24-the-fix-readonly-as-a-promise-to-the-compiler), which reads better on a reread.

---

## Part 1 · Copies you could have seen coming

### 1.1 Boxing: a cast is an allocation

> [Boxing and Indexers](https://dometrain.com/take/course/mastering-csharp-3256129/boxing-and-indexers-69958856/) · [Exploring Different Kind of Copies](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-different-kind-of-copies-69958857/)

A struct that implements an interface is still a value type.
Casting it to that interface **boxes** it: the runtime allocates a heap object and copies the struct's content into it.

```csharp
interface IIncrementable { void Increment(); }

struct Counter : IIncrementable
{
    public int Value { get; private set; }
    public void Increment() => Value++;
}

// Explicit boxing: mutation occurs on a boxed copy, not the original instance
var counter = new Counter();
((IIncrementable)counter).Increment();
((IIncrementable)counter).Increment();
Console.WriteLine(counter.Value); // Output: 0
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/boxing-and-indexers-69958856/?t=10)

Two increments, and the answer is `0`.
Worse than it first looks: each cast boxes **separately**, so the two calls did not even mutate the same copy.
Two heap objects were allocated, each incremented once, and both were discarded.

### 1.2 Indexers: why `List<T>` and array disagree

Same struct, two collections that look interchangeable, opposite results.

```csharp
// List indexer: returns a copy of the struct
List<Counter> list = [new()];
list[0].Increment();
Console.WriteLine(list[0].Value); // Output: 0

// Array indexer: provides direct access to the element memory
Counter[] array = [new()];
array[0].Increment();
Console.WriteLine(array[0].Value); // Output: 1
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/boxing-and-indexers-69958856/?t=10)

The distinction is what the indexer *is*:

- **`List<T>`**: the indexer is an ordinary **property** returning `T`. For a struct that means a copy. `list[0]` produces a temporary, `Increment()` mutates the temporary, and the temporary is discarded at the end of the statement.
- **Arrays**: array element access has special CLR support and yields a **reference to the element's storage**. `array[0].Increment()` mutates the data in the array itself.

The syntax is identical and the semantics are not, which is what makes this a genuine trap rather than a curiosity.

> **Aside, verified on this machine.**
> The language is not entirely silent here, but its warning arrives only for assignment, not for method calls.
> `list[0].Value = 5;` is a compile error, **CS1612: "Cannot modify the return value of `List<Counter>.this[int]` because it is not a variable"**.
> `list[0].Increment();` compiles with no diagnostic at all, even though it does the same thing less obviously.
> C# blocks the version you would notice and permits the version you would not.

#### Getting array-like behaviour back: `ref` returns

A custom collection can return a reference from its indexer, which restores in-place mutation.

```csharp
class RefList<T> : IEnumerable<T>
{
    private T[] _items;
    private int _count;

    public RefList(int capacity = 4) { _items = new T[capacity]; }

    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }
        _items[_count++] = item;
    }

    // Returning by reference allows in-place mutation of structs
    public ref T this[int index] => ref _items[index];

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
```

`ref T this[int index]` is the whole trick: the caller receives an alias to the slot rather than its contents.
This is also why the same problem does not exist for `Span<T>`, whose indexer is `ref`-returning by design.

The generalization the chapter draws: **.NET passes structs by value nearly everywhere**, including arguments, return values, properties, and most indexers.
Arrays and `ref` returns are the exceptions, not the rule.

---

## Part 2 · Copies the compiler inserts

### 2.1 The trap: a reader that never advances

> [The Readonly Field Trap](https://dometrain.com/take/course/mastering-csharp-3256129/the-readonly-field-trap-69958858/)

This is the best example in the chapter, because there is nothing in the source to point at.

```csharp
var parser = new Parser();
parser.Parse();

struct SequenceReader
{
    private int _position;

    public int Position => _position;

    public bool TryAdvance()
    {
        if (_position >= 5) return false;
        _position++;
        return true;
    }
}

class Parser
{
    private readonly SequenceReader _reader;

    public void Parse()
    {
        for (int i = 0; i < 3; i++)
        {
            bool advanced = _reader.TryAdvance();
            Console.WriteLine($" Advanced: {advanced}, Position: {_reader.Position}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-readonly-field-trap-69958858/?t=10)

`TryAdvance` returns `true` every time, and `Position` stays `0` every time.

The reason: `_reader` is a `readonly` field, so on each call the compiler copies `_reader` into a hidden local and calls `TryAdvance()` on the copy.
The copy's `_position` reaches 1, then the copy is thrown away.
The next iteration copies the untouched original again, so the loop is stuck at zero forever while cheerfully reporting success.

Note how bad the failure mode is.
There is no exception, no warning, and the return value actively lies.
Bump the loop to a `while (_reader.TryAdvance())` and it never terminates.

### 2.2 Why: mutable `this`

> [Why the Compiler Makes a Copy?](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/)

The compiler is not being paranoid without cause.
Think of an instance member as a static method whose first parameter is `this`, and for a struct that parameter is passed **by mutable reference**.

```csharp
// This is what a Distance property conceptually is
public static double get_Distance(ref Point @this)
{
    @this = new Point(1, 2);
    return 0;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/?t=45)

Any member of a non-`readonly` struct can legally reassign `this` wholesale, **including a property getter**.

```csharp
struct Point
{
    public int X { get; }
    public int Y;

    public Point(int x, int y) { X = x; Y = y; }

    public double Distance
    {
        get
        {
            // legal for a non-readonly struct member
            this = new Point(1, 2);
            return 0;
        }
    }
}

class Holder
{
    private readonly Point _point = new(3, 4);

    public void Show()
    {
        Console.WriteLine($"X: {_point.X}");
        Console.WriteLine($"Y: {_point.Y}");
        Console.WriteLine($"Distance: {_point.Distance}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/why-the-compiler-makes-a-copy-69958859/?t=45)

Reading `_point.Distance` would destroy a `readonly` field if the call went through directly.
The defensive copy is what keeps `_point` at `(3, 4)`, so the compiler is buying a real guarantee, not an imaginary one.

The other half of the explanation is that `readonly` means something **stronger** for a struct field than for a class field.

| Field type | What `readonly` protects |
| --- | --- |
| class | the **reference** only; the object it points at is freely mutable |
| struct | the **data itself**, so no member call may alter it |

That stronger promise is the entire source of the problem.
The compiler has to keep a promise it cannot verify any other way, and copying is the only tool available.

### 2.3 The four `readonly` contexts

> [Defensive Copy Overview](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/)

A defensive copy can appear in exactly four places:

1. `readonly` fields
2. `in` parameters
3. `ref readonly` parameters
4. `ref readonly` locals

The unifying idea is worth stating as a rule, because it also tells you when copies do *not* happen:

> The compiler emits a defensive copy **unless it can prove** the access cannot mutate.

What counts as proof:

```csharp
// Accessing X won't cause a copy
public int X { get; }
// Accessing Y won't cause a copy
public int Y;

// Accessing Distance won't cause a copy because it is marked readonly
public readonly double Distance
    => Math.Sqrt(X * X + Y * Y);

// Accessing any members of a readonly struct won't cause a copy
readonly struct Point
{
    public int X { get; }
    public int Y { get; }
    public double Distance => Math.Sqrt(X * X + Y * Y);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/?t=10)

Fields and auto-properties are safe because the compiler generated their access itself and knows it does nothing.
Anything hand-written needs the `readonly` keyword to say so.

`in` and `ref readonly` are the same mechanism underneath, which the lowered form makes obvious:

```csharp
private static void ByIn(in Point point)
{
    // A defensive copy is created because Distance is not readonly
    Console.WriteLine(point.Distance);
}

// The compiler effectively transforms the above into:
private static void ByIn_Decompiled([In][IsReadOnly] ref Point point)
{
    Point point2 = point; // Defensive copy
    Console.WriteLine(point2.Distance);
}

private static void ByRefReadonly(ref readonly Point point)
{
    // A defensive copy is created
    Console.WriteLine(point.Distance);
}

// Read-only local context
Point[] points = [new()];
ref readonly var first = ref points[0];
// A defensive copy is created
Console.WriteLine(first.Distance);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/defensive-copy-overview-69958861/?t=40)

`in` is literally `ref` plus `[In][IsReadOnly]` metadata.
`Point point2 = point;` is the injected copy, and that one line is the entire phenomenon.

This connects directly back to [chapter 3](03-value-types-vs-reference-types.md#read-only-aliases-in-and-ref-readonly), where `in` was introduced as the way to avoid copying a large struct.
Here is the sting: **`in` on a mutable struct can reintroduce the very copy it was meant to avoid**, once per member access rather than once per call.
For a large struct in a loop, `in` can end up slower than passing by value.

### 2.4 The fix: `readonly` as a promise to the compiler

> [How to Avoid Defensive Copies for Structs?](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/)

The lesson walks a single example through progressively stronger guarantees.
The snippets are scratch code from a decompiler playground rather than production shapes, so read them for the modifier that changes rather than for the constructors.

**Start**: pass by value, no copy issue, no `in` benefit either.

```csharp
public struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }

    public double Distance => Math.Sqrt(X * X + Y * Y);
}

public class C {

    public void M( Point p) {

        Console.WriteLine(p.Distance);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=40)

Change the signature to `M(in Point p)` and `p.Distance` starts making a defensive copy, because `Distance` is not marked `readonly`.

**Step 1**: mark the member.

```csharp
public readonly double Distance => Math.Sqrt(X * X + Y * Y);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=55)

The copy disappears for that member.
The chapter then shows how narrow that fix is: turn `X` from an auto-property into a hand-written getter (`public int X => 42;`) and reading `p.X` brings the copy back, because a custom getter is no longer provably safe.

**Step 2**: mark the type, which makes every member implicitly `readonly`.

```csharp
public readonly struct Point
{
    public int X => 42;
    public int Y { get; }

    public Point(int x, int y) { Y = y; }

    public readonly double Distance => Math.Sqrt(X * X + Y * Y);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/how-to-avoid-defensive-copies-for-structs-69958860/?t=80)

`readonly struct` is the one that scales, since it cannot be defeated by someone later adding a member and forgetting a keyword.

**How to confirm it worked**: the chapter is explicit that lowered C# in some tools does not show these copies, and that you have to look at **IL**.
The tell is a local variable appearing before the call.
Remove `readonly` from the field, or add `readonly` to the member, and the local vanishes.

The chapter's recommendation, which is also just good design: prefer `readonly struct` by default, and treat a mutable struct as a deliberate choice you can justify.

---

## Verified on this machine

Every claim in this chapter is observable, so all of them were run on **.NET 10.0.11, Windows 11 x64**.
Nothing was added to the repo; this was a throwaway console app.

```
Runtime: .NET 10.0.11
=== 1. Boxing ===
counter.Value = 0
=== 2. Indexers ===
List element after Increment: 0
Array element after Increment: 1
=== 3. Readonly field trap ===
  Advanced: True, Position: 0
  Advanced: True, Position: 0
  Advanced: True, Position: 0
=== 4. Non-readonly field, same struct ===
  Advanced: True, Position: 1
  Advanced: True, Position: 2
  Advanced: True, Position: 3
=== 5. Property getter that reassigns this ===
  X: 3
  Y: 4
  Distance: 0
  after reading Distance -> X: 3, Y: 4
=== 6. in parameter ===
  Distance seen inside: 0
after ByIn, p = (3, 4)
```

Every course claim reproduced exactly.
Three observations are worth keeping:

**Section 4 is a control the chapter does not show, and it is the clearest proof available.**
It is the identical `Parser` class with one character changed: `readonly` removed from the field.
The reader then advances 1, 2, 3 as anyone would expect.
Same struct, same loop, same call, and the only variable is the `readonly` keyword, which pins the cause precisely.

**Section 5 shows the defensive copy doing its job rather than causing a bug.**
`Distance` reassigns `this` to `(1, 2)` and returns 0, yet `_point` is still `(3, 4)` afterwards.
Without the copy, a property getter would have silently overwritten a `readonly` field.

**None of this produces a single compiler warning.**
The probe built with `0 Warning(s)` while containing all six traps.

> **Aside: there is an analyzer, and it is not on by default.**
> **IDE0251, "Member can be made `readonly`"**, flags exactly the members whose omission causes defensive copies, and **IDE0250** does the same for whole structs.
> Enabling `dotnet_diagnostic.IDE0251.severity = warning` in the probe immediately flagged `public int Position => _position;` in `SequenceReader`.
> This repo already sets `EnforceCodeStyleInBuild=true` in `src/Directory.Build.props` and enables IDE0005 and IDE0161 in `src/.editorconfig`, so adding these two rules would cost one line each and would catch the whole category at build time.
> That is the same move the previous chapter made with [CA2214](04-mastering-classes.md#14-letting-the-analyzer-catch-it): stop relying on remembering, and let the build fail.

---

## Common misconceptions

**"`readonly` on a struct field just stops me reassigning it."**
That is what it means for a class field.
For a struct field it protects the **data**, which is why the compiler has to copy defensively to keep the promise.

**"`readonly` makes the code safer."**
Here it is what makes the code wrong.
The reader in [2.1](#21-the-trap-a-reader-that-never-advances) works fine until someone adds `readonly` to the field, and deleting the keyword fixes it.
The real fix is `readonly` on the struct instead, but the immediate lesson is that the modifier is not free.

**"`in` is a free optimization for large structs."**
Only for `readonly struct` or members marked `readonly`.
On a mutable struct, `in` triggers a defensive copy **per member access**, which can be worse than passing by value.

**"`list[0]` and `array[0]` are the same thing."**
One is a property returning a copy, the other is a reference to storage.
For structs they behave oppositely, and only the assignment form is caught at compile time.

**"A property getter can't change anything."**
On a non-`readonly` struct member, `this = new Point(1, 2);` inside a getter compiles.
This is precisely why the compiler cannot assume getters are safe.

---

## Self-test

1. `((IIncrementable)counter).Increment()` runs twice and `counter.Value` is still 0. How many heap allocations happened, and how many distinct objects were incremented?
2. `list[0].Increment()` compiles but `list[0].Value = 5` does not. Explain the asymmetry, and name the error.
3. Why does `array[0].Increment()` work when the `List<T>` version does not?
4. `TryAdvance()` returns `true` three times while `Position` stays 0. Where did the increments go?
5. Give the one-character change to `Parser` that makes the reader advance correctly, and say why that is still not the fix you should ship.
6. Name the four contexts where a defensive copy can be emitted.
7. State the compiler's rule for when it does *not* emit a copy, and list the three kinds of member access that satisfy it.
8. You have a 64-byte mutable struct and pass it with `in` to a method that reads three of its properties. How many copies, and how does that compare to passing by value?
9. Where do you have to look to confirm a defensive copy is gone, and what exactly disappears?

<details>
<summary>Answer key</summary>

1. Two allocations. Each cast to the interface boxes independently, so two separate heap objects were each incremented once and then discarded. The original was never touched.
2. `list[0]` returns a value, not a variable. Assigning to it is caught as **CS1612, "Cannot modify the return value ... because it is not a variable"**. Calling a mutating method on the same temporary is legal C# and produces no diagnostic, so the compiler blocks the obvious form and permits the subtle one.
3. Array element access has special CLR support and yields a reference to the element's storage location, while `List<T>`'s indexer is an ordinary property returning `T` by value.
4. Into three separate defensive copies. `_reader` is a `readonly` field, so each call copied it into a hidden local, incremented the copy, and discarded it.
5. Delete `readonly` from the `_reader` field. Not shippable as the real fix, because it gives up the immutability guarantee to work around a symptom; making `SequenceReader` a `readonly struct` is not possible here since it genuinely mutates, so the honest options are a mutable field or a redesign that returns a new reader.
6. `readonly` fields, `in` parameters, `ref readonly` parameters, `ref readonly` locals.
7. The compiler emits a copy unless it can prove the access cannot mutate. Proof comes from: fields, auto-properties, and any member explicitly marked `readonly` (including everything in a `readonly struct`).
8. Three copies of 64 bytes each, one per property access, versus a single 64-byte copy for pass-by-value. `in` is the slower option here, which is the opposite of why you would have reached for it.
9. In the IL, not the lowered C#, which some tools do not show it in. What disappears is the local variable holding the copy, emitted just before the call.

</details>

---

## No companion project

Skipped by request.

This chapter would ordinarily earn one, since every claim is a short program with observable output and the `readonly` / non-`readonly` pair is a real experiment rather than a demo.
The verification above was run as a throwaway console app outside the repo, so nothing was added under `src/`.

If it gets built later, the pieces worth having are the six probes reproduced in [Verified on this machine](#verified-on-this-machine), plus an IL view (`ildasm`, ILSpy, or sharplab.io) of `Parse` with and without `readonly` on the field, since the injected local is the thing the notes can only describe.

---

## Threads into later chapters

| Deferred here | Picked up in |
| --- | --- |
| Default struct equality and its performance cost | Mastering Records |
| `record struct` as the readable way to get an immutable value type | Mastering Records |
| Boxing as an allocation source in iteration | Mastering LINQ (the cost of boxed iterators) |
| Mutable structs used deliberately, as enumerators do | Mastering LINQ (iterator as a mutable struct) |

The conclusion lesson hands off explicitly: next is the default equality behaviour of structs and what it costs.
