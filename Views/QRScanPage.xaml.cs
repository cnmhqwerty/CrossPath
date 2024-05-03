using CrossPath.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace CrossPath.Views;

public partial class QRScanPage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();

    Camera.MAUI.ZXingHelper.BarcodeEventArgs args;
    public QRScanPage()
	{
		InitializeComponent();

		cameraView.BarCodeOptions = new()
		{
			PossibleFormats = { ZXing.BarcodeFormat.QR_CODE }
		};
    }

	private void CameraView_CamerasLoaded(object sender, EventArgs e)
	{
		cameraView.Camera = cameraView.Cameras.First();

		MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
		});
	}

    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs tempargs)
    {
        args = tempargs;
    }
    private void ScanBtn_Clicked(object sender, EventArgs e)
    {
        if (args != null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            { //breakdown data from barcode convert into map pin using .split and delimiters
                Pin pin = new Pin
                {
                    Label = args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[0],
                    Address = args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[3],
                    Type = PinType.Place,
                    Location = new Location(Convert.ToDouble(args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[1]), Convert.ToDouble(args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[2]))
                };
                Data.MapPins.Add(pin);
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(Path.Combine(FileSystem.AppDataDirectory, "MapPins.txt")))
                {
                    sw.WriteLine("{0};", args.Result[0]);
                }

                //breakdown interests bool into data for display.
                string interests = "Their Interests: ";
                string tempInterests = args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[3];
                char[] temp = tempInterests.ToCharArray();
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == '1')
                    {
                        interests = interests + Data.InterestsCollection[i].Name + ", ";
                    }
                }
                Data.ConnectionsCollection.Add(new IProfile.Connection(args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[0], interests));


                using (StreamWriter sw = File.AppendText(Path.Combine(FileSystem.AppDataDirectory, "Connections.txt")))
                {
                    sw.WriteLine("{0},{1};", args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[0], tempInterests);
                }
            });
        }
    }
}