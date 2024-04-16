using CrossPath.ViewModels;
using Microsoft.Maui.Controls.Maps;

namespace CrossPath.Views;

public partial class QRScanPage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();
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

    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
		MainThread.BeginInvokeOnMainThread(() =>
		{ //breakdown data from barcode convert into map pin using .split and delimiters
			barcodeResult.Text = $"{args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[0]}";
            Pin pin = new Pin
            {
                Label = args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[0],
                Address = "Test Location",
                Type = PinType.Place,
                Location = new Location(Convert.ToDouble(args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[1]), Convert.ToDouble(args.Result[0].Text.Split(",", StringSplitOptions.TrimEntries)[2]))
            };
            Data.MapPins.Add(pin);
        });
    }
}