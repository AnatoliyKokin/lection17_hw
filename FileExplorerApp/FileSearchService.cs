namespace FileExplorerApp;

using System.IO;

public class FileSearchService
{
    private readonly FileExplorer mFileExplorer;
    private string mSearchPath = string.Empty;
    private bool mSearchResult = false;
    private bool mSearchFinished = false;

    public FileSearchService(FileExplorer explorer)
    {
        mFileExplorer = explorer;
    }

    public Task<bool> Find(string dirPath, string fileName)
    {
        
        return Task.Run(() =>
        {
            
            mSearchPath = fileName;
            mSearchResult = false;
            mSearchFinished = false;

            mFileExplorer.FileFound += OnFileFound;
            mFileExplorer.ExploreFinished += OnExploreFinished;

            mFileExplorer.Explore(dirPath);
            
            while(!mSearchFinished);
            
            return mSearchResult;
        });
    }


    private void OnFileFound(object? sender, FileEventArgs args)
    {
        Console.WriteLine(args.Path);
        if (Path.GetFileName(args.Path) == mSearchPath)
        {
            mSearchResult = true;
            args.Stoppable?.Stop();
            
        }

    }

    private void OnExploreFinished(object? sender, EventArgs args)
    {
        mFileExplorer.ExploreFinished -= OnExploreFinished;
        mFileExplorer.FileFound -= OnFileFound;
        mSearchFinished = true;
    }


}