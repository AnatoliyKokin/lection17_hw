using FileExplorerApp;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

var parseResult = CommandLine.Parser.Default.ParseArguments<AppOptions>(args);

parseResult.WithParsed<AppOptions>(opts => 
Run(opts));


static void Run(AppOptions options)
{
    var serviceProvider = new ServiceCollection().
    AddTransient(typeof(FileExplorer)).
    AddTransient(typeof(MaxFileService)).
    AddTransient(typeof(FileSearchService)).
    AddTransient(typeof(StartupService)).
    BuildServiceProvider();

    var startService = serviceProvider.GetRequiredService<StartupService>();
    startService.Run(options).Wait();
    //task.Wait();
}
