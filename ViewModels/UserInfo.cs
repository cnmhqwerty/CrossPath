using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;

namespace CrossPath.ViewModels
{
    class UserInfo : IProfile
    {

        public Location Location { get; set; } = new Location();
        public string Username { get; set; }
        public ObservableCollection<Pin> MapPins { get; set; } = new ObservableCollection<Pin>();
        public ObservableCollection<IProfile.Interest> InterestsCollection { get; set; } = new ObservableCollection<IProfile.Interest> {
            new IProfile.Interest("Books", false),
            new IProfile.Interest("Games", false),
            new IProfile.Interest("Animals", false),
            new IProfile.Interest("Music", false),
            new IProfile.Interest("Anime", false),
            new IProfile.Interest("Movies", false)
        };
        public ObservableCollection<IProfile.Connection> ConnectionsCollection { get; set; } = new ObservableCollection<IProfile.Connection>();
    }
}
