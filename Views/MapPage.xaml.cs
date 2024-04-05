using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using GeolocatorPlugin.Abstractions;
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
            Label = "Santa Cruz new",
            Address = "The city with a boardwalk",
            Type = PinType.Place,
            Location = new Location(36.9628066, -122.0194722)
        };

        //foreach (Pin p in Data.pins)
        //{
        //    map.Pins.Add(p);
        //};
    }
}