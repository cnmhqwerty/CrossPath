using CrossPath.ViewModels; 
using Microsoft.Maui.Controls.Shapes;
using Path = System.IO.Path;

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
        Data.Username = ((Entry)sender).Text;
    }

    private async void OnPfPClicked(object sender, EventArgs e)
    {
        FileResult pfp = await MediaPicker.Default.PickPhotoAsync();
        if( pfp != null)
        {
            string filePath = Path.Combine(FileSystem.CacheDirectory, pfp.FileName);
            using Stream sourceStream = await pfp.OpenReadAsync();
            using FileStream fileStream = File.OpenWrite(filePath);
            await sourceStream.CopyToAsync(fileStream);
            pfpImage.Source = fileStream.Name;
            var clip1 = new RectangleGeometry(new Rect(0, 0, DeviceDisplay.MainDisplayInfo.Width/2, DeviceDisplay.MainDisplayInfo.Width/2));
            pfpImage.Clip = clip1;
        }
    }
}