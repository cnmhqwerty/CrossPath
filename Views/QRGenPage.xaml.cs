using CrossPath.ViewModels;
using GeolocatorPlugin.Abstractions;
using QRCoder;

namespace CrossPath.Views;

public partial class QRGenPage : ContentPage
{
    readonly IProfile Data = DependencyService.Get<IProfile>();
    public QRGenPage()
	{
		InitializeComponent();
        GenQR();
    }
    private void OnGenerateClicked(object sender, EventArgs e) 
    {
        GenQR();
    }

    private void GenQR()
    {        
        Position pos = Data.GetCurrentPosition().Result;
        Data.Location = new Location(pos.Latitude, pos.Longitude);
        String QRText = string.Format("{0}, {1}, {2}", Data.Username, pos.Latitude, pos.Longitude);

        QRCodeGenerator qrGenerator = new();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRText, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }


    async void OnScanClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QRScanPage(), true);
    }
}