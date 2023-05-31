using System;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public interface IProject
{
    string ProjectName { get; }
    string RelativeProjectFilePath { get; }
    string RelativeProjectPath { get; }
    Guid ProjectType { get; }
    Guid ProjectGuid { get; }
    XDocument ProjectXml { get; }

    void AddProperty(XElement property);

    void AddItem(XElement item);
}