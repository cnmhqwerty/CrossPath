using CrossPath.ViewModels;
using Microsoft.Maui.Controls;

namespace CrossPath.Views;

public partial class ConnectionsPage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public ConnectionsPage()
	{
        InitializeComponent();
        ConnectionsList.ItemsSource = Data.ConnectionsCollection;
    }

    private void DelBtn_Clicked(object sender, EventArgs e)
    {
        File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Connections.txt"));
        File.Delete(Path.Combine(FileSystem.AppDataDirectory, "MapPins.txt"));
        Data.ConnectionsCollection.Clear();
        Data.MapPins.Clear();
    }
}