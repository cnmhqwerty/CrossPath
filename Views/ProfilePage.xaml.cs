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
}