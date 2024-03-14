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

        Position pos = Data.GetCurrentPosition().Result;
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
}