using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public sealed class SolutionGenerator
{
    public void Generate(int projectCount, IProjectGenerator generator, IEnumerable<IProjectModifier> modifiers, string solutionPath)
    {
        var projects = new List<IProject>();

        for (var i = 0; i < projectCount; i++)
        {
            var project = generator.Generate(i);

            Directory.CreateDirectory(Path.Combine(solutionPath, project.RelativeProjectPath));

            foreach (var modifier in modifiers)
            {
                modifier.Modify(project, projects, solutionPath);
            }

            using var stream = new FileStream(Path.Combine(solutionPath, project.RelativeProjectFilePath), FileMode.Create);
            using var writer = XmlWriter.Create(stream, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, IndentChars = "  " });
            project.ProjectXml.Save(writer);

            projects.Add(project);
        }

        WriteSolutionFile();

        void WriteSolutionFile()
        {
            using var stream = File.OpenWrite(Path.Combine(solutionPath, $"{Path.GetFileName(solutionPath)}.sln"));
            using var writer = new StreamWriter(stream, Encoding.ASCII);

            writer.WriteLine();
            writer.WriteLine(
                """
                Microsoft Visual Studio Solution File, Format Version 12.00
                # Visual Studio 15
                VisualStudioVersion = 15.0.28010.2019
                MinimumVisualStudioVersion = 10.0.40219.1
                """);

            foreach (IProject project in projects)
            {
                writer.WriteLine(
                    $"""
                    Project("{project.ProjectType:B}") = "{project.ProjectName}", "{project.RelativeProjectFilePath}", "{project.ProjectGuid:B}"
                    EndProject
                    """);
            }

            writer.WriteLine(
                """
                Global
                    GlobalSection(SolutionConfigurationPlatforms) = preSolution
                        Debug|Any CPU = Debug|Any CPU
                        Release|Any CPU = Release|Any CPU
                    EndGlobalSection
                    GlobalSection(ProjectConfigurationPlatforms) = postSolution
                """);
            foreach (var project in projects)
            {
                writer.WriteLine(
                    $"""
                        {project.ProjectGuid:B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
                        {project.ProjectGuid:B}.Debug|Any CPU.Build.0 = Debug|Any CPU
                        {project.ProjectGuid:B}.Release|Any CPU.ActiveCfg = Release|Any CPU
                        {project.ProjectGuid:B}.Release|Any CPU.Build.0 = Release|Any CPU");
                        {project.ProjectGuid:B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
                        {project.ProjectGuid:B}.Debug|Any CPU.Build.0 = Debug|Any CPU
                        {project.ProjectGuid:B}.Release|Any CPU.ActiveCfg = Release|Any CPU
                        {project.ProjectGuid:B}.Release|Any CPU.Build.0 = Release|Any CPU"
                    """);
            }

            writer.WriteLine(
                $"""
                    EndGlobalSection
                    GlobalSection(SolutionProperties) = preSolution
                        HideSolutionNode = FALSE
                    EndGlobalSection
                    GlobalSection(ExtensibilityGlobals) = postSolution
                        SolutionGuid = {Guid.NewGuid():B}
                    EndGlobalSection
                EndGlobal
                """);
        }
    }
}
