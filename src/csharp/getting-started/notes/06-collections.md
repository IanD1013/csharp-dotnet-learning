# Collections

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 6
> 3 lessons · ~37:50
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Arrays](https://dometrain.com/take/course/getting-started-csharp-2732244/arrays-54134811/) | 10:55 | [↓](#1-arrays) |
| 2 | [Lists](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/) | 12:12 | [↓](#2-lists) |
| 3 | [Dictionaries](https://dometrain.com/take/course/getting-started-csharp-2732244/dictionaries-54134814/) | 14:43 | [↓](#3-dictionaries) |

---

## 1. Arrays

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/arrays-54134811/) · 10:55

### Summary

Arrays are the most fundamental collection type in C#, allowing developers to group multiple variables of the same type into a single structure.
They are zero-based, meaning the first element is accessed at index 0, and they have a fixed size that must be defined at the time of creation.
Arrays are mutable, allowing for both reading and updating of individual elements, but their overall capacity cannot be changed once initialized.

### Key concepts

* **Zero-based indexing**: The first element of an array is accessed at index 0.
* **Fixed size**: The capacity of an array must be specified at the time of declaration and cannot be changed later.
* **Mutability**: Elements within an array can be modified after the array has been created.
* **Homogeneous types**: Every element within a specific array must be of the same data type.
* **Length property**: The `.Length` property returns the total number of elements an array is defined to hold.

### Lesson notes

In C#, an array is a fixed-size collection of variables of the same type.
This structure is fundamental for handling large datasets efficiently.
Arrays are zero-based, meaning the index of the first element is 0.
Consequently, the index of any given element is its ordinal position minus one—a convention that frequently leads to "off-by-one" errors if not carefully managed.

#### Declaration and Indexing

When declaring an array, the type is followed by square brackets (e.g., `int[]`).
The `new` keyword is used to allocate memory for the array, at which point its size must be specified.
Once created, the size of an array is fixed and cannot be altered.

Arrays are mutable, meaning the values stored in their slots can be updated.
To assign a value to a specific position, use the index within square brackets on the left side of an assignment.
To retrieve or read a value, use the same index notation on the right side of an assignment.

```csharp
// arrays are a collection of variables of the same type
// - arrays are zero based
// - arrays are fixed in size
// - we can get values from an array
// - we can set values in an array

// here is how we declare an array
int[] numbers = new int[3];

// [1]
// [2]
// [3]

// here is how we set values in an array
numbers[0] = 1;
numbers[1] = 2;
numbers[2] = 3;

// here is how we get values from an array
int firstNumber = numbers[0];
int secondNumber = numbers[1];
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/arrays-54134811/?t=325)

#### Initialization Variations

C# offers multiple syntaxes for initializing arrays with data at the time of declaration.
These include explicit initialization with the `new` keyword, implicit initialization using curly braces (known as a collection initializer), and collection expressions using square brackets.
Regardless of the syntax, all elements must be of the same type; for instance, a `double[]` array must contain floating-point values.

#### The Length Property

To determine the size of an existing array, use the `.Length` property.
This is a read-only property that returns the total number of slots defined when the array was created.
This is useful for programmatically iterating through or validating the bounds of the collection.

```csharp
// here is how we set values in an array
numbers[0] = 1;
numbers[1] = 2;
numbers[2] = 3;

// here is how we get values from an array
int firstNumber = numbers[0];
int secondNumber = numbers[1];

// here is how we declare and initialize an array
int[] numbers2 = new int[]
{
    5,
    6,
    7,
    8,
};
double[] numbers3 =
{
    10.1,
    11.1,
    12.1,
};
int[] numbers4 =
[
    3,
    4,
    5,
];

// here is how we get the length of an array
int length = numbers.Length;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/arrays-54134811/?t=535)

While arrays are powerful for fixed-size data sets, C# provides other collection types for scenarios where the size of the data is not known in advance.

---

## 2. Lists

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/) · 12:12

### Summary

Lists are dynamic collection types in C# that allow for flexible storage of multiple values of the same type.
Unlike arrays, which have a fixed size determined at instantiation, lists can grow or shrink as items are added, removed, or inserted.
They utilize zero-based indexing and support various operations such as clearing all elements or sorting the collection based on the underlying data type.

### Key concepts

