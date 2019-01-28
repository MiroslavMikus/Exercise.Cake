var target = Argument("target", "Compile");

var buildDir = System.IO.Directory.GetDirectories("./", "Debug", SearchOption.AllDirectories);

Task("Clean")
    .Does(()=>{
        foreach (var dir in buildDir)
        {
            Information($"Cleaning: {dir}");
            CleanDirectory(dir);
        }
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("Exercise.Cake.sln");
});

Task("Compile")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(()=>{
        MSBuild("Exercise.Cake.sln");
    });

Task("Test")
    .IsDependentOn("Compile")
    .Does(()=> {
        VSTest(@"**\bin\**\*.Test.dll", new VSTestSettings
        {
            Parallel = true,
            TestAdapterPath = @"Exercise.Cake.Test\bin\Debug\"
        });
    });

RunTarget(target);