I said before that the hello world file doesn't really exist.
That's not completely accurate. It's currently being generated and consumed by the compiler, but it's not being kept
afterwards, at least not in the regular source folders.

If you want to save the output of the generator, you can make a simple change to the consuming project.
I'll open the `demo.domain.csproj` file. Set an element to the main property group called emit compiler generated files
and set it to true.

```cs
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <LangVersion>latest</LangVersion>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
    </PropertyGroup>

    <ItemGroup>
        <ProjectReference Include="..\Demo.Generator\Demo.Generator.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false"/>
    </ItemGroup>

</Project>
```

Rebuild and the generated source files will be saved to a new generated folder inside the obj folder for your build. So
now I have a copy of the file that was generated for the last build, and you 'll need to be aware of this because it can
be a little confusing.

The generated copy of the file that's in the dependencies folder here, that's up to date with the last time the
generator ran.

But the copy that's down here in the object folder is only up to date with the last time the project was compiled, so
there's a little bit of a lag there.

### Why would you even want to do this?

Well, I can think of two scenarios where you would want to keep the generated files.

One, you're not using an IDE, but you still want to check and proofread the output.

Number two, you're required to keep copies of all the source code for any given build in source control. Maybe this is
required to comply with some policy or regulation depending on your industry, or maybe it's just because you need the
generated code to go through a code review like everything else. If your solution includes a source generator, then
someone may want to check its output, especially when you make changes to the generator itself.

Since the files are generated in the project's obj folder, they currently won 't get picked up by Git. So you could
modify your Git ignore file to include these generated files, but a better option is just to change where the files go
in the first place.

```cs
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <LangVersion>latest</LangVersion>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
        <CompilerGeneratedFilesOutputPath>Generated\$(TargetFramework)</CompilerGeneratedFilesOutputPath>
    </PropertyGroup>

    <ItemGroup>
        <ProjectReference Include="..\Demo.Generator\Demo.Generator.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false"/>
    </ItemGroup>
    
    <ItemGroup>
        <Compile Remove="$(CompilerGeneratedFilesOutputPath)/**/*.cs"></Compile>
        <Folder Include="Generated\**" />
    </ItemGroup>

</Project>
```

Since the files in the generated folder are just for reference, we can exclude them from the compilation, so I'll add a
new item group to the domain project, and then I 'll add a compile element within that, and a remove attribute, and I
want to set this to the path of the new generated folder.

### Your First Source Generator

- Created Project
- Generated HelloWorld.g.cs
- Analyzer Reference Differences
- Visual Studio Experiences
- JetBrains Rider Experiences
- Persisting Generated Files