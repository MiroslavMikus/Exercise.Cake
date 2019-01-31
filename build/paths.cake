public static class Paths
{
    public static FilePath SolutionPath => "Exercise.Cake.sln";
    public static FilePath CodeCoverageResultFile => $"{ReportDirectory}/coverage.xml";
    public static string ReportDirectory => "report";
    public static FilePath NuspecFile => "Exercise.Cake/Exercise.Cake.nuspec";
}

public static FilePath Combine(DirectoryPath directory, FilePath file)
{
    return directory.CombineWithFilePath(file);
}