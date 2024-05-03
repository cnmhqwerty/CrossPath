using CrossPath.ViewModels;
using System.Diagnostics;

namespace CrossPath.Views;

public partial class MainPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public MainPage()
    {
        InitializeComponent();
        File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "connections.txt"), "testing 124, 53.4739427, -2.2932821, 101010");
        using (StreamWriter sw = File.AppendText(Path.Combine(FileSystem.AppDataDirectory, "MapPins.txt")))
        {
            sw.WriteLine("testing 124, 53.4739427, -2.2932821, 101010");
        }
        Data.ReadPinsFile();
        Data.ReadConnectionsFile();
    }
}