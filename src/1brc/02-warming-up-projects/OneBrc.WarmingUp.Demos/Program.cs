using System.Globalization;

using OneBrc.WarmingUp.Core;
using OneBrc.WarmingUp.Demos.Sections;

var section = args.Length > 0 ? args[0] : "all";
var rowCount = args.Length > 1 ? long.Parse(args[1], CultureInfo.InvariantCulture) : 1_000_000L;

Console.WriteLine($"Files directory: {GlobalConstants.FilesDirectory}");
Console.WriteLine();

switch (section)
{
    case "spec":
        WhatIs1Brc.Run();
        break;

    case "generator":
        DataGeneratorAndOtherProjects.Run(rowCount);
        break;

    case "test-files":
        LetsGenerateTestFiles.Run();
        break;

    case "all":
        WhatIs1Brc.Run();
        DataGeneratorAndOtherProjects.Run(rowCount);
        LetsGenerateTestFiles.Run();
        break;

    default:
        Console.WriteLine("Usage: dotnet run -c Release -- [spec|generator|test-files|all] [rowCount]");
        break;
}
