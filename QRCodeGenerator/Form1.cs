using QRCoder;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QRCodeGenerator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var qrGenerator = new QRCoder.QRCodeGenerator();
			QRCodeData qrCodeData = qrGenerator.CreateQrCode(textBox1.Text.Trim(), QRCoder.QRCodeGenerator.ECCLevel.Q);
			QRCode qrCode = new QRCode(qrCodeData);
			Bitmap qrCodeImage = qrCode.GetGraphic(20);

			//var options = new QrCodeEncodingOptions
			//{
			//	DisableECI = true,
			//	CharacterSet = "UTF-8",
			//	Width = 500,
			//	Height = 500,
			//};
			//var writer = new BarcodeWriter();
			//writer.Format = BarcodeFormat.QR_CODE;
			//writer.Options = options;

			//var qr = new ZXing.BarcodeWriter();
			//qr.Options = options;
			//qr.Format = ZXing.BarcodeFormat.QR_CODE;
			//var result = new Bitmap(qr.Write(textBox1.Text.Trim()));
			pictureBox1.Image = qrCodeImage;
			pictureBox1.Image.Save("qrcode.png");
		}
	}
}
