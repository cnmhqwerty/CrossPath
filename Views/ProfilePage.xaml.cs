using CrossPath.ViewModels;
using System.Diagnostics;
using Path = System.IO.Path;

namespace CrossPath.Views;

public partial class ProfilePage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public ProfilePage()
    {
        InitializeComponent();
        Data.ReadProfileFile();

        UsernameEntry.Text = Data.Username;
        InterestList.ItemsSource = Data.InterestsCollection;
    }

    void OnEntryComplete(object sender, EventArgs e)
    {
        string interests = "";
        foreach (var item in Data.InterestsCollection)
        {
            if (item.IsChecked == true)
            {
                interests = interests + "1";
            }
            else
            {
                interests = interests + "0";
            }
        }
        File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Username.txt"));
        using (StreamWriter sw = File.CreateText(Path.Combine(FileSystem.AppDataDirectory, "Username.txt")))
        {
            sw.WriteLine("{0};{1};{2}", UsernameEntry.Text, interests, pfpImage.Source);
        }
        Data.Username = UsernameEntry.Text;
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
            
            string interests = "";
            foreach (var item in Data.InterestsCollection)
            {
                if (item.IsChecked == true)
                {
                    interests = interests + "1";
                }
                else
                {
                    interests = interests + "0";
                }
            }
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, "Username.txt"));
                using (StreamWriter sw = File.CreateText(Path.Combine(FileSystem.AppDataDirectory, "Username.txt")))
                {
                    sw.WriteLine("{0};{1};{2}", UsernameEntry.Text, interests, pfp.FileName);
                }
            
        }
    }

    //private void UsernameBtn_Clicked(object sender, EventArgs e)
    //{
    //    UsernameEntry.IsVisible = true;
    //    UsernameBtn.IsVisible = false;
    //}
}