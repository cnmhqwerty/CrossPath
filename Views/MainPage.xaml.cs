using CrossPath.ViewModels;
using System.Diagnostics;

namespace CrossPath.Views;

public partial class MainPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public MainPage()
    {
        InitializeComponent();
        //File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Username.txt"));
        //File.Delete(Path.Combine(FileSystem.AppDataDirectory, "MapPins.txt"));
        Data.ReadPinsFile();
        Data.ReadConnectionsFile();
    }
}