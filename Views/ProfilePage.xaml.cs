using CrossPath.ViewModels;
using System.Collections.ObjectModel;
using Path = System.IO.Path;

namespace CrossPath.Views;

public partial class ProfilePage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();
    public class Interest
    {
        public Interest(string name, bool isChecked)
        {
            this.Name = name;
            this.IsChecked = isChecked;
        }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    };


    private ObservableCollection<Interest> interestsCollection;

    public ObservableCollection<Interest> InterestsCollection
    {
        get { return interestsCollection; }
        set { interestsCollection = value; }
    }


    public ProfilePage()
    {
        InitializeComponent();
        InterestsCollection = new()
        {
            new Interest("Books", false),
            new Interest("Games", false),
            new Interest("Animals", false),
            new Interest("Music", false),
            new Interest("Anime", false),
            new Interest("Movies", false)
        };

        InterestList.ItemsSource = InterestsCollection;
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

    private void OnCheckBoxCheckedChanged(object sender, EventArgs e)
    {
    }
}