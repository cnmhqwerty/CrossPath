using CrossPath.ViewModels;
using Microsoft.Maui.Controls;

namespace CrossPath.Views;

public partial class ProfilePage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public ProfilePage()
	{
        InitializeComponent();

    }

    void OnEntryComplete(object sender, EventArgs e)
    {
        Data.username = ((Entry)sender).Text;
    }
}