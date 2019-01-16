using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration
{
    public sealed class CSharpProjectGenerator : IProjectGenerator
    {
        private static readonly Guid _csprojLibrary = Guid.Parse("9A19103F-16F7-4668-BE54-9A1E7A4F7556");

        public int ProjectCount { get; set; } = 10;
        public int ItemPerProjectCount { get; set; } = 10;

        public void Generate(string solutionPath, List<(string projectName, string relativeProjectPath, Guid projectType, Guid projectGuid)> projects)
        {
            for (var projectNumber = 1; projectNumber <= ProjectCount; projectNumber++)
            {
                var projectName = $"Project{projectNumber}";
                var projectPath = Path.Combine(solutionPath, projectName);
                var projectFileName = $"{projectName}.csproj";

                projects.Add((projectName, Path.Combine(projectName, projectFileName), _csprojLibrary, Guid.NewGuid()));

                Directory.CreateDirectory(projectPath);

                using (var stream = new FileStream(Path.Combine(projectPath, projectFileName), FileMode.Create, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(stream, Encoding.ASCII))
                {
                    GenerateProjectFile(writer, projectName);
                }

                for (var itemNumber = 1; itemNumber <= ItemPerProjectCount; itemNumber++)
                {
                    var itemFile = $"Class{itemNumber}.cs";
                    var itemPath = Path.Combine(projectPath, itemFile);

                    using (var stream = new FileStream(itemPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (var writer = new StreamWriter(stream, Encoding.ASCII))
                    {
                        GenerateSourceFile(writer, projectName, itemNumber);
                    }
                }
            }
        }

        private static void GenerateProjectFile(StreamWriter writer, string projectName)
        {
            writer.WriteLine($@"<Project Sdk=""Microsoft.NET.Sdk"">

  <!-- This is a generated file -->
  <!-- https://github.com/drewnoakes/solution-generator -->

  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <AssemblyName>{projectName}</AssemblyName>
  </PropertyGroup>

</Project>
");
        }

        private static void GenerateSourceFile(StreamWriter writer, string projectName, int itemNumber)
        {
            writer.WriteLine($@"// This is a generated file
// https://github.com/drewnoakes/solution-generator

namespace {projectName}
{{
    public class Class{itemNumber}
    {{
    }}
}}");
        }
    }
}