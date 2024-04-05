using GeolocatorPlugin.Abstractions;
using GeolocatorPlugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;

namespace CrossPath.ViewModels
{
    class UserInfo : IProfile
    {

        public Location location { get; set; } = new Location();
        public string username { get; set; }
        public IList<Pin> pins { get; set; }
    }
}
