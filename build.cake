#tool "nuget:?package=Microsoft.TestPlatform&version=15.7.0"
#load "build/paths.cake"


///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var target = Argument("target", "Compile");
var configuration = Argument("configuration", "Debug");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("temp")
    .Does(()=>{
        var path = Context.Tools.Resolve("vstest.console.exe");
        Information(path);
    });

Task("Clean")
    .Does(()=>{
        foreach (var dir in GetDirectories("**/Debug"))
        {
            Information($"Cleaning: {dir}");
            CleanDirectory(dir);
        }
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    // NuGetRestore("**\*.sln");

    foreach(var file in GetFiles("*.sln"))
    {
        Information("Nuget-restore: {0}", file);
        NuGetRestore(GetFiles(file.ToString()));
    }
});

Task("Generic-Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(()=>{
        foreach(var file in GetFiles("*.sln"))
        {
            Information("Compiling: {0}", file);
            MSBuild(file.ToString(), settings => settings.SetConfiguration(configuration));
        }
    });
    
Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(()=>{
        MSBuild(Paths.SolutionPath.ToString(), settings => settings.SetConfiguration(configuration));
    });
    
Task("Test")
    .IsDependentOn("Build")
    .Does(()=> {
        VSTest($"**/bin/{configuration}/*.Test.dll", new VSTestSettings
        {
            Parallel = true,
            TestAdapterPath = @"Exercise.Cake.Test\bin\Debug\"
        });
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("Test");

RunTarget(target); 