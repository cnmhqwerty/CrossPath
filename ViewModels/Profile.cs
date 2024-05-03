using GeolocatorPlugin.Abstractions;
using GeolocatorPlugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace CrossPath.ViewModels
{
    public interface IProfile
    {
        public string Username { get; set; }

        public Location Location { get; set; }

        public ObservableCollection<Pin> MapPins { get; set; }
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
        public ObservableCollection<Interest> InterestsCollection { get; set;}

        public class Connection
        {
            public Connection(string name, string interests)
            {
                this.Name=name;
                this.Interests = interests;
            }
            public string Name { get; set; }
            public string Interests { get; set; }
        }

        public ObservableCollection<Connection> ConnectionsCollection { get; set;}


    public async Task<Position> GetCurrentPosition()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            Debug.WriteLine(output);

            return position;
        }

        public void ReadPinsFile()
        {
            string _fileName = Path.Combine(FileSystem.AppDataDirectory, "MapPins.txt");
            if (File.Exists(_fileName))
            {
                //separate file data into each pin
                string temp;
                temp = File.ReadAllText(_fileName);
                string[] fileStrings = temp.Split(";", StringSplitOptions.RemoveEmptyEntries);
                //add these pins to the collection
                for (int i = 0; i < fileStrings.Length; i++)
                {
                    Pin pin = new Pin
                    {
                        Label = fileStrings[i].Split(",", StringSplitOptions.TrimEntries)[0],
                        //Address = s.Split(",", StringSplitOptions.TrimEntries)[3],
                        Type = PinType.Place,
                        Location = new Location(Convert.ToDouble(fileStrings[i].Split(",", StringSplitOptions.TrimEntries)[1]), Convert.ToDouble(fileStrings[i].Split(",", StringSplitOptions.TrimEntries)[2]))
                    };

                    MapPins.Add(pin);
                }
            }
        }

        public void ReadConnectionsFile()
        {
            string _fileName = Path.Combine(FileSystem.AppDataDirectory, "Connections.txt");
            if (File.Exists(_fileName))
            {
                //separate file data into each connection
                string temp = File.ReadAllText(_fileName);
                Debug.WriteLine(Path.Combine(FileSystem.AppDataDirectory, "Connections.txt"));
                string interests = "Their Interests: ";
                string[] fileStrings = temp.Split(";", StringSplitOptions.RemoveEmptyEntries);
                //add these connections to the collection
                for (int i = 0; i < fileStrings.Length; i++)
                {
                    string tempInterests = fileStrings[0].Split(",", StringSplitOptions.TrimEntries)[3];
                    char[] iter = tempInterests.ToCharArray();
                    for (int t = 0; t < iter.Length; t++)
                    {
                        if (iter[t] == '1')
                        {
                            interests = interests + InterestsCollection[t].Name + ", ";
                        }
                    }

                    ConnectionsCollection.Add(new IProfile.Connection(fileStrings[0].Split(",", StringSplitOptions.TrimEntries)[0], interests));
                }
            }
        }
    }
}
