

namespace FileExplorerApp;

public class FileExplorer
{

    public event EventHandler<FileEventArgs>? FileFound;
    public event EventHandler? ExploreFinished;

    private bool mStopRequired = false;

    private StopImpl mStoppable;
    public FileExplorer() 
    { 
        mStoppable = new StopImpl(this);
    }


    public void Explore(string dirPath)
    {
        ExploreDir(dirPath);
        ExploreFinished?.Invoke(this, new EventArgs());
        mStopRequired = false;
    }

    private void ExploreDir(string dirPath)
    {
        foreach (string f in Directory.GetFiles(dirPath))
        {
            if (mStopRequired) return;
            FileFound?.Invoke(this, new FileEventArgs(f, mStoppable));
        }

        foreach (string d in Directory.GetDirectories(dirPath))
        {
            ExploreDir(d);
        }

    }

    internal class StopImpl : IStoppable
    {
        private readonly FileExplorer mOwner;
        public StopImpl(FileExplorer owner)
        {
            mOwner = owner;
        }

        public void Stop()
        {
            mOwner.mStopRequired = true;
        }
    }


}