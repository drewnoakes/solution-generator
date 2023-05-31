using System.Collections.Generic;

namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public interface IProjectModifier
{
    void Modify(IProject project, IReadOnlyList<IProject> priorProjects, string solutionPath);
}