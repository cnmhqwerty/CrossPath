using CrossPath.ViewModels;
using System.Diagnostics;

namespace CrossPath.Views;

public partial class MainPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public MainPage()
    {
        InitializeComponent();
        Debug.WriteLine(Path.Combine(FileSystem.AppDataDirectory, "Connections.txt"));
        File.AppendText(Path.Combine(FileSystem.AppDataDirectory, "MapPins.txt")).WriteLine("testing 124, 53.4739427, -2.2932821, 101010");
        Data.ReadPinsFile();
        Data.ReadConnectionsFile();
    }
}