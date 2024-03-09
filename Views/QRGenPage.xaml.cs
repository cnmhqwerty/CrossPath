using CrossPath.ViewModels;
using QRCoder;

namespace CrossPath.Views;

public partial class QRGenPage : ContentPage
{
    IProfile Data = DependencyService.Get<IProfile>();
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
        String QRText = string.Format("{0}+{1}, {2}", Data.username, Data.location.Latitude, Data.location.Longitude);

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRText, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }
}