using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public class PackageReference(string id, string version) : IProjectModifier
{
    public void Modify(IProject project, IReadOnlyList<IProject> priorProjects, string solutionPath)
    {
        project.AddItem(
            new XElement(
                "PackageReference",
                new XAttribute("Include", id),
                new XAttribute("Version", version)));
    }
}