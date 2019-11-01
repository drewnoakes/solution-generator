namespace Microsoft.VisualStudio.ProjectSystem.SolutionGeneration.Console
{
    // TODO consider dotnet cli tool

    internal static class Program
    {
        public static void Main()
        {
            new SolutionGenerator().Generate(
                projectCount: 20,
                new SdkProjectGenerator("netcoreapp2.1"),
                new IProjectModifier[]
                {
                    new CSharpClassFileCreator(itemCount: 10),
                    new MaximalProjectToProjectReferences(),
                    new PackageReferences("MetadataExtractor", "2.2.0")
                },
                @"d:\generated-solutions\maximal-p2p-20");
        }
    }
}
