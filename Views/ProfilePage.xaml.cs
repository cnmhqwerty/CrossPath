using CrossPath.ViewModels;
using Path = System.IO.Path;

namespace CrossPath.Views;

public partial class ProfilePage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();

    public ProfilePage()
    {
        InitializeComponent();
        InterestList.ItemsSource = Data.InterestsCollection;
    }

    void OnEntryComplete(object sender, EventArgs e)
    {
        Data.Username = ((Entry)sender).Text;
    }

    private async void OnPfPClicked(object sender, EventArgs e)
    {
        FileResult pfp = await MediaPicker.Default.PickPhotoAsync();
        if (pfp != null)
        {
            string filePath = Path.Combine(FileSystem.CacheDirectory, pfp.FileName);
            using Stream sourceStream = await pfp.OpenReadAsync();
            using FileStream fileStream = File.OpenWrite(filePath);
            await sourceStream.CopyToAsync(fileStream);
            pfpImage.Source = fileStream.Name;
        }
    }
}