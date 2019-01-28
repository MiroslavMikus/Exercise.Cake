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
    NuGetRestore(@"**\*.sln");
});

Task("Compile")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(()=>{
        MSBuild(@"**\*.sln");
    });

Task("Test")
    .IsDependentOn("Compile")
    .Does(()=>{
        MSTest(@"**\bin\**\*.Test.dll");
    });

RunTarget(target);