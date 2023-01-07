using System.IO;

namespace FileExplorerApp;


public class FileEventArgs : EventArgs
{
    public FileEventArgs(string path)
    {
        Path = path;
    }

    public FileEventArgs(string path, IStoppable stoppable)
    {
        Path = path;
        Stoppable = stoppable;
    }


    public string Path { get; }

    public IStoppable? Stoppable { get; }
}