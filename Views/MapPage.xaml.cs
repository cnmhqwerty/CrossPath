using Microsoft.Maui.Maps;

namespace CrossPath.Views;

public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();

        var location = new Location(53.47334804386584, -2.297137864535083);
        var mapSpan = new MapSpan(location, 0.01, 0.01);

        map.MoveToRegion(mapSpan);
    }
}