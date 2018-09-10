using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration
{
    public sealed class SolutionGenerator
    {
        public void Generate(IEnumerable<IProjectGenerator> generators, string solutionPath)
        {
            var projects = new List<(string projectName, string relativeProjectPath, Guid projectType, Guid projectGuid)>();

            foreach (var generator in generators)
            {
                generator.Generate(solutionPath, projects);
            }

            using (var stream = File.OpenWrite(Path.Combine(solutionPath, $"{Path.GetFileName(solutionPath)}.sln")))
            using (var writer = new StreamWriter(stream, Encoding.ASCII))
            {
                writer.WriteLine();
                writer.WriteLine(@"Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 15
VisualStudioVersion = 15.0.28010.2019
MinimumVisualStudioVersion = 10.0.40219.1");

                foreach ((string projectName, string relativeProjectPath, Guid projectType, Guid projectGuid) in projects)
                {
                    writer.WriteLine(
$@"Project(""{projectType:B}"") = ""{projectName}"", ""{relativeProjectPath}"", ""{projectGuid:B}""
EndProject");
                }

                writer.WriteLine(
@"Global
    GlobalSection(SolutionConfigurationPlatforms) = preSolution
        Debug|Any CPU = Debug|Any CPU
        Release|Any CPU = Release|Any CPU
    EndGlobalSection
    GlobalSection(ProjectConfigurationPlatforms) = postSolution");
                foreach ((_, _, _, Guid projectGuid) in projects)
                {
                    writer.WriteLine(
$@"        {projectGuid:B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
        {projectGuid:B}.Debug|Any CPU.Build.0 = Debug|Any CPU
        {projectGuid:B}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {projectGuid:B}.Release|Any CPU.Build.0 = Release|Any CPU");
                }
                                writer.WriteLine(
$@"    EndGlobalSection
    GlobalSection(SolutionProperties) = preSolution
        HideSolutionNode = FALSE
    EndGlobalSection
    GlobalSection(ExtensibilityGlobals) = postSolution
        SolutionGuid = {Guid.NewGuid():B}
    EndGlobalSection
EndGlobal");
            }
        }
    }
}
