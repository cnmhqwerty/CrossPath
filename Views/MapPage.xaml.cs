using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using GeolocatorPlugin.Abstractions;
using CrossPath.ViewModels;
using System.Diagnostics;
using System.Collections.Specialized;

namespace CrossPath.Views;

public partial class MapPage : ContentPage
{

    IProfile Data = DependencyService.Get<IProfile>();

    public MapPage()
    {
        InitializeComponent();

        Data.mapPins.CollectionChanged += HandleChange;

        Position pos = Data.GetCurrentPosition().Result;
        Data.location = new Location(pos.Latitude, pos.Longitude);
        MapSpan mapSpan = new MapSpan(Data.location, 0.1, 0.1);

        if (map != null)
        {
            map.MoveToRegion(mapSpan);
        }
    }

    private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
    {
        foreach (Pin x in e.NewItems)
        {
            map.Pins.Add(x);
            Debug.WriteLine(x.Location);
        }
    }
}