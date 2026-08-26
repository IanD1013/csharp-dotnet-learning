# Warming Up Projects

> 课程:[From Zero to Hero: 1 Billion Row Performance Challenge in .NET](https://dometrain.com/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet/) · 第 2 章
> 共 3 课 · 约 26:29
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is 1BRC actually?](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-1brc-actually-69958207/) | 9:50 | [↓](#1-what-is-1brc-actually) |
| 2 | [Data Generator and Other Projects](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/data-generator-and-other-projects-69958208/) | 9:03 | [↓](#2-data-generator-and-other-projects) |
| 3 | [Let's Generate Test Files](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-generate-test-files-69958209/) | 7:36 | [↓](#3-lets-generate-test-files) |

## 1. What is 1BRC actually?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/what-is-1brc-actually-69958207/) · 9:50

### 总结

十亿行挑战(1BRC)是一个以性能为导向的任务,它通过解析一个包含十亿条温度记录的巨型文本文件,来测试 .NET 中数据处理能力的极限。
这个挑战最初由 Java 社区提出,要求为最多 10,000 个不重复的气象站计算最低温度、最高温度和平均温度,并按字母顺序输出结果。
首要目标是超越朴素实现(它们可能耗时数分钟),转向高度优化的解决方案,这些方案利用并发、高效的内存管理和底层工程技能,同时始终遵守只能使用标准 .NET 库这一约束。

### 核心概念

*   **Input Scale**:一个 13-14 GB 的 UTF-8 文本文件,恰好包含 1,000,000,000 行。
*   **Data Format**:每一行遵循 `Station Name;Temperature` 的模式(例如 `Hamburg;12.0`)。
*   **Station Constraints**:最多 10,000 个不重复的气象站名称,每个长度在 1 到 100 字节之间。
*   **Temperature Constraints**:取值范围为 -99.9 到 99.9,并且总是恰好带有一位小数。
*   **Output Requirements**:一份排好序的不重复气象站列表,包含它们的最低温度、平均温度(四舍五入到一位小数)和最高温度。
*   **Implementation Rules**:不允许使用外部库(NuGet);解决方案必须是动态的,不能针对某个特定数据集硬编码。

### 课程笔记

十亿行挑战(1BRC)是一场专注于以物理上尽可能快的速度解析大型数据集的竞赛。
输入是一个包含十亿行温度数据的文本文件,总大小约为 13 到 14 GB。
由于文件如此之大,无法用 `File.ReadAllLines()` 这类标准方法把它整个加载进内存,那样会导致应用程序崩溃。

#### 数据格式与编码

输入文件采用 UTF-8 编码。
这是一个关键细节,因为 UTF-8 字符可能是多字节的,也就是说一个字符不保证正好占一个字节。
文件中的每一行代表来自某个气象站的一次测量,格式为气象站名称后跟一个分号,再跟温度值。

*   **Station Names**:长度可以在 1 到 100 字节之间。虽然挑战在测试时可能使用较小的集合(例如 413 个不重复的气象站),但实现必须支持最多 10,000 个不重复的气象站名称。
*   **Temperature Values**:取值范围为 -99.9 到 99.9。格式被严格固定为总是包含恰好一位小数(例如 `12.0` 或 `-5.4`)。

#### 处理要求

应用程序必须处理整个文件,为找到的每一个不重复气象站计算三项特定指标:

1.  **Minimum Temperature**:该气象站记录到的最低值。
2.  **Mean Temperature**:所有记录值的平均数,使用四舍五入(half-up)方式精确保留到一位小数。
3.  **Maximum Temperature**:该气象站记录到的最高值。

最终结果必须按气象站名称的字母顺序打印到标准输出。
要求的输出格式是一个用花括号括起来的逗号分隔列表:`{StationA=min/mean/max, StationB=min/mean/max}`。

#### 工程约束

这个挑战的设计目的是考察工程能力,而不是对库的了解程度。
因此,禁止使用外部 NuGet 包;开发者必须完全依赖标准 .NET 库。
解决方案还必须是动态的,也就是说它不能针对某组特定的气象站名称或某个预先算好的数据集硬编码。
它必须能够正确处理任何按既定规则随机生成的文件。

#### 性能与并发

最终目标是速度。
使用基础 LINQ 或标准文件读取的朴素做法可能需要将近五分钟来处理这些数据,而优化后的方案可以把时间压缩到几秒,在高端硬件上(例如 32 核、128 GB 内存的机器)甚至只要几毫秒。
为了达到这样的结果,开发者需要用满所有可用的 CPU 核心并优化内存使用。
这个挑战的瓶颈不在磁盘 I/O 速度,而在于数据被读进来之后,CPU 和内存能以多高的效率处理它。

## 2. Data Generator and Other Projects

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/data-generator-and-other-projects-69958208/) · 9:03

### 总结

本课介绍 .NET 版十亿行挑战(1BRC)的基础设施,包括一个存放常量和性能日志的共享库,以及一个专门的数据生成器项目。
生成器使用高斯分布产生合成气象数据,以模拟数百个全球气象站上真实的温度波动;与此同时,日志系统会记录执行时间、总体内存使用量和垃圾回收统计信息,便于在不同实现方案之间做性能对比。

### 核心概念

* **Project Structure**:建立一个解决方案,其中包含用于基准测试、共享工具和数据生成的专门项目。
* **Shared Infrastructure**:集中管理文件路径和气象站数量,以确保各个实现层级之间保持一致。
* **Performance Logging**:采集高保真度的指标,包括 Working Set、GC 回收次数(Gen 0、1、2)以及吞吐量(rows/sec 和 MB/sec)。
* **Data Generation**:实现 Box-Muller 变换,生成服从高斯分布的真实感温度数据。
* **1BRC Compliance**:遵循挑战规范,使用不带字节顺序标记(BOM)的 UTF-8 编码和 Unix 风格的行结束符(`\n`)。

### 课程笔记

#### 共享基础设施与性能日志

解决方案被组织成多个项目,以便进行迭代式的性能测试。
一个共享库包含用于文件路径的 `GlobalConstants` 和用于统一性能报告的 `ResultLogger`。
`ResultLogger` 的设计目标是记录每一次迭代的执行上下文,从而可以对不同方案(例如 naive 与 expert)做历史性的对比。

```csharp
namespace Shared;

public static class GlobalConstants
{
    public const string FilesDirectory = "V:\\Dometrain\\1BRC\\Files";
    public const string FilePath = "V:\\Dometrain\\1BRC\\Files\\measurements.txt";

    public const int ExpectedStationCount = 413;
    //public const int ExpectedStationCount = 10_000;
}


public static class ResultLogger
{
    private const string ResultFileName = "results.log";

    /// <summary>
    /// Appends the 1BRC result to a log file in the solution directory.
    /// </summary>
    /// <param name="projectName">Name of the project (e.g., "Level01_Naive")</param>
    /// <param name="output">The 1BRC formatted output string</param>
    /// <param name="elapsed">Time elapsed for processing</param>
    /// <param name="rowCount">Number of rows processed</param>
    /// <param name="stationCount">Number of unique stations found</param>
    public static void SaveResult(
        string projectName,
        string output,
        TimeSpan elapsed,
        long rowCount,
        int stationCount)
    {
        try
        {
            var solutionDirectory = GlobalConstants.FilesDirectory;
            var filePath = Path.Combine(solutionDirectory, ResultFileName);

            // Collect memory statistics
            var workingSetMB = Environment.WorkingSet / 1024 / 1024;
            var gcMemoryMB = GC.GetTotalMemory(false) / 1024 / 1024;
            var gen0Collections = GC.CollectionCount(0);
            var gen1Collections = GC.CollectionCount(1);
            var gen2Collections = GC.CollectionCount(2);

            // Calculate throughput
            var throughputMBps = rowCount > 0 ? (rowCount * 25.0 / 1024 / 1024) / elapsed.TotalSeconds : 0; // Assuming ~25 bytes per row
            var rowsPerSecond = rowCount / elapsed.TotalSeconds;

            var logEntry = $"""
                ================================================================================
                [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {projectName}
                ================================================================================
                Performance:
                  Rows:               {rowCount:N0}
                  Stations:           {stationCount}
                  Elapsed:            {elapsed}
                  Throughput:         {rowsPerSecond:N0} rows/sec ({throughputMBps:F2} MB/sec)
                
                Memory:
                  Working Set:        {workingSetMB:N0} MB
                  GC Memory:          {gcMemoryMB:N0} MB
                  Gen0 Collections:   {gen0Collections}
                  Gen1 Collections:   {gen1Collections}
                  Gen2 Collections:   {gen2Collections}
                
                Processor:
                  CPU Cores:          {Environment.ProcessorCount}
                --------------------------------------------------------------------------------
                {output}



                """;

            File.AppendAllText(filePath, logEntry, System.Text.Encoding.UTF8);
            Console.WriteLine($"\n📁 Results saved: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠️ Results could not be saved: {ex.Message}");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/data-generator-and-other-projects-69958208/?t=10)

日志中包含 `WorkingSet`,用来监控整体内存压力,以及垃圾回收器(GC)统计信息。
高内存占用会增加 GC 压力,而这会对性能产生负面影响。
此外,日志还记录处理器数量,这对评估多核实现的效率至关重要。

#### 数据生成

Data Generator 项目负责创建挑战所需的合成数据集。
它使用一份包含 413 个气象站的列表,其中包括 Unicode 名称,以确保实现能够处理各种字符集。

```csharp
(string Name, double MeanTemp)[] Stations =
[
    ("Abha", 18.0),
    ("Abidjan", 26.0),
    ("Abéché", 29.4),
    ("Accra", 26.4),
    ("Addis Ababa", 16.0),
    ("Adelaide", 17.3),
    ("Aden", 29.1),
    ("Ahvaz", 25.4),
    ("Albuquerque", 14.0),
    ("Alexandra", 11.0),
    ("Alexandria", 20.0),
    // ... additional stations
    ("Zürich", 9.3)
];
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/data-generator-and-other-projects-69958208/?t=265)

生成器会提示输入行数,这样就可以先生成较小的测试文件(例如 10k 或 100k 行),然后再去尝试生成完整的十亿行。
这对快速调试和初期基准测试来说是必不可少的。

```csharp
Console.Write("Row Count: ");
var rowInput = Console.ReadLine();
var rowCount = string.IsNullOrEmpty(rowInput)
    ? DefaultRowCount
    : long.Parse(rowInput
                 .Replace(".", "")
                 .Replace(",", "")
                 .Replace("_", ""));


Console.Write($"  Output file [{DefaultOutputPath}]: ");
var pathInput = Console.ReadLine();
var outputPath = string.IsNullOrWhiteSpace(pathInput) ? DefaultOutputPath : pathInput;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/data-generator-and-other-projects-69958208/?t=355)

为了产生真实感的温度数据,生成器采用了 Box-Muller 变换。
这个算法围绕某个气象站的平均温度构造出温度的高斯(正态)分布,避免数值不切实际地聚集在极值边界(-99.9 到 99.9)附近。

```csharp
// Use UTF-8 encoding WITHOUT BOM (as per 1BRC specification)
var utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, BufferSize, FileOptions.SequentialScan))
using (var writer = new StreamWriter(fileStream, utf8NoBom, BufferSize))
{
    // Use Unix line endings (\n) as per 1BRC specification
    writer.NewLine = "\n";

    var random = new Random();
    Span<char> tempBuffer = stackalloc char[32];

    for (long row = 0; row < rowCount; row++)
    {
        // Randomly select a station
        var stationIndex = random.Next(stationCount);
        var (name, meanTemp) = stations[stationIndex];

        // Generate temperature with Gaussian-like distribution (Box-Muller approximation)
        var u1 = random.NextDouble();
        var u2 = random.NextDouble();
        var stdDev = 10.0;
        var gaussian = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        var temperature = meanTemp + gaussian * stdDev;

        // Clamp to reasonable range and format to 1 decimal place
        temperature = Math.Round(Math.Clamp(temperature, -99.9, 99.9), 1);

        // Write the line
        writer.Write(name);
        writer.Write(';');
        writer.WriteLine(temperature.ToString("0.0"));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/data-generator-and-other-projects-69958208/?t=400)

生成器包含一个进度报告器,它会计算吞吐量(每秒百万行)并给出预计完成时间(ETA)。
生成十亿行通常需要 40 秒到一分钟,具体取决于硬件性能。

```csharp
        // Update progress
        completedRows = row + 1;
        if (completedRows - lastProgressRows >= ProgressInterval)
        {
            lastProgressRows = completedRows;
            var percentage = (double)completedRows / rowCount * 100;
            var elapsed = stopwatch.Elapsed.TotalSeconds;
            var rowsPerSecond = completedRows / elapsed;
            var estimatedTotal = rowCount / rowsPerSecond;
            var remaining = estimatedTotal - elapsed;

            Console.Write($"\r  Progress: {percentage,6:F2}% | {completedRows:N0} / {rowCount:N0} rows | ");
            Console.Write($"{rowsPerSecond / 1_000_000:F2}M rows/sec | ");
            Console.Write($"ETA: {TimeSpan.FromSeconds(remaining):hh\\:mm\\:ss}    ");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/data-generator-and-other-projects-69958208/?t=490)

## 3. Let's Generate Test Files

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-generate-test-files-69958209/) · 7:36

### 总结

本课演示如何运行数据生成器来创建从 10,000 行到十亿行不等的测试文件。
它确立了目标文件格式,即以分号分隔的气象站名称与温度、且温度带一位小数的列表,并给出了这一挑战的规模基准:十亿行的文件大约占用 13 GB 磁盘空间。

### 核心概念

- **File Format**:输出是一个文本文件,其中每一行遵循 `StationName;Temperature` 的模式(例如 `Abha;18.0`)。
- **Temperature Precision**:温度总是格式化为恰好一位小数。
- **Data Scale**:完整的十亿行数据集生成的文件大小约为 12.8 GB 到 13.1 GB。
- **Generation Performance**:生成器的产出速率大约为每秒 600 万行。
- **Incremental Testing**:开发从较小的子集开始(例如 10k 行)以验证逻辑,然后再去处理完整的 13 GB 文件。

### 课程笔记

数据生成过程首先要定义目标行数,以及一组气象站及其各自的平均温度。
这些常量和气象站数组构成了随机数据生成的基础。

```csharp
using Shared;
using System.Diagnostics;
using System.Text;

const long DefaultRowCount = 1_000_000_000L;
const int BufferSize = 8_000_000;
const string DefaultOutputPath = GlobalConstants.FilePath;
const int ProgressInterval = 10_000_000;



(string Name, double MeanTemp)[] Stations =
[
    ("Abha", 18.0),
    ("Abidjan", 26.0),
    ("Abéché", 29.4),
    ("Accra", 26.4),
    ("Addis Ababa", 16.0),
    ("Adelaide", 17.3),
    ("Aden", 29.1),
    ("Ahvaz", 25.4),
    ("Albuquerque", 14.0),
    ("Alexandra", 11.0),
    ("Alexandria", 20.0),
    ("Algiers", 18.2),
    ("Alice Springs", 21.0),
    ("Almaty", 10.0),
    ("Amsterdam", 10.2),
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-generate-test-files-69958209/?t=10)

生成器产出一个文本文件,其中每一行包含一个气象站名称,后跟一个分号和一个温度值。
温度总是恰好包含一位小数(例如 `12.3` 或 `5.0`)。
小数点前的位数会有变化,但格式保持一致。

运行生成器时,用户可以指定行数。
生成 10,000 行几乎是瞬间完成的,产生的文件约为 134 KB。
随着行数每增加十倍,文件大小也按比例扩大:

- 100,000 行:约 1.31 MB
- 1,000,000 行:约 13.1 MB
- 10,000,000 行:约 131 MB
- 100,000,000 行:约 1.3 GB(耗时大约 14 秒)

应用程序会在开始生成之前先验证或创建目标目录,以确保输出环境已经就绪。

```csharp
Console.WriteLine($"  - Output file:    {outputPath}");
Console.WriteLine($"  - Station count:  {Stations.Length}");
Console.WriteLine();

// Ensure output directory exists
var directory = Path.GetDirectoryName(outputPath);
if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
{
    Directory.CreateDirectory(directory);
    Console.WriteLine($"  Directory created: {directory}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet-3256116/let-s-generate-test-files-69958209/?t=355)

在测试硬件上,生成完整的十亿行数据集大约需要 2 分 34 秒,产生的文件约为 12.8 到 13.1 GB,具体取决于气象站名称的随机性。
这个文件是这场性能挑战的主要基准。
初期开发和"朴素"实现会先聚焦在 10k 行的文件上,以确保功能正确,然后再去处理完整规模的数据。

---

## 运行 Demo

Demo 代码按课程官方仓库 [Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet](https://github.com/Dometrain/from-zero-to-hero-1-billion-row-performance-challenge-in-dotnet) 的方式组织,按 level 分组而不是按章节号:

```
src/1brc/
├── notes/            本章及后续各章笔记
├── Shared/           SharedTypes.cs:GlobalConstants、ResultLogger
└── DataGenerator/    Program.cs:413 个气象站 + Box-Muller 生成器
```

后续章节按上游仓库补上 `Level1_Naive/` … `Level6_Expert/` 和 `Benchmarkts/`。

```bash
cd src/1brc/DataGenerator
dotnet run -c Release
```

课程里两个问题都在提示符上手输;这里参数优先,所以也能非交互运行:`dotnet run -c Release -- 10_000_000 [path]`。
生成的测量文件写在 `%TEMP%\1brc\Files` 下,不进入工作区。

10,000 行:

```text
╔══════════════════════════════════════════════════════════════╗
║           1BRC Weather Data Generator (.NET 10)              ║
╚══════════════════════════════════════════════════════════════╝

Row Count:   Output file [C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt]:
  ► Row count:      10,000
  ► Output file:    C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
  ► Station count:  413

  Directory created: C:\Users\iantu\AppData\Local\Temp\1brc\Files
  Generating data...



╔══════════════════════════════════════════════════════════════╗
║                      Generation Complete!                    ║
╚══════════════════════════════════════════════════════════════╝

  Total rows:     10,000
  Time elapsed:   00:00:00.015
  Rows/second:    640,911
  Output file:    C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
  File size:      0.00 GB (138,231 bytes)
```

10,000,000 行:

```text
╔══════════════════════════════════════════════════════════════╗
║           1BRC Weather Data Generator (.NET 10)              ║
╚══════════════════════════════════════════════════════════════╝

Row Count:   Output file [C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt]:
  ► Row count:      10,000,000
  ► Output file:    C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
  ► Station count:  413

  Generating data...

  Progress: 100.00% | 10,000,000 / 10,000,000 rows | 5.59M rows/sec | ETA: 00:00:00

╔══════════════════════════════════════════════════════════════╗
║                      Generation Complete!                    ║
╚══════════════════════════════════════════════════════════════╝

  Total rows:     10,000,000
  Time elapsed:   00:00:01.805
  Rows/second:    5,539,075
  Output file:    C:\Users\iantu\AppData\Local\Temp\1brc\Files\measurements.txt
  File size:      0.13 GB (138,018,630 bytes)
```

生成文件的头几行:

```text
Xi'an;24.3
Gangtok;-0.2
Bujumbura;34.9
Lomé;30.6
Paris;-10.9
```
