using Microsoft.VisualStudio.ProjectSystem.SolutionGeneration;

//new SolutionGenerator().Generate(
//    projectCount: 20,
//    new SdkProjectGenerator("net7.0"),
//    new IProjectModifier[]
//    {
//        new CSharpClassFileCreator(itemCount: 10),
//        new MaximalProjectToProjectReferences(),
//        new PackageReferences("MetadataExtractor", "2.2.0")
//    },
//    @"d:\generated-solutions\maximal-p2p-20");

new SolutionGenerator().Generate(
    @"d:\generated-solutions\parallelism-sequential-20",
    projectCount: 20,
    generator: new SdkProjectGenerator(),
    modifiers: new IProjectModifier[]
    {
        new CSharpClassFileCreator(itemCount: 10),
        new SequentialProjectToProjectReferences(),
        new PackageReference("MetadataExtractor", "2.2.0")
    },
    postSteps: new IPostStep[]
    {
        new DirectoryBuildPropsFile
        {
            Properties =
            {
                { "TargetFramework", "net5.0" }
            }
        }
    });

new SolutionGenerator().Generate(
    @"d:\generated-solutions\parallelism-parallel-20",
    projectCount: 20,
    generator: new SdkProjectGenerator(),
    modifiers: new IProjectModifier[]
    {
        new CSharpClassFileCreator(itemCount: 10),
        new PackageReference("MetadataExtractor", "2.2.0")
    },
    postSteps: new IPostStep[]
    {
        new DirectoryBuildPropsFile
        {
            Properties =
            {
                { "TargetFramework", "net5.0" }
            }
        }
    });
