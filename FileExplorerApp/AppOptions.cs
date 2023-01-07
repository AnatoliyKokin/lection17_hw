using CommandLine;

namespace FileExplorerApp;

public class AppOptions
{
    [Option('d', "dir",
        HelpText = "directory path")]
    public string? DirPath { get; set; }

    [Option('f', "find",
        HelpText = "name of file to find")]
    public string? SearchPath { get; set; }

}