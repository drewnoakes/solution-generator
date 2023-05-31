using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public class CSharpClassFileCreator(int itemCount) : IProjectModifier
{
    private readonly int _itemCount = itemCount;

    public void Modify(IProject project, IReadOnlyList<IProject> priorProjects, string solutionPath)
    {
        for (var itemNumber = 1; itemNumber <= _itemCount; itemNumber++)
        {
            var itemFile = $"Class{itemNumber}.cs";
            var itemPath = Path.Combine(solutionPath, project.RelativeProjectPath, itemFile);

            using var stream = new FileStream(itemPath, FileMode.Create, FileAccess.Write, FileShare.Read);
            using var writer = new StreamWriter(stream, Encoding.ASCII);

            GenerateSourceFile(writer, project.ProjectName, itemNumber);
        }
    }

    private static void GenerateSourceFile(StreamWriter writer, string projectName, int itemNumber)
    {
        writer.WriteLine(
            $$"""
            // This is a generated file
            // https://github.com/drewnoakes/solution-generator

            namespace {{projectName}}
            {
                public class Class{{itemNumber}}
                {
                }
            }
            """);
    }
}