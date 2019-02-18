using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.QrCode;
using ZXing;

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
			var options = new QrCodeEncodingOptions
			{
				DisableECI = true,
				CharacterSet = "UTF-8",
				Width = 500,
				Height = 500,
			};
			var writer = new BarcodeWriter();
			writer.Format = BarcodeFormat.QR_CODE;
			writer.Options = options;

			var qr = new ZXing.BarcodeWriter();
			qr.Options = options;
			qr.Format = ZXing.BarcodeFormat.QR_CODE;
			var result = new Bitmap(qr.Write(textBox1.Text.Trim()));
			pictureBox1.Image = result;
			pictureBox1.Image.Save("qrcode.png");
		}
	}
}
