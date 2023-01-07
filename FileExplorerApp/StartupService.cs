namespace FileExplorerApp;
using Microsoft.Extensions.DependencyInjection;

public class StartupService
{
    private readonly IServiceProvider mServiceProvider;
    public StartupService(IServiceProvider serviceProvider)
    {
        mServiceProvider = serviceProvider;
    }
    public async Task Run(AppOptions options)
    {
        if (options.SearchPath != null)
        {
            bool result = await mServiceProvider.GetRequiredService<FileSearchService>().
            Find(options.DirPath ?? Directory.GetCurrentDirectory(), (string)options.SearchPath);
            Console.WriteLine(result ? "Found" : "Not Found");
            return;
        }
        var info = await mServiceProvider.GetRequiredService<MaxFileService>().
        MaxFile(options.DirPath ?? Directory.GetCurrentDirectory());
        if (info!=null)
        {
            Console.WriteLine("MaxSize: " + info.Length + "(File:" + info.FullName + ")");
        }
    }




}