using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.Controls.Maps;
using System.Diagnostics;
using Microsoft.Maui.Maps;
using GeolocatorPlugin.Abstractions;
using GeolocatorPlugin;
using CrossPath.ViewModels;

namespace CrossPath.Views;

public partial class MapPage : ContentPage
{

    IProfile Data = DependencyService.Get<IProfile>();
    public MapPage()
    {
        InitializeComponent();

        Position pos = GetCurrentPosition().Result;
        Data.location = new Location(pos.Latitude, pos.Longitude);
        MapSpan mapSpan = new MapSpan(Data.location, 0.1, 0.1);

        Map map = new Map
        {
            IsShowingUser = true
        };

        Content = map;
        map.MoveToRegion(mapSpan);

        Pin pin = new Pin
        {
            Label = "Santa Cruz",
            Address = "The city with a boardwalk",
            Type = PinType.Place,
            Location = new Location(36.9628066, -122.0194722)
        };
        map.Pins.Add(pin);
    }
    public static async Task<Position> GetCurrentPosition()
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
}