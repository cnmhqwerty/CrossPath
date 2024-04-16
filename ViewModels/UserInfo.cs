using GeolocatorPlugin.Abstractions;
using GeolocatorPlugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;

namespace CrossPath.ViewModels
{
    class UserInfo : IProfile
    {

        public Location Location { get; set; } = new Location();
        public string Username { get; set; }
        public ObservableCollection<Pin> MapPins { get; set; } = new ObservableCollection<Pin>();

    }
}
