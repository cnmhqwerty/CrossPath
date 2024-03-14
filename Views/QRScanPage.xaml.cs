namespace CrossPath.Views;

public partial class QRScanPage : ContentPage
{
	public QRScanPage()
	{
		InitializeComponent();
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
}