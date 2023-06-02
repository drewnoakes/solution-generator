namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

public interface IPostStep
{
    void Go(string solutionPath);
}
