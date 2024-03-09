using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrossPath.ViewModels
{
    public interface IProfile
    {
        public string username { get; set; }

        public Location location { get; set; }
    }
}
