using System;
using System.Collections.Generic;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration
{
    public interface IProjectGenerator
    {
        void Generate(string solutionPath, List<(string projectName, string relativeProjectPath, Guid projectType, Guid projectGuid)> projects);
    }
}