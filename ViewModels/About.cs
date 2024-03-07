using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPath.ViewModels
{
    class About
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://c-helliwell.carrd.co";
        public string Message => "This app is written in XAML and C# with.NET MAUI." ;
    }
}
