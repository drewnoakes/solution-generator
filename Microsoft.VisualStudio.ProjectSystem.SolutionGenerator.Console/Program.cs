namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration.Console
{
    // TODO consider dotnet cli tool

    internal static class Program
    {
        public static void Main()
        {
            new SolutionGenerator().Generate(
                new[]
                {
                    new CSharpProjectGenerator
                    {
                        ProjectCount = 20,
                        ItemPerProjectCount = 20
                    }
                },
                @"c:\Users\drew\dev\ms\generated-solutions\Solution1");
        }
    }
}