- **Dynamic sizing**: Lists do not require a predefined size and expand automatically as elements are added.
- **Generics**: The `List<T>` syntax allows the collection to work with any specified type while maintaining type safety.
- **Zero-based indexing**: Accessing and modifying elements starts at index 0.
- **Collection methods**: Built-in support for `Add`, `Remove`, `Insert`, `Clear`, and `Sort`.
- **Count property**: Used to retrieve the current number of elements in the list, replacing the `Length` property used in arrays.

### Lesson notes

Lists are very similar to arrays but offer additional characteristics that make them easier to use in dynamic situations.
Like arrays, lists are zero-based, meaning the first element is accessed at index 0.
The primary difference is that lists are dynamic in size; while it is helpful to know the size upfront, it is not required as the list grows automatically as items are added.

#### Declaration and Adding Items

To declare a list, use the `List<T>` syntax, where `T` represents the type of values the list will hold.
This is a feature called generics, meaning the list implementation works the same way regardless of whether it holds strings, integers, or other types.
You can use the full declaration or the shortened `new()` syntax.

```csharp
// lists are used to store multiple values
// - lists are zero based
// - lists are dynamic in size
// - we can get values from a list
// - we can set values in a list
// - we can add values to a list
// - we can remove values from a list
// - we can insert values into a list
// - we can clear a list
// - we can sort a list!

// here is how we declare a list
List<string> words = new List<string>();

// here is how we add values to a list
words.Add("one");
words.Add("two");
words.Add("three");

// here is how we get values from a list
string firstWord = words[0];
string secondWord = words[1];
string thirdWord = words[2];

// here is how we declare and initialize a list
List<int> numbers = new List<int>
{
    1,
    2,
    3,
    4,
};
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/?t=25)

#### Accessing and Modifying Lists

Values are read from a list using square bracket notation.
You can also overwrite a value at a specific index by assigning a new value to it.
To determine how many items are currently in a list, use the `Count` property.

```csharp
words[0] = "four";

// here is how we declare and initialize a list
List<int> numbers = new List<int>
{
    1,
    2,
    3,
    4,
};

// here is how we get the count of a list
int count = numbers.Count; //4
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/?t=265)

#### Removing and Inserting Items

Unlike arrays, lists allow you to remove specific items or insert them at a particular index.
The `Remove` method takes the item itself as an argument and removes its first occurrence from the list.
The `Insert` method takes two arguments: the index where the item should be placed and the item itself.
When an item is inserted, the subsequent items are shifted to accommodate the new value.

```csharp
// here is how we remove a value from a list
numbers.Remove(1);
numbers.Remove(2);
numbers.Remove(3);
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/?t=295)

After removing 1, 2, and 3 from a list containing 1, 2, 3, and 4, only the value 4 remains.
If you then perform insertions, the list grows again:

```csharp
// here is how we insert a value into a list
numbers.Insert(0, 1);
numbers.Insert(0, 2);
numbers.Insert(1, 3);

// here is how we clear a list
numbers.Clear();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/?t=325)

In the example above, inserting 1 at index 0 results in `[1, 4]`.
Inserting 2 at index 0 shifts the 1, resulting in `[2, 1, 4]`.
Finally, inserting 3 at index 1 results in `[2, 3, 1, 4]`.
The `Clear` method can be used to remove all items from the list at once.

#### Sorting Lists

Lists can be sorted using the `Sort` method.
The sorting logic depends on the data type: numeric types are sorted in ascending order, while strings are sorted alphabetically.

```csharp
// here is how we sort a list
words.Sort();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/?t=370)

For a list of strings containing "four", "two", and "three", sorting alphabetically results in "four" (starting with F), followed by "three" (starting with TH), and finally "two" (starting with TW).

```csharp
words[0] = "four";
// words is now:
// ["four"]
// ["two"]
// ["three"]
// after sorting:
// ["four"]
// ["three"]
// ["two"]

// here is how we declare and initialize a list
List<int> numbers = new List<int>
{
    1,
    2,
    3,
    4,
};
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/lists-54134812/?t=670)

---

## 3. Dictionaries

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/dictionaries-54134814/) · 14:43

### Summary

A dictionary is a dynamic collection in C# that stores key-value pairs, allowing for efficient lookups using unique keys of any type rather than just numeric indices.
Dictionaries are unordered by default and provide high-performance retrieval, making them ideal for large datasets where searching through a linear collection would be inefficient.

### Key concepts

* **Key-Value Pairs**: Dictionaries map unique keys to specific values.
* **Generics**: Dictionaries use type parameters `<TKey, TValue>` to define the types for keys and values.
* **Uniqueness**: Keys must be unique within a dictionary; attempting to add a duplicate key via the `Add` method results in an error.
* **Dynamic Size**: Like lists, dictionaries can grow and shrink as items are added or removed.
* **Performance**: Dictionaries offer extremely fast lookups compared to iterating through linear collections like arrays or lists.
* **Safe Access**: Methods like `ContainsKey` and `TryGetValue` prevent runtime errors when accessing potentially missing keys.

### Lesson notes

Dictionaries are generic collections that store data in key-value pairs.
Unlike arrays or lists, which map integer indices to values, dictionaries can use any type as a key.
They are dynamic in size, allowing for the addition and removal of elements at runtime.
Crucially, dictionaries are unordered; the sequence in which items are added is not guaranteed to be preserved.

To declare a dictionary, you specify the types for both the key and the value within angle brackets.
C# also supports a shorthand "target-typed new" syntax to reduce redundancy.

```csharp
// here is how we declare a dictionary
Dictionary<string, int> wordsToNumbers = new Dictionary<string, int>();
Dictionary<int, string> numbersToWords = new Dictionary<int, string>();
Dictionary<string, int> shorthand = new();

// here is how we add entries into a dictionary
wordsToNumbers.Add("one", 1);
wordsToNumbers.Add("two", 2);
wordsToNumbers.Add("three", 3);

// here is how we get values from a dictionary
int one = wordsToNumbers["one"];
int two = wordsToNumbers["two"];
int three = wordsToNumbers["three"];
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/dictionaries-54134814/?t=25)

The indexer syntax can also be used to update existing values.
If a key already exists, assigning a new value to it via the indexer will overwrite the previous entry.

```csharp
// here is how we set values in a dictionary
wordsToNumbers["one"] = 111;
wordsToNumbers["two"] = 222;

// wordsToNumbers is now:
// ["one"] = 111
// ["two"] = 222
// ["three"] = 3
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/dictionaries-54134814/?t=325)

There are multiple ways to initialize a dictionary with data at the time of declaration.
You can use the standard collection initializer syntax with nested curly braces or the indexer-style initializer, which is often preferred for readability.

```csharp
// here is how we declare and initialize a dictionary
Dictionary<int, string> numbersToWords2 = new Dictionary<int, string>
{
    { 1, "one" },
    { 2, "two" },
    { 3, "three" },
    { 4, "four" },
};
Dictionary<int, string> numbersToWords3 = new Dictionary<int, string>
{
    [1] = "one",
    [2] = "two",
    [3] = "three",
    [4] = "four",
};
Dictionary<int, string> numbersToWords4 = new()
{
    [1] = "one",
    [2] = "two",
    [3] = "three",
    [4] = "four",
};
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/dictionaries-54134814/?t=490)

The `Count` property returns the number of key-value pairs currently in the collection.
To manage the contents, you can use `Remove(key)` to delete a specific entry or `Clear()` to empty the entire dictionary.
The `ContainsKey` method allows you to verify the existence of a key before attempting to access it.

```csharp
// here is how we get the count of a dictionary
int count = numbersToWords2.Count; //4

// here is how we remove a value from a dictionary
numbersToWords2.Remove(1);
numbersToWords2.Remove(2);

// here is how we clear a dictionary
numbersToWords3.Clear();
numbersToWords4.Clear();

// here is how we check if a dictionary contains a key
bool contains = numbersToWords2.ContainsKey(3); //true
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/dictionaries-54134814/?t=595)

For a more efficient lookup that combines checking for existence and retrieving the value, use `TryGetValue`.
This method returns a boolean indicating if the key was found and uses an `out` parameter to provide the value.
If the key is missing, the `out` variable is assigned a `null` value.
Additionally, while the `Add` method throws an exception if a key already exists, the indexer syntax safely adds or updates the value.

```csharp
// here is how we check and get a value from a dictionary
bool contains2 = numbersToWords2.TryGetValue(
    5,
    out string? value);

// what happens if we add something that already exists?
// ERROR!
// wordsToNumbers.Add("one", 1);
// numbersToWords2.Add(1, "one");

// we can use the indexer to add or set values
// which will overwrite existing values
wordsToNumbers["one"] = 1;
numbersToWords2[1] = "one";
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/dictionaries-54134814/?t=775)
