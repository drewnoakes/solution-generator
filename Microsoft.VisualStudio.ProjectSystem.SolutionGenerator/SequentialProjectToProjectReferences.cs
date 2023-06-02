using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public class SequentialProjectToProjectReferences : IProjectModifier
{
    public void Modify(IProject project, IReadOnlyList<IProject> priorProjects, string solutionPath)
    {
        if (priorProjects.Count == 0)
            return;

        IProject priorProject = priorProjects[^1];

        if (priorProject is not null)
        {
            project.AddItem(new XElement("ProjectReference",
                new XAttribute("Include",
                    $"..\\{priorProject.RelativeProjectFilePath}")));
        }
    }
}