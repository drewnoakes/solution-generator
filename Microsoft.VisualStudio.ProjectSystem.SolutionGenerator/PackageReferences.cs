using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration
{
    public class PackageReferences : IProjectModifier
    {
        private readonly string _id;
        private readonly string _version;

        public PackageReferences(string id, string version)
        {
            _id = id;
            _version = version;
        }

        public void Modify(IProject project, IReadOnlyList<IProject> priorProjects, string solutionPath)
        {
            foreach (var priorProject in priorProjects)
            {
                project.AddItem(new XElement("PackageReference",
                    new XAttribute("Include", _id),
                    new XAttribute("Version", _version)));
            }
        }
    }
}