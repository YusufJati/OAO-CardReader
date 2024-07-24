using System.Drawing;

namespace WpfApplication1.Utils
{
    public class Settings
    {
        public static Font BackSettings = new Font("ARIAL", 5f, FontStyle.Regular);
        public static Point BarcodeLocation = new Point(180, 70);
        public static Size BarcodeSize = new Size(684, 250);
        public static Point CardBackgroundLocation = new Point(0, 0);
        public static Size CardBackgroundSize = new Size(1029, 660);
        public static string CardFrontImage = "ClientFront.png";
        public static int CardSide = 1;
        public static Font Header1Settings = new Font("MoolBoran", 16f, FontStyle.Regular);
        public static Font Header2Settings = new Font("Calibri", 7f, FontStyle.Bold);
        public static Point HeaderLocation = new Point(0, 0);
        public static Size HeaderSize = new Size(1000, 100);
        public static Color LabelColor = Color.Blue;
        public static Font LabelSettings = new Font("Calibri", 7f, FontStyle.Regular);
        public static Point LogoLocation = new Point(20, 20);
        public static Size LogoSize = new Size(120, 120);
        public static Font MRZSettings = new Font("OCR-B 10 BT", 12f, FontStyle.Regular);
        public static Point PhotoLocation = new Point(725, 120);
        public static Size PhotoSize = new Size(200, 252);
        public static bool PrintBarCode = false;
        public static string Printer = "HDP5000 Card Printer";
        public static bool PrintLogo = false;
        public static bool PrintPhoto = true;
        public static bool PrintSignature = true;
        public static Point SignLocation = new Point(700, 405);
        public static Size SignSize = new Size(280, 75);
        public static int TextAlignment = 0;
        public static Color TextColor = Color.Black;
        public static Point TextLocation = new Point(1030, 170);
        public static Font TextSettings = new Font("Calibri", 7f, FontStyle.Bold);
        public static Font TopSettings = new Font("Times New Roman", 5.5f, FontStyle.Regular);
        public static Font StockItSettings = new Font("OCR-B 10 BT", 8f, FontStyle.Bold);
        public static Size FotoSize = new Size(400, 500);
        public static Point FotoLocation = new Point(750, 130);
        public static Size TtdSize = new Size(500, 180);
        public static Point TtdLocation = new Point(700, 450);
    }
}