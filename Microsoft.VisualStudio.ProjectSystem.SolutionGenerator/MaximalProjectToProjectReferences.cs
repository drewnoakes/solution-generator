using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public class MaximalProjectToProjectReferences : IProjectModifier
{
    public void Modify(IProject project, IReadOnlyList<IProject> priorProjects, string solutionPath)
    {
        foreach (var priorProject in priorProjects)
        {
            project.AddItem(new XElement("ProjectReference",
                new XAttribute("Include",
                    $"..\\{priorProject.RelativeProjectFilePath}")));
        }
    }
}