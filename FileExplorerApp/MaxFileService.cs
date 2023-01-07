namespace FileExplorerApp;

using Extensions;

public class MaxFileService
{
    private readonly FileExplorer mFileExplorer;
    private List<FileInfo> mInfos = new List<FileInfo>();
    private bool mFinished = false;
    public MaxFileService(FileExplorer explorer)
    {
        mFileExplorer = explorer;
    }

    public Task<FileInfo?> MaxFile(string path)
    {
        return Task.Run(() =>
        {
            mFileExplorer.FileFound += OnFileFound;
            mFileExplorer.ExploreFinished += OnExploreFinished;
            mFinished = false;

            mFileExplorer.Explore(Directory.GetCurrentDirectory());
            while (!mFinished) ;

            if (mInfos.Count > 0)
            {
                var info = mInfos.GetMax(info => { return (float)info.Length; });
                mInfos.Clear();
                return info;
            }
            return null;
        });
    }

    private void OnFileFound(object? sender, FileEventArgs args)
    {
        Console.WriteLine(args.Path);
        mInfos.Add(new FileInfo(args.Path));

    }

    private void OnExploreFinished(object? sender, EventArgs args)
    {
        mFinished = true;
        mFileExplorer.ExploreFinished -= OnExploreFinished;
        mFileExplorer.FileFound -= OnFileFound;
    }


}