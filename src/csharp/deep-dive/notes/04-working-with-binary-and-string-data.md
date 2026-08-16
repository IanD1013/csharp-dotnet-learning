# Working With Binary and String Data

> Course: [Deep Dive: C#](https://dometrain.com/course/deep-dive-csharp/) · Chapter 4
> 5 lessons · ~58:01
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Encoding Strings and Bytes](https://dometrain.com/take/course/deep-dive-csharp-2732260/encoding-strings-and-bytes-54135525/) | 9:57 | [↓](#1-encoding-strings-and-bytes) |
| 2 | [Streams](https://dometrain.com/take/course/deep-dive-csharp-2732260/streams-54135526/) | 11:54 | [↓](#2-streams) |
| 3 | [Reading and Writing Files](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/) | 12:41 | [↓](#3-reading-and-writing-files) |
| 4 | [Using & Disposable](https://dometrain.com/take/course/deep-dive-csharp-2732260/using-disposable-54135528/) | 9:51 | [↓](#4-using--disposable) |
| 5 | [XML and JSON](https://dometrain.com/take/course/deep-dive-csharp-2732260/xml-and-json-54135529/) | 13:38 | [↓](#5-xml-and-json) |

---

## 1. Encoding Strings and Bytes

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/encoding-strings-and-bytes-54135525/) · 9:57

### Summary

Character encoding is the essential mechanism for translating between human-readable C# strings and the binary data required by computers for storage and transmission.
This lesson contrasts the legacy ASCII standard, which is limited to 128 characters, with the modern Unicode standard and its UTF-8 encoding.
It demonstrates that while ASCII is sufficient for basic English text, it fails to preserve data integrity for complex characters like emojis, leading to permanent data loss.
By using UTF-8, developers can ensure that a much broader range of characters is accurately represented in binary form and successfully reconstructed without corruption.

### Key concepts

- **Encoding**: A mapping system that converts human-readable characters to binary data and vice versa.
- **ASCII**: A legacy encoding standard (1963) supporting 128 characters, including the English alphabet, numbers, and basic symbols.
- **Unicode**: A universal standard that maps characters to unique numeric values, supporting a vast array of symbols and languages.
- **UTF-8**: A variable-width encoding for Unicode that represents characters as one to four bytes, maintaining compatibility with ASCII while supporting complex characters.
- **Data Resolution Loss**: Permanent corruption that occurs when an encoding cannot represent a specific character, often resulting in replacement characters like `?` or `!`.

### Lesson notes

In C#, a string is a sequence of characters.
While humans read words and symbols, computers process only binary data.
Encoding provides the mapping required to convert between these two formats, which is critical for saving data to disk or transferring it over a network.

#### ASCII Encoding

ASCII (American Standard Code for Information Interchange) is a basic character encoding established in 1963.
Due to historical constraints, it supports only 128 characters, including the English alphabet (upper and lower case), numbers, and a limited set of symbols.
This was sufficient for early computing needs in English-speaking contexts.

To convert a string to bytes using ASCII, use the `Encoding.ASCII` property from the `System.Text` namespace.
The `GetBytes` method converts the string to a byte array, and `GetString` performs the reverse operation.

```csharp
using System.Text;

string helloWorld = "Hello World!";
byte[] bytesForHelloWorldAsAscii = Encoding.ASCII.GetBytes(helloWorld);

// we can go backwards too!
string helloWorldConvertedBack = Encoding.ASCII.GetString(bytesForHelloWorldAsAscii);

Console.WriteLine($"Converting '{helloWorld}' to bytes and back with ASCII");
Console.WriteLine($" Original: {helloWorld}");
Console.WriteLine($"Converted: {helloWorldConvertedBack}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/encoding-strings-and-bytes-54135525/?t=40)

#### Limitations and Data Loss

When a string contains characters outside the 128-character ASCII range, such as emojis, ASCII encoding cannot map them correctly.
This results in data resolution loss.
During the conversion to bytes, the original character is replaced by an undefined character marker (often rendered as a question mark or exclamation mark in the console).
This data loss is permanent; once the string is encoded incorrectly to bytes, the original information cannot be recovered from those bytes.

```csharp
// what happens if we use characters in the string
// that aren't in the ASCII character set?
string unsupportedAsciiString = "😀 I'm in danger!";
byte[] unsupportedAsciiBytes = Encoding.ASCII.GetBytes(unsupportedAsciiString);
string convertedBackFromUnsupportedAscii = Encoding.ASCII.GetString(unsupportedAsciiBytes);

Console.WriteLine("Converting to ASCII and back with unsupported characters");
Console.WriteLine($" Original: {unsupportedAsciiString}");
Console.WriteLine($"Converted: {convertedBackFromUnsupportedAscii}");
Console.WriteLine($" Original String Length: {unsupportedAsciiString.Length}");
Console.WriteLine($"Converted String Length: {convertedBackFromUnsupportedAscii.Length}");
Console.WriteLine($"     ASCII Bytes Length: {unsupportedAsciiBytes.Length}");
Console.WriteLine($"Strings Equal: {unsupportedAsciiString == convertedBackFromUnsupportedAscii}");
Console.WriteLine($"First Chars Equal: {unsupportedAsciiString[0] == convertedBackFromUnsupportedAscii[0]}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/encoding-strings-and-bytes-54135525/?t=205)

In the example above, the original string length is 17 (the emoji `😀` is a surrogate pair consisting of two `char` values in C#).
While the length remains 17 after conversion, the content is no longer equal because the emoji was corrupted during the ASCII encoding process.

#### Unicode and UTF-8

To handle a larger character set, we use Unicode.
Unicode maps characters to numbers, and encodings like UTF-8, UTF-16, and UTF-32 determine how those numbers are represented in bytes.
UTF-8 is the most common encoding in modern software.

UTF-8 can represent complex characters by using more bytes than ASCII.
For instance, while ASCII uses 1 byte per character, UTF-8 might use 4 bytes for an emoji.
This increased byte length provides the resolution necessary to map the character accurately in both directions.

```csharp
// to handle this, we can use "Unicode", which is a standard
// that that defines a much larger character set
// and we'll use UTF-8, which is a way to encode Unicode characters
// to make this example work!
byte[] unsupportedStringAsUtf8Bytes = Encoding.UTF8.GetBytes(unsupportedAsciiString);
string unsupportedStringAsUtf8 = Encoding.UTF8.GetString(unsupportedStringAsUtf8Bytes);

Console.WriteLine("Converting to UTF-8 and back with the original characters");
Console.WriteLine($" Original: {unsupportedAsciiString}");
Console.WriteLine($"Converted: {unsupportedStringAsUtf8}");
Console.WriteLine($"   Original Length: {unsupportedAsciiString.Length}");
Console.WriteLine($"  Converted Length: {unsupportedStringAsUtf8.Length}");
Console.WriteLine($"ASCII Bytes Length: {unsupportedAsciiBytes.Length}");
Console.WriteLine($" UTF8 Bytes Length: {unsupportedStringAsUtf8Bytes.Length}");
Console.WriteLine($"Strings Equal: {unsupportedAsciiString == unsupportedStringAsUtf8}");
Console.WriteLine($"First Chars Equal: {unsupportedAsciiString[0] == unsupportedStringAsUtf8[0]}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/encoding-strings-and-bytes-54135525/?t=430)

Using UTF-8 ensures that the strings remain equal after the round-trip conversion, as the encoding supports the full range of characters present in the original string.

---

## 2. Streams

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/streams-54135526/) · 11:54

### Summary

Streams in C# provide a unified, abstract API for working with binary data, decoupling the consumer from the underlying implementation, whether it is a byte array, a file, or a network stream.
By inheriting from the abstract Stream base class, all stream types share a common interface for navigating, reading, and writing data.
This abstraction allows developers to focus on data processing logic rather than the specifics of the data source or destination.

### Key concepts

- **Abstract Stream Base Class**: The `System.IO.Stream` class defines the standard API for all stream implementations.
- **Stream Properties**: Key properties include `Position` (current pointer), `Length` (total data size), and `CanRead`/`CanWrite`/`CanSeek` (capability flags).
- **MemoryStream**: A specific implementation backed by a byte array in memory.
- **Capacity vs. Length**: In a `MemoryStream`, `Length` is the actual number of bytes written, while `Capacity` is the size of the underlying buffer, which grows dynamically to optimize performance.
- **Navigation**: Moving the stream pointer using the `Seek` method or by directly setting the `Position` property.
- **Buffer-based I/O**: Reading and writing operations use byte arrays (buffers) to transfer data to and from the stream.

### Lesson notes

The `Stream` class is the abstract base for all streams in .NET.
Because it is abstract, it cannot be instantiated directly, but it provides the contract that all derived streams must follow.
This API allows for navigation through a set of bytes regardless of whether the backing source is a byte array, a file, or a network connection.

```csharp
// Streams in C# allow us to work with data
// in a more abstract way than just having
// a byte array.
// Streams provide us a common API for working
// with binary data without having to know
// exactly how that data is represented
// behind the scenes!

// Remember inheritance? There is a base class
// for all streams called Stream, which is abstract.
// Stream stream = new Stream(); // won't compile!

// Streams provide information about the data source
// that we're working with, like:
//  - the length of the data
//  - the current position in the data
//  - whether we can read from or write to the data
// It's important to note that all streams get
// these properties/methods because they inherit
// from the base... but not all of them support
// all of the functionality!

// let's start with one of the most basic streams,
// the MemoryStream

using System.Text;

MemoryStream memoryStream = new MemoryStream();

// we can write data to a memory stream
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/streams-54135526/?t=40)

#### Working with MemoryStream

A `MemoryStream` is a stream backed by an array of bytes.
When a new `MemoryStream` is initialized, its `Position`, `Length`, and `Capacity` all start at zero.
As data is written, the `Position` and `Length` increase.
The `Capacity` represents the size of the internal buffer; to avoid frequent reallocations, the `Capacity` often grows in powers of two, remaining larger than the `Length` to optimize performance.

To write data to a stream, use the `Write` method.
This method requires a source byte array, an offset into that array, and the count of bytes to write.

```csharp
// let's start with one of the most basic streams,
// the MemoryStream

using System.Text;

MemoryStream memoryStream = new MemoryStream();

// we can write data to a memory stream
Console.WriteLine("Writing data to memory stream...");
Console.WriteLine($"Stream Position Before: {memoryStream.Position}");
Console.WriteLine($" Stream Length Before: {memoryStream.Length}");
Console.WriteLine($"Stream Capacity Before: {memoryStream.Capacity}");

byte[] data = Encoding.UTF8.GetBytes("Hello, World! From Nick Cosentino");
memoryStream.Write(
    data,
    offset: 0,
    count: data.Length);

Console.WriteLine($"Stream Position After: {memoryStream.Position}");
Console.WriteLine($"Stream Length After: {memoryStream.Length}");
Console.WriteLine($"Stream Capacity After: {memoryStream.Capacity}");
Console.WriteLine();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/streams-54135526/?t=250)

#### Navigation and Reading

After writing to a stream, the `Position` pointer is located at the end of the data.
To read the data back, the stream must be repositioned to the beginning.
This can be achieved using the `Seek` method with `SeekOrigin.Begin` or by setting the `Position` property directly to `0`.

Reading requires a destination buffer (a byte array).
The `Read` method populates this buffer and returns an integer representing the actual number of bytes read.
This return value is critical because the stream might contain less data than the buffer's total size.

```csharp
// if we wanted to read the data back out of the stream,
// it's important to note the position we're currently in now

// let's get back to the start using "Seek", which allows
// us to specify where we're seeking relative to
Console.WriteLine("Repositioning memory stream...");
memoryStream.Seek(0, SeekOrigin.Begin);
// or
memoryStream.Position = 0;
Console.WriteLine($"Stream Position After: {memoryStream.Position}");

// now we can read our data back out!
// ... but how much data do we need to read?
// ... where are we putting it?

byte[] readBuffer = new byte[memoryStream.Length];

Console.WriteLine("Reading data from memory stream...");
int numberOfBytesRead = memoryStream.Read(
    readBuffer,
    0,
    readBuffer.Length);
Console.WriteLine($"Number of bytes read: {numberOfBytesRead}");

string readString = Encoding.UTF8.GetString(readBuffer);
Console.WriteLine($"Read string: {readString}");
Console.WriteLine();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/streams-54135526/?t=415)

#### MemoryStream Shortcuts

While the standard `Read` and `Write` methods work across all stream types, `MemoryStream` provides a specific shortcut called `ToArray()`.
This method returns a copy of the stream's contents as a byte array, which is more convenient than manually managing a read buffer when the entire content is needed.

```csharp
// also important to note that MemoryStream
// has a little "shortcut" for getting the bytes:
Console.WriteLine("Reading data from memory stream using ToArray()...");
byte[] memoryStreamBytes = memoryStream.ToArray();
readString = Encoding.UTF8.GetString(memoryStreamBytes);
Console.WriteLine($"Read string: {readString}");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/streams-54135526/?t=625)

---

## 3. Reading and Writing Files

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/) · 12:41

### Summary

C# provides a robust set of tools for file I/O, ranging from high-level convenience methods in the static File class to low-level stream manipulation via FileStream.
While methods like WriteAllText and ReadAllBytes are efficient for small files, they require careful consideration of character encoding to correctly translate between strings and binary data.
For larger or more complex data, FileStream allows for granular control over file modes, access permissions, and sharing.
To prevent memory exhaustion when handling large files, developers should utilize incremental reading strategies such as StreamReader for text or manual byte chunking, ensuring that only manageable portions of data are loaded into memory at any given time.

### Key concepts

- Static `File` class convenience methods for atomic read/write operations.
- String-to-byte encoding requirements using `System.Text.Encoding`.
- `FileStream` configuration using `FileMode`, `FileAccess`, and `FileShare`.
- The inheritance relationship between `FileStream` and the base `Stream` class.
- Managing stream `Position` via the `Seek` method.
- Memory-safe reading strategies: `StreamReader` for text and byte chunking for binary data.
- Resource cleanup and closing streams.

### Lesson notes

The static `File` class in C# provides several convenience methods for common file operations.
Methods such as `WriteAllText` and `WriteAllBytes` allow for quick output, while `ReadAllBytes` and `ReadAllText` handle input.
Because files are fundamentally binary data, string-based methods require an encoding (typically UTF-8) to translate characters into bytes.
If an encoding is not specified, the system uses a default, but explicit encoding ensures data consistency across different environments.

```csharp
// We can read and write files using built-in classes in C#.

// There are many convenience methods that we have access to:
using System.Text;

File.WriteAllText(
    "some-file.txt",
    "Hello, World!");
File.WriteAllBytes(
    "some-file.txt",
    Encoding.UTF8.GetBytes("Hello, World! From Nick Cosentino"));
byte[] someFileBytes = File.ReadAllBytes("some-file.txt");
string someFileString = File.ReadAllText("some-file.txt");
File.Delete("some-file.txt");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/?t=25)

For more granular control, the `File.Open` method provides access to a `FileStream`.
This allows developers to specify the `FileMode` (e.g., `OpenOrCreate`), `FileAccess` (e.g., `Write`), and `FileShare`.
`FileShare` determines the level of access other processes have to the file while it is open; for instance, `FileShare.Read` allows other processes to read the file while the current process is writing to it.

```csharp
// but we can use similar methods to gain access to a stream!
FileStream fileStream = File.Open(
    "some-file.txt",
    FileMode.OpenOrCreate,
    FileAccess.Write,
    FileShare.Read);
byte[] buffer = Encoding.UTF8.GetBytes("Hello, World!");
fileStream.Write(buffer, 0, buffer.Length);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/?t=145)

`FileStream` inherits from the base `Stream` class, meaning it shares a common API with other stream types like `MemoryStream`.
This allows a `FileStream` to be treated as a generic `Stream`.
Writing to a stream advances its internal `Position`.
To overwrite existing data or re-read from the start, the `Seek` method is used to reset the position to the beginning of the stream.

```csharp
// but these are the APIs directly on the Stream class!
Stream fileStreamAsStream = fileStream;
fileStreamAsStream.Seek(0, SeekOrigin.Begin);
fileStreamAsStream.Write(buffer, 0, buffer.Length);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/?t=265)

Once operations are complete, the stream should be closed to release system resources.

```csharp
// what do we do when we're all done?
fileStream.Close();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/?t=355)

When reading files, one approach is to read the entire content into a buffer based on the stream's `Length`.
However, this is dangerous for large files (e.g., several gigabytes) as it can lead to memory exhaustion.

```csharp
// We can use a very similar API for reading from a file:
FileStream readingStream = File.Open(
    "some-file.txt",
    FileMode.Open,
    FileAccess.Read,
    FileShare.None);

// ...but how many bytes do we need if we just want... all of it?
// we *could* use the Length property of the stream...
byte[] bufferForReading = new byte[readingStream.Length];
var bytesReadFromStream = readingStream.Read(
    bufferForReading,
    0,
    bufferForReading.Length);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/?t=370)

For text files, `StreamReader` provides a string-oriented interface.
It allows for incremental processing, such as reading a file line-by-line using a `while` loop and `ReadLine()`.
This is significantly more memory-efficient than loading the entire file at once.

```csharp
readingStream.Seek(0, SeekOrigin.Begin);
StreamReader reader = new StreamReader(
    readingStream,
    encoding: Encoding.UTF8);
while (!reader.EndOfStream)
{
    string line = reader.ReadLine();
    Console.WriteLine(line);
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/?t=475)

A general-purpose strategy for handling large files is "chunking."
By using a fixed-size buffer (typically a power of two like 1024), data is read in manageable increments.
The `Read` method returns the number of bytes actually placed into the buffer.
It is critical to use this return value because the final read operation may not fill the entire buffer; in such cases, only the first $n$ bytes of the buffer are valid, while the remaining bytes contain stale data from the previous iteration.

```csharp
// you could also write your program to read blocks
// of bytes from the stream at a time by "chunking"
// it up and determining when you've read
// enough!
int chunkSize = 7; // 1024;
Console.WriteLine($"Reading chunks of size {chunkSize} of file...");
readingStream.Seek(0, SeekOrigin.Begin);
byte[] bufferForChunking = new byte[chunkSize];
while ((bytesReadFromStream = readingStream.Read(
    bufferForChunking,
    0,
    bufferForChunking.Length)) > 0)
{
    Console.WriteLine($"Read {bytesReadFromStream} bytes for this chunk!");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/reading-and-writing-files-54135527/?t=550)

---

## 4. Using & Disposable

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/using-disposable-54135528/) · 9:51

### Summary

While C# is a managed language that handles memory automatically, certain external resources like file handles and network streams require explicit cleanup to be released back to the operating system.
The IDisposable interface provides a standard mechanism for this through its Dispose method.
To ensure resources are always cleaned up—even when exceptions occur—C# provides the using statement and the C# 8.0 using declaration, which automate the call to Dispose at the end of a defined scope.

### Key concepts

* **IDisposable Interface**: A standard interface with a single `Dispose()` method used to release unmanaged resources.
* **Deterministic Cleanup**: The practice of explicitly releasing resources as soon as they are no longer needed, rather than waiting for the Garbage Collector.
* **Try-Finally Pattern**: The underlying mechanism used to ensure `Dispose()` is called even if an exception is thrown during resource usage.
* **Using Statement**: A block-scoped syntax that provides a clean way to handle `IDisposable` objects.
* **Using Declaration**: A modern, less indented syntax introduced in C# 8.0 that disposes of an object when its containing scope is exited.

### Lesson notes

C# manages memory for most objects automatically, but resources like files and network connections are "unmanaged" and must be released explicitly.
The `IDisposable` interface is the standard way to indicate that a class owns such resources and requires manual cleanup.

```csharp
public class MyDisposable : IDisposable
{
    public void Dispose()
    {
        // Release resources that are owned by this object
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/using-disposable-54135528/?t=40)

When using a class that implements `IDisposable`, such as a `FileStream`, you are expected to call the `Dispose()` method when finished.
This ensures the resource is freed for other parts of the system or other applications.

```csharp
Stream readmeStream = File.Open("readme.txt", FileMode.Open, FileAccess.Read);

// Perform work with the stream

readmeStream.Dispose();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/using-disposable-54135528/?t=130)

A problem arises if an exception is thrown while the resource is in use; the execution will jump to an exception handler, skipping the `Dispose()` call.
To prevent resource leaks, you can use a `try-finally` block, which guarantees that the `finally` block executes regardless of whether the `try` block succeeds or fails.

```csharp
Stream readmeStream = File.Open("readme.txt", FileMode.Open, FileAccess.Read);
try
{
    // Perform work
}
finally
{
    readmeStream.Dispose();
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/using-disposable-54135528/?t=220)

Because the `try-finally` pattern is verbose and requires declaring the variable outside the block to maintain scope, C# provides the `using` statement.
This statement is a syntactic shortcut that automatically wraps the code in a `try-finally` block and calls `Dispose()` at the closing brace.

```csharp
using (Stream myUsingStream = File.Open("readme.txt", FileMode.Open, FileAccess.Read))
{
    // Perform work
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/using-disposable-54135528/?t=265)

C# 8.0 introduced the `using` declaration, which further simplifies the syntax by removing the need for curly braces.
The resource is disposed of when the variable goes out of scope, typically at the end of the method.

```csharp
using Stream myUsingStream2 = File.Open(
    "readme.txt",
    FileMode.Open,
    FileAccess.Read);

// The stream is disposed when myUsingStream2 goes out of scope
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/using-disposable-54135528/?t=370)

While the `using` declaration is more concise and improves readability by reducing indentation, the traditional `using` block is still useful when you need fine-grained control.
If a resource needs to be released halfway through a long method to free up a file for another operation, the block-based `using` statement or an explicit `Dispose()` call is necessary.

---

## 5. XML and JSON

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/xml-and-json-54135529/) · 13:38

### Summary

This lesson explores the processing of XML and JSON data formats within the .NET ecosystem.
It demonstrates how to use the XmlDocument class for navigating and mutating XML via XPath, and the JsonSerializer for serializing C# objects into JSON and deserializing them back into typed collections using both strings and streams.

### Key concepts

* XML as a hierarchical markup language for data storage and transport.
* Utilizing the System.Xml namespace and XmlDocument for DOM-style manipulation.
* Querying XML structures using XPath syntax via SelectNodes.
* Modifying XML node content using the InnerText property.
* JSON as a lightweight alternative for data representation.
* Serialization and deserialization using the System.Text.Json namespace.
* Mapping JSON structures to C# types using records.
* Handling JSON data through both string and Stream interfaces.

### Lesson notes

XML is a hierarchical markup language designed to store and transport data.
In .NET, the `System.Xml` namespace provides the `XmlDocument` class, which is a built-in tool for reading and writing XML files.
Unlike third-party libraries, this functionality is included directly in the framework.

```csharp
using System.Text.Json;
using System.Xml;
// there are two very popular file formats that we often
// work with as programmers:
// - XML
// - JSON

// XML is a markup language that is designed to store and transport data.

// let's make an example XML to work with!
string rawXml =
    """
    <people>
        <person>
            <name>John Doe</name>
            <age>42</age>
        </person>
        <person>
            <name>Jane Doe</name>
            <age>39</age>
        </person>
    </people>
    """;
File.WriteAllText("people.xml", rawXml);

// we can use the XmlDocument class to read and write XML files
XmlDocument xmlDocument = new XmlDocument();
xmlDocument.Load("people.xml");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/xml-and-json-54135529/?t=25)

When calling `xmlDocument.Load()`, the framework handles the transition from the binary data stored on disk to a structured memory representation.
This process involves using streams to read bytes and encodings to translate those bytes into characters.
Once loaded, the `XmlDocument` provides various methods for navigating the hierarchy, such as accessing the document root or specific child nodes.

To query specific elements, you can use the `SelectNodes` method, which accepts an XPath string.
XPath is a specialized syntax used to indicate which types of elements are required from the document hierarchy.

```csharp
<people>
        <person>
            <name>John Doe</name>
            <age>42</age>
        </person>
        <person>
            <name>Jane Doe</name>
            <age>39</age>
        </person>
    </people>
    """;
File.WriteAllText("people.xml", rawXml);

// we can use the XmlDocument class to read and write XML files
XmlDocument xmlDocument = new XmlDocument();
xmlDocument.Load("people.xml");

// how might the XmlDocument class understand
// how to go between the binary data of the file
// and the text representation of the XML?

// we can use the SelectNodes method to select nodes in the XML
// the parameter that we pass in for being able to select
// different data is called "XPath"
XmlNodeList? people = xmlDocument.SelectNodes("/people/person");
if (people == null)
{
    Console.WriteLine("No people found!");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/xml-and-json-54135529/?t=115)

The resulting `XmlNodeList` can be iterated using a `foreach` loop.
Within this loop, individual nodes can be accessed or modified.
For example, you can target specific child nodes like `name` or `age` and update their `InnerText`.
After modification, the `Save` method writes the updated structure back to a file.

```csharp
XmlNodeList? people = xmlDocument.SelectNodes("/people/person");
if (people == null)
{
    Console.WriteLine("No people found!");
}
else
{
    foreach (XmlNode person in people)
    {
        Console.WriteLine(person["name"].InnerText);
        Console.WriteLine(person["age"].InnerText);

        // let's change all the names to uppercase!
        //person.InnerText = person.InnerText.ToUpper();
        person["name"].InnerText = person["name"].InnerText.ToUpper();
        person["age"].InnerText = person["age"].InnerText.ToUpper();
    }
}

xmlDocument.Save("people2.xml");

Console.WriteLine("Contents of People2:");
Console.WriteLine(File.ReadAllText("people2.xml"));
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/xml-and-json-54135529/?t=430)

JSON is a lighter-weight hierarchical structure often used for the same purposes as XML.
In .NET, the `System.Text.Json` namespace provides the `JsonSerializer` class for serialization (converting objects to strings/bytes) and deserialization (converting strings/bytes back to objects).
To facilitate this, C# objects such as records can be defined to match the expected data structure.

```csharp
// XML is a markup language that is designed to store and transport data.

// let's make an example XML to work with!
string rawXml =
    """

    <people>
        <person>
            <name>John Doe</name>
            <age>42</age>
        </person>
        <person>
            <name>Jane Doe</name>
            <age>39</age>
        </person>
    </people>
    """;


// but what about JSON?
// it's lighter-weight but accomplishes a similar goal

// let's make an example JSON to work with!
// but this time, let's "deserialize" the JSON into a C# object!
public record Person(string Name, int Age);
public record PeopleCollection(Person[] People);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/xml-and-json-54135529/?t=475)

The `JsonSerializer` supports multiple workflows.
You can serialize an object directly to a string or write it to a file.
For deserialization, you can read from a string or use a `Stream` for more efficient processing of binary data.
When using streams, it is important to utilize `using` statements to ensure resources are cleaned up properly.

```csharp
PeopleCollection peopleCollection = new PeopleCollection(new[]
{
    new Person("John Doe", 42),
    new Person("Jane Doe", 39)
});

Console.WriteLine("Serializing people to JSON...");
string rawJson = JsonSerializer.Serialize(peopleCollection);
Console.WriteLine(rawJson);

File.WriteAllText("people.json", rawJson);

Console.WriteLine("Deserializing people from JSON using Stream...");
using var peopleJsonStream = File.OpenRead("people.json");
PeopleCollection deserializedPeopleFromStream = JsonSerializer.Deserialize<PeopleCollection>(peopleJsonStream);

foreach (var person in deserializedPeopleFromStream.People)
{
    Console.WriteLine($"{person.Name}: {person.Age}");
}

Console.WriteLine("Deserializing people from JSON using String...");
var deserializedPeopleFromString = JsonSerializer.Deserialize<PeopleCollection>(File.ReadAllT

foreach (var person in deserializedPeopleFromString.People)
{
    Console.WriteLine($"{person.Name}: {person.Age}");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/xml-and-json-54135529/?t=715)

Both XML and JSON allow developers to take objects in memory, encode them into strings or bytes, and persist them to disk or transmit them over a network.
The process can then be reversed to reconstruct the original objects.
