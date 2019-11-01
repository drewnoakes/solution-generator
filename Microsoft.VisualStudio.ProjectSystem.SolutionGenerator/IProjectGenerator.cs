namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration
{
    public interface IProjectGenerator
    {
        IProject Generate(int projectIndex);
    }
}