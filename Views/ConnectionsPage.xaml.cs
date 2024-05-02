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
}