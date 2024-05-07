using CrossPath.ViewModels;
using System.Diagnostics;

namespace CrossPath.Views;

public partial class MainPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public MainPage()
    {
        InitializeComponent();
        //File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Connections.txt"));
        //File.Delete(Path.Combine(FileSystem.AppDataDirectory, "MapPins.txt"));
        Data.ReadNameFile();
        Data.ReadPinsFile();
        Data.ReadConnectionsFile();
    }
}