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
        string interests="";
        foreach (var item in Data.InterestsCollection)
        {
            if (item.IsChecked == true)
            {
                interests = interests + "1";
            }
            else
            {
                interests = interests + "0";
            }
        }
        string QRText = string.Format("{0}, {1}, {2}, {3}", Data.Username, pos.Latitude, pos.Longitude, interests);

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