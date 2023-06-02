using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public class DirectoryBuildPropsFile : IPostStep
{
    public Dictionary<string, string> Properties { get; } = new();

    public void Go(string solutionPath)
    {
        var doc = new XDocument(
            new XElement("Project",
                new XElement("PropertyGroup",
                    Properties.Select(pair => new XElement(pair.Key, pair.Value)))));

        doc.Save(Path.Join(solutionPath, "Directory.Build.props"));
    }
}
