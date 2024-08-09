using iTextSharp.text;
using iTextSharp.text.pdf;
using libComm;
//using Microsoft.CSharp.RuntimeBinder;
//using Microsoft.Office.Interop.Excel;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApplication1.Tables;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCBacaKTP : UserControl
    {
        private string constr = KSEIApp.Properties.Settings.Default.DBKTPLocalConn;
        private string connectionstring = "Data Source=data.sqlite;Version=3;";
        private SQLiteConnection conn;
        private SqlCeConnection sqc;
        private DBKTPContext dbc;
        private DataEktp data = (DataEktp) null;
        private UtilImageText util = new UtilImageText();
        private UtilLog log = new UtilLog();
        private List<MD_KTP> ListKTP = new List<MD_KTP>();
        private static int i = 0;
        private WpfApplication1.Utils.UtilFile utilFile = new WpfApplication1.Utils.UtilFile();
        public UCBacaKTP()
        {
            try
            {
                conn = new SQLiteConnection(connectionstring);
                conn.Open();
                dbc = DBContextSingleton.Instance;
                // print data dengan id terbaru dari tbl_ktp table
                // using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM tbl_ktp ORDER BY id DESC LIMIT 1", conn))
                // {
                //     using (SQLiteDataReader reader = command.ExecuteReader())
                //     {
                //         while (reader.Read())
                //         {
                //             Console.WriteLine("Data with latest id from tbl_ktp table: " + reader["id"] + " " + reader["nik"] + " " + reader["nama"]);
                //         }
                //     }
                // }
                // this.sqc = new SqlCeConnection(this.constr);
                // this.sqc.Open();
                // this.dbc = new DBKTPContext((IDbConnection)this.sqc);
            }
            catch (Exception e)
            { 
                int num = (int) MessageBox.Show("Error :" + e.Message);
            }
            
            UCBacaKTP.i = 0;
            InitializeComponent();
            InitHidden();
            spinning.IsChecked = new bool?(true);
            wheel.Visibility = Visibility.Hidden;
            this.Loaded += UCBacaKTP_Loaded; // Add event handler for Loaded event
        }

        private void InitHidden()
        {
            niklbl.Visibility = Visibility.Hidden;
            namalbl.Visibility = Visibility.Hidden;
            ttllbl.Visibility = Visibility.Hidden;
            jnskelaminlbl.Visibility = Visibility.Hidden;
            goldarahlbl.Visibility = Visibility.Hidden;
            alamatlbl.Visibility = Visibility.Hidden;
            rtrwlbl.Visibility = Visibility.Hidden;
            kellbl.Visibility = Visibility.Hidden;
            keclbl.Visibility = Visibility.Hidden;
            agamalbl.Visibility = Visibility.Hidden;
            statuskawinlbl.Visibility = Visibility.Hidden;
            pekerjaanlbl.Visibility = Visibility.Hidden;
            kewarganegaraanlbl.Visibility = Visibility.Hidden;
            berlakulbl.Visibility = Visibility.Hidden;
            kotalbl.Visibility = Visibility.Hidden;
            imagepb.Source = (ImageSource)null;
            signature_pb.Source = (ImageSource)null;
            propinsilbl.Visibility = Visibility.Hidden;
        }

        private void InitShow()
        {
            niklbl.Visibility = Visibility.Visible;
            namalbl.Visibility = Visibility.Visible;
            ttllbl.Visibility = Visibility.Visible;
            jnskelaminlbl.Visibility = Visibility.Visible;
            goldarahlbl.Visibility = Visibility.Visible;
            alamatlbl.Visibility = Visibility.Visible;
            rtrwlbl.Visibility = Visibility.Visible;
            kellbl.Visibility = Visibility.Visible;
            keclbl.Visibility = Visibility.Visible;
            agamalbl.Visibility = Visibility.Visible;
            statuskawinlbl.Visibility = Visibility.Visible;
            pekerjaanlbl.Visibility = Visibility.Visible;
            kewarganegaraanlbl.Visibility = Visibility.Visible;
            berlakulbl.Visibility = Visibility.Visible;
            kotalbl.Visibility = Visibility.Visible;
            propinsilbl.Visibility = Visibility.Visible;
        }
        
        // public void countData()
        // {
        //     try
        //     {
        //         this.jmlcountlbl.Content = (object) this.ListKTP.Select<MD_KTP, MD_KTP>((System.Func<MD_KTP, MD_KTP>) (em => em)).Count<MD_KTP>().ToString();
        //     }
        //     catch (Exception ex)
        //     {
        //         this.log.LogError(ex);
        //     }
        // }

        public void loadData()
        {
            try
            {
                // Show all data (not only today's data)
                IQueryable<WpfApplication1.Tables.Tbl_ktp> source = this.dbc.Tbl_ktp.Select<Tbl_ktp, Tbl_ktp>((Expression<System.Func<Tbl_ktp, Tbl_ktp>>) (em => em));
                Expression<System.Func<Tbl_ktp, DateTime?>> keySelector = (Expression<System.Func<Tbl_ktp, DateTime?>>) (q => q.Tglinput);
                foreach (Tbl_ktp tblKtp in (IEnumerable<Tbl_ktp>) source.OrderBy<Tbl_ktp, DateTime?>(keySelector))
                {
                    MD_KTP mdKtp = new MD_KTP();
                    ++UCBacaKTP.i;
                    mdKtp.ID = UCBacaKTP.i;
                    mdKtp.NIK = tblKtp.Nik.ToUpper();
                    mdKtp.NAMA = tblKtp.Nama.ToUpper();
                    mdKtp.TEMPATLAHIR = tblKtp.Tempatlhr.ToUpper();
                    DateTime dateTime1 = Convert.ToDateTime((object) tblKtp.Tgllahir);
                    mdKtp.TGLLAHIR = dateTime1.ToShortDateString().ToUpper();
                    mdKtp.JNSKELAMIN = tblKtp.Jnskelamin.ToUpper();
                    mdKtp.ALAMAT = tblKtp.Alamat.ToUpper();
                    mdKtp.RTRW = tblKtp.Rtrw.ToUpper();
                    mdKtp.KEL = tblKtp.Kel.ToUpper();
                    mdKtp.KEC = tblKtp.Kec.ToUpper();
                    mdKtp.AGAMA = tblKtp.Agama.ToUpper();
                    mdKtp.STATUSKAWIN = tblKtp.Statuskawin.ToUpper();
                    mdKtp.PEKERJAAN = tblKtp.Pekerjaan.ToUpper();
                    mdKtp.KEWARGANEGARAAN = tblKtp.Kewarganegaraan.ToUpper();
                    DateTime dateTime2 = Convert.ToDateTime((object) tblKtp.Berlaku);
                    mdKtp.BERLAKU = dateTime2.ToShortDateString().ToUpper();
                    mdKtp.PROPINSI = tblKtp.Provinsi.ToUpper();
                    mdKtp.FOTO = this.util.HexStringToByteArray(tblKtp.Foto);
                    mdKtp.TANDATANGAN = this.util.HexStringToByteArray(tblKtp.Tandatangan);
                    mdKtp.GOLDARAH = tblKtp.Goldarah.ToUpper();
                    mdKtp.TGLINPUT = Convert.ToString((object) tblKtp.Tglinput).ToUpper();
                    this.ListKTP.Add(mdKtp);
                }
            }
            catch (Exception e)
            {
                log.LogError(e);
            }
        }

        public void loadDataHariIni()
        {
            try
            { 
                IQueryable<WpfApplication1.Tables.Tbl_ktp> source = this.dbc.Tbl_ktp.Select<Tbl_ktp, Tbl_ktp>((Expression<System.Func<Tbl_ktp, Tbl_ktp>>) (em => em)).Where<Tbl_ktp>((Expression<System.Func<Tbl_ktp, bool>>) (q => q.Tglinput.Value.Year == DateTime.Now.Year)).Where<Tbl_ktp>((Expression<System.Func<Tbl_ktp, bool>>) (r => r.Tglinput.Value.Month == DateTime.Now.Month)).Where<Tbl_ktp>((Expression<System.Func<Tbl_ktp, bool>>) (s => s.Tglinput.Value.Day == DateTime.Now.Day));
                Expression<System.Func<Tbl_ktp, DateTime?>> keySelector = (Expression<System.Func<Tbl_ktp, DateTime?>>) (q => q.Tglinput);
                foreach (Tbl_ktp tblKtp in (IEnumerable<Tbl_ktp>) source.OrderBy<Tbl_ktp, DateTime?>(keySelector))
                {
                  MD_KTP mdKtp = new MD_KTP();
                  ++UCBacaKTP.i;
                  mdKtp.ID = UCBacaKTP.i;
                  mdKtp.NIK = tblKtp.Nik.ToUpper();
                  mdKtp.NAMA = tblKtp.Nama.ToUpper();
                  mdKtp.TEMPATLAHIR = tblKtp.Tempatlhr.ToUpper();
                  DateTime dateTime1 = Convert.ToDateTime((object) tblKtp.Tgllahir);
                  mdKtp.TGLLAHIR = dateTime1.ToShortDateString().ToUpper();
                  mdKtp.JNSKELAMIN = tblKtp.Jnskelamin.ToUpper();
                  mdKtp.ALAMAT = tblKtp.Alamat.ToUpper();
                  mdKtp.RTRW = tblKtp.Rtrw.ToUpper();
                  mdKtp.KEL = tblKtp.Kel.ToUpper();
                  mdKtp.KEC = tblKtp.Kec.ToUpper();
                  mdKtp.AGAMA = tblKtp.Agama.ToUpper();
                  mdKtp.STATUSKAWIN = tblKtp.Statuskawin.ToUpper();
                  mdKtp.PEKERJAAN = tblKtp.Pekerjaan.ToUpper();
                  mdKtp.KEWARGANEGARAAN = tblKtp.Kewarganegaraan.ToUpper();
                  DateTime dateTime2 = Convert.ToDateTime((object) tblKtp.Berlaku);
                  mdKtp.BERLAKU = dateTime2.ToShortDateString().ToUpper();
                  mdKtp.PROPINSI = tblKtp.Provinsi.ToUpper();
                  mdKtp.FOTO = this.util.HexStringToByteArray(tblKtp.Foto);
                  mdKtp.TANDATANGAN = this.util.HexStringToByteArray(tblKtp.Tandatangan);
                  mdKtp.GOLDARAH = tblKtp.Goldarah.ToUpper();
                  mdKtp.TGLINPUT = Convert.ToString((object) tblKtp.Tglinput).ToUpper();
                  this.ListKTP.Add(mdKtp);
                }
            }
            catch (Exception e)
            {
                log.LogError(e);
            }
        }
        
        public static byte[] CreateImageThumbnail(byte[] image, int width = 60, int height = 70)
        {
            using (MemoryStream memoryStream1 = new MemoryStream(image))
            {
                using (MemoryStream memoryStream2 = new MemoryStream())
                {
                    System.Drawing.Image.FromStream((Stream) memoryStream1).GetThumbnailImage(width, height, (System.Drawing.Image.GetThumbnailImageAbort) (() => false), IntPtr.Zero).Save((Stream) memoryStream2, ImageFormat.Jpeg);
                    return memoryStream2.GetBuffer();
                }
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return (BitmapImage) null;
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                memoryStream.Position = 0L;
                bitmapImage.BeginInit();
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = (Uri) null;
                bitmapImage.StreamSource = (Stream) memoryStream;
                bitmapImage.EndInit();
            }
            bitmapImage.Freeze();
            return bitmapImage;
        }
        
        // reverse LoadImage
        private static byte[] reverseLoadImage(BitmapImage bitmapImage)
        {
            byte[] numArray = (byte[]) null;
            if (bitmapImage != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BitmapEncoder bitmapEncoder = (BitmapEncoder) new PngBitmapEncoder();
                    bitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource) bitmapImage));
                    bitmapEncoder.Save((Stream) memoryStream);
                    numArray = memoryStream.ToArray();
                }
            }
            return numArray;
        }
        
        public Bitmap[] PrintCardDesign(DataEktp dataktp)
        {
          Bitmap bitmap1 = new Bitmap(!File.Exists(Settings.CardFrontImage) ? System.Drawing.Image.FromFile(Settings.CardFrontImage) : System.Drawing.Image.FromFile(Settings.CardFrontImage), 1035, 661);
          bitmap1.SetResolution(300f, 300f);
          Graphics graphics = Graphics.FromImage((System.Drawing.Image) bitmap1);
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.TextContrast = 0;
          graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
          int num = 33;
          new StringFormat().Alignment = StringAlignment.Center;
          System.Drawing.Font textSettings = Settings.TextSettings;
          System.Drawing.Font labelSettings = Settings.LabelSettings;
          int x1 = 30;
          int y1 = 118;
          int x2 = 250;
          System.Drawing.Brush brush = (System.Drawing.Brush) new SolidBrush(System.Drawing.Color.Black);
          
          if (dataktp != null)
          {
            graphics.DrawString("PROVINSI  ", Settings.StockItSettings, System.Drawing.Brushes.Black, 310f, 20f);
            graphics.DrawString(dataktp.getProvinsi().ToUpper(), Settings.StockItSettings, System.Drawing.Brushes.Black, 490f, 20f);
            graphics.DrawString(dataktp.getKota().ToUpper(), Settings.StockItSettings, System.Drawing.Brushes.Black, 380f, 60f);
            graphics.DrawString("NIK", Settings.LabelSettings, brush, (float) x1, (float) y1);
            graphics.DrawString(": " + dataktp.getNik(), Settings.StockItSettings, System.Drawing.Brushes.Black, 200f, (float) y1);
            int y2 = y1 + num;
            System.Drawing.Rectangle rectangle1 = new System.Drawing.Rectangle(x2, y2, 380, 100);
            graphics.DrawRectangle(Pens.Transparent, rectangle1);
            graphics.DrawString("Nama", Settings.LabelSettings, brush, (float) x1, (float) y2);
            int y3;
            if (this.data.getNama().Length < 23)
            {
              graphics.DrawString(": " + dataktp.getNama().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y2);
              y3 = y2 + num;
            }
            else
            {
              graphics.DrawString(dataktp.getNama().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (RectangleF) rectangle1);
              y3 = y2 + num * 2;
            }
            
            dataktp.getTtl().Split(',');
            graphics.DrawString("Tempat/Tgl Lahir", Settings.LabelSettings, brush, (float) x1, (float) y3);
            graphics.DrawString(": " + dataktp.getTtl(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y3);
            int y4 = y3 + num;
            graphics.DrawString("Gol. Darah", Settings.LabelSettings, brush, 500f, (float) y4);
            graphics.DrawString("Jenis Kelamin", Settings.LabelSettings, System.Drawing.Brushes.Black, (float) x1, (float) y4);
            graphics.DrawString(": " + dataktp.getJenisKelamin().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y4);
            graphics.DrawString(": " + dataktp.getGolDarah(), Settings.TextSettings, System.Drawing.Brushes.Black, 640f, (float) y4);
            int y5 = y4 + num;
            System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(x2, y5, 430, 100);
            graphics.DrawRectangle(Pens.Transparent, rectangle2);
            graphics.DrawString("Alamat", Settings.LabelSettings, brush, (float) x1, (float) y5);
            int y6;
            if (this.data.getAlamat().Length < 40)
            {
              graphics.DrawString(": " + dataktp.getAlamat().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y5);
              y6 = y5 + num;
            }
            else
            {
              graphics.DrawString(": " + dataktp.getAlamat().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (RectangleF) rectangle2);
              y6 = y5 + num * 2;
            }
            graphics.DrawString("RT/RW", Settings.LabelSettings, brush, 70f, (float) y6);
            graphics.DrawString(": " + dataktp.getRtRw().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y6);
            int y7 = y6 + num;
            graphics.DrawString("Kel/Desa", Settings.LabelSettings, brush, 70f, (float) y7);
            graphics.DrawString(": " + dataktp.getKelDesa().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y7);
            int y8 = y7 + num;
            graphics.DrawString("Kecamatan", Settings.LabelSettings, brush, 70f, (float) y8);
            graphics.DrawString(": " + dataktp.getKecamatan().ToUpper(), textSettings, System.Drawing.Brushes.Black, (float) x2, (float) y8);
            int y9 = y8 + num;
            graphics.DrawString("Agama", Settings.LabelSettings, brush, (float) x1, (float) y9);
            graphics.DrawString(": " + dataktp.getAgama().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y9);
            int y10 = y9 + num;
            graphics.DrawString("Status Perkawinan", Settings.LabelSettings, brush, (float) x1, (float) y10);
            graphics.DrawString(": " + dataktp.getStatusPerkawinan().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y10);
            int y11 = y10 + num;
            graphics.DrawString("Pekerjaan", Settings.LabelSettings, brush, (float) x1, (float) y11);
            graphics.DrawString(": " + dataktp.getPekerjaan().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y11);
            int y12 = y11 + num;
            graphics.DrawString("Kewarganegaraan", Settings.LabelSettings, brush, (float) x1, (float) y12);
            graphics.DrawString(": " + dataktp.getKewarganegaraan().ToUpper(), Settings.TextSettings, System.Drawing.Brushes.Black, (float) x2, (float) y12);
            int y13 = y12 + num;
            graphics.DrawString("Berlaku Hingga", Settings.LabelSettings, brush, (float) x1, (float) y13);
            graphics.DrawString(": " + dataktp.getBerlakuHingga(), textSettings, System.Drawing.Brushes.Black, (float) x2, (float) y13);
            if (this.data.getPoto() != null)
            {
              Bitmap original;
              using (MemoryStream memoryStream = new MemoryStream(dataktp.getPoto()))
                original = new Bitmap((Stream) memoryStream);
              Bitmap bitmap2 = new Bitmap((System.Drawing.Image) original, Settings.FotoSize);
              bitmap2.SetResolution(500f, 500f);
              graphics.DrawImage((System.Drawing.Image) bitmap2, Settings.FotoLocation);
            }
            if (this.data.getTandatangan() != null)
            {
              Bitmap original;
              using (MemoryStream memoryStream = new MemoryStream(dataktp.getTandatangan()))
                original = new Bitmap((Stream) memoryStream);
              Bitmap bitmap3 = new Bitmap((System.Drawing.Image) original, Settings.TtdSize);
              bitmap3.SetResolution(500f, 500f);
              graphics.DrawImage((System.Drawing.Image) bitmap3, Settings.TtdLocation);
            }
          }
          return new Bitmap[1]{ bitmap1 };
        }
            
        private void readKTP(string port)
        {
          string status = "";
          int attempt = 0;
          string[] passjari = (string[]) null;
          ReadSerial readSerial = new ReadSerial(port);
          try
          {
            for (; attempt < 4; ++attempt)
            {
              if (attempt.Equals(0))
                this.data = readSerial.ReadDemogFotoVerV3(attempt, ref passjari, ref status);
              else if (attempt.Equals(2))
              {
                MessageBox.Show("Letakkan " + passjari[0], "Alert", MessageBoxButton.OK, MessageBoxImage.Question);
                this.data = readSerial.ReadDemogFotoVerV3(attempt, ref passjari, ref status);
                if (this.data != null && status == "verified")
                {
                  this.showData(this.data);
                  attempt = 3;
                }
              }
              else if (attempt.Equals(1))
              {
                MessageBox.Show("Letakkan " + passjari[0], "Alert", MessageBoxButton.OK, MessageBoxImage.Question);
                this.data = readSerial.ReadDemogFotoVerV3(attempt, ref passjari, ref status);
                if (this.data != null && status == "verified")
                {
                  this.showData(this.data);
                  attempt = 3;
                }
              }
              else
              {
                MessageBox.Show("Letakkan " + passjari[1], "Alert", MessageBoxButton.OK, MessageBoxImage.Question);
                this.data = readSerial.ReadDemogFotoVerV3(attempt, ref passjari, ref status);
                if (this.data != null)
                {
                  if (status == "verified")
                  {
                    this.showData(this.data);
                    attempt = 3;
                  }
                }
                else
                {
                  int num = (int) MessageBox.Show("Verifikasi gagal 3 kali!", "Perhatian");
                }
              }
            }
            if (status == "gagal")
            {
              int num1 = (int) MessageBox.Show("Pembacaan gagal, KTP Tidak Terbaca!", "Perhatian");
            }
            attempt = 0;
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Kartu Tidak Terbaca!", "Perhatian");
          }
          readSerial.closePort();
        }
        
        private void UCBacaKTP_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            //dataLoad();
        }
        
        private void bacaEktptrial()
        {
            this.data.setNik("3277011003850024");
            this.data.setNama("ZAINI HAFID");
            this.data.setTtl("BANDUNG, 10-03-1985");
            this.data.setJenisKelamin("LAKI-LAKI");
            this.data.setGolDarah("O");
            this.data.setAlamat("Jl. MUKODAR TENGAH NO 247");
            this.data.setRtRw("005/007");
            this.data.setKelDesa("CIBEUREUM");
            this.data.setKecamatan("CIMAHI SELATAN");
            this.data.setAgama("ISLAM");
            this.data.setStatusPerkawinan("BELUM KAWIN");
            this.data.setPekerjaan("KARYAWAN SWASTA");
            this.data.setKewarganegaraan("WNI");
            this.data.setBerlakuHingga("10-03-2017");
            this.data.setProvinsi("JAWA BARAT");
            this.data.setKota("KOTA CIMAHI");
            this.data.setPoto((byte[]) null);
            this.data.setTandatangan((byte[]) null);
        }
        
        private void showData(DataEktp data)
        {
            try
            {
                if (data == null)
                    return;
                this.niklbl.Content = (object) data.getNik();
                this.namalbl.Content = (object) data.getNama();
                this.ttllbl.Content = (object) data.getTtl();
                this.jnskelaminlbl.Content = (object) data.getJenisKelamin();
                this.goldarahlbl.Content = (object) data.getGolDarah();
                this.alamatlbl.Content = (object) data.getAlamat();
                this.rtrwlbl.Content = (object) data.getRtRw();
                this.kellbl.Content = (object) data.getKelDesa();
                this.keclbl.Content = (object) data.getKecamatan();
                this.agamalbl.Content = (object) data.getAgama();
                this.statuskawinlbl.Content = (object) data.getStatusPerkawinan();
                this.pekerjaanlbl.Content = (object) data.getPekerjaan();
                this.kewarganegaraanlbl.Content = (object) data.getKewarganegaraan();
                this.berlakulbl.Content = (object) data.getBerlakuHingga();
                this.propinsilbl.Content = (object) data.getProvinsi();
                this.kotalbl.Content = (object) data.getKota();
                this.imagepb.Source = (ImageSource) UCBacaKTP.LoadImage(data.getPoto());
                this.signature_pb.Source = (ImageSource) UCBacaKTP.LoadImage(data.getTandatangan());
                InitShow();
            }
            catch (Exception ex)
            {
                this.log.LogError(ex);
            }
        }
        
        public bool cariPort()
        {
            string[] strArray = (string[]) null;
            bool flag1 = false;
            bool flag2;
            do
            {
                try
                {
                    strArray = SerialPort.GetPortNames();
                    flag2 = false;
                }
                catch (COMException ex)
                {
                    flag2 = true;
                }
                Thread.Sleep(100);
            }
            while (flag2);
            if (strArray.Length != 0)
            {
                string port1 = (string) null;
                foreach (string port2 in strArray)
                {
                    ReadSerial readSerial = new ReadSerial(port2);
                    if (readSerial.getIsConnected())
                    {
                        port1 = port2;
                        flag1 = true;
                        readSerial.closePort();
                        break;
                    }
                    readSerial.closePort();
                }
                if (flag1)
                {
                    KSEIApp.Properties.Settings.Default.port = port1;
                    KSEIApp.Properties.Settings.Default.Save();
                    KSEIApp.Properties.Settings.Default.Upgrade();
                    this.readKTP(port1);
                }
            }
            return flag1;
        }
        
        /*private void dataLoad()
        {
            try
            {
                this.listktpdg.ItemsSource = (IEnumerable) this.ListKTP.Select(em => new
                {
                    ID = em.ID,
                    NIK = em.NIK,
                    NAMA = em.NAMA,
                    TEMPATLAHIR = em.TEMPATLAHIR,
                    TGLLAHIR = em.TGLLAHIR,
                    JNSKELAMIN = em.JNSKELAMIN,
                    ALAMAT = em.ALAMAT,
                    RTRW = em.RTRW,
                    KEL = em.KEL,
                    KEC = em.KEC,
                    AGAMA = em.AGAMA,
                    STATUSKAWIN = em.STATUSKAWIN,
                    PEKERJAAN = em.PEKERJAAN,
                    KEWARGANEGARAAN = em.KEWARGANEGARAAN,
                    BERLAKU = em.BERLAKU,
                    PROPINSI = em.PROPINSI,
                    GOLDARAH = em.GOLDARAH,
                    TGLINPUT = em.TGLINPUT
                });
                // print tanda tangan dan foto (just the base64 string)
                // Console.WriteLine("Tanda Tangan: " + this.ListKTP[0].TANDATANGAN);
                // Console.WriteLine("Foto: " + this.ListKTP[0].FOTO);
                //this.countData();
            }
            catch (Exception ex)
            {
                this.log.LogError(ex);
            }
        }*/

        private void bacaktpBtn_Click(object sender, RoutedEventArgs e)
        {
            InitHidden();
            //bacaEktptrial();
            string port = KSEIApp.Properties.Settings.Default.port;
            if (port != string.Empty)
            {
                if (new ReadSerial(port).getIsConnected())
                {
                    MessageBox.Show("Kabel dan BacaKTP telah tersambung", "Alert", MessageBoxButton.OK);
                    readKTP(port);
                }
                else if (!cariPort())
                {
                    MessageBox.Show("Perhatian: Kabel USB belum terhubung. Silakan periksa dan pastikan kabel tersambung dengan benar untuk melanjutkan. 🚦🔌", "Alert", MessageBoxButton.OK);
                    //int num1 = (int) MessageBox.Show("Kabel USB Belum Terhubung", "Alert", MessageBoxButton.OK);
                }
            }
            else if (cariPort())
            {
                int num2 = (int) MessageBox.Show("Kabel USB Belum Terhubung", "Alert", MessageBoxButton.OK);
            }
            //MessageBox.Show("System still in development");
        }

        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("🚧 System still in development. Please check back later! 🚀", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            try
            {
                if (this.data != null)
                {
                    PrintDialog printDialog = new PrintDialog();
                    bool? nullable = printDialog.ShowDialog();
                    bool flag = true;
                    if (nullable.GetValueOrDefault() != flag || !nullable.HasValue)
                        return;
                    Bitmap[] bitmapArray = this.PrintCardDesign(this.data);
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(15.0);
                    System.Windows.Controls.Image element = new System.Windows.Controls.Image();
                    element.Width = 500.0;
                    element.Stretch = Stretch.Uniform;
                    element.Source = (ImageSource) this.util.loadBitmap(bitmapArray[0]);
                    stackPanel.Children.Add((UIElement) element);
                    stackPanel.Measure(new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
                    stackPanel.Arrange(new Rect(new System.Windows.Point(0.0, 0.0), stackPanel.DesiredSize));
                    printDialog.PrintVisual((Visual) stackPanel, "KTP-el");
                }
                else
                {
                    int num = (int) MessageBox.Show("Baca KTP-el Terlebih Dahulu!", "Informasi");
                }
            }
            catch (Exception exception)
            {
                log.LogError(exception);
            }
        }

        private void exportallBtn_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("🚧 System still in development. Please check back later! 🚀", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            DateTime datenow = DateTime.Now;
            MessageBox.Show("Apakah data akan diexport?", "Alert", MessageBoxButton.OK, MessageBoxImage.Question);
            if (KSEIApp.Properties.Settings.Default.txt)
                this.utilFile.generateFileAll(this.ListKTP, KSEIApp.Properties.Settings.Default.singleexportpath, datenow);
            if (KSEIApp.Properties.Settings.Default.xml)
                this.utilFile.generateXMLAll(KSEIApp.Properties.Settings.Default.singleexportpath, datenow, this.ListKTP);
            /*if (KSEIApp.Properties.Settings.Default.excel)
            {
              Thread thread = new Thread((ParameterizedThreadStart) (param => this.exportToExcelAll(KSEIApp.Properties.Settings.Default.singleexportpath, datenow, ".xls")));
              thread.SetApartmentState(ApartmentState.STA);
              thread.Start();
            }
            if (KSEIApp.Properties.Settings.Default.excel2013)
            {
              Thread thread = new Thread((ParameterizedThreadStart) (param => this.exportToExcelAll(KSEIApp.Properties.Settings.Default.singleexportpath, datenow, ".xlsx")));
              thread.SetApartmentState(ApartmentState.STA);
              thread.Start();
            }*/
            int num = (int) MessageBox.Show("Export Sukses, Silahkan Cek di Folder " + KSEIApp.Properties.Settings.Default.serviceexportpath + " !", "Alert", MessageBoxButton.OK);
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            ListKTP.Clear();
            UCBacaKTP.i = 0;
            //dataLoad();
            //MessageBox.Show("🚧 System still in development. Please check back later! 🚀", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void simpanBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.data != null)
                {
                    if (!(MessageBox.Show("Apakah data akan disimpan?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question).ToString() != "No"))
                        return;

                    // Dapatkan ID terbaru dari database
                    string getLastIdQuery = "SELECT MAX(id) FROM Tbl_ktp";
                    SQLiteCommand getLastIdCmd = new SQLiteCommand(getLastIdQuery, conn);
                    int lastId = Convert.ToInt32(getLastIdCmd.ExecuteScalar());

                    // Perbarui data dengan ID terbaru
                    try
                    {
                        // Query untuk update data
                        string updateQuery = "UPDATE Tbl_ktp SET Nik = @Nik, Nama = @Nama, Tempatlhr = @Tempatlhr, Tgllahir = @Tgllahir, Jnskelamin = @Jnskelamin, Alamat = @Alamat, Rtrw = @Rtrw, Kel = @Kel, Kec = @Kec, Agama = @Agama, Statuskawin = @Statuskawin, Pekerjaan = @Pekerjaan, Kewarganegaraan = @Kewarganegaraan, Berlaku = @Berlaku, Provinsi = @Provinsi, Goldarah = @Goldarah, Foto = @Foto, Tandatangan = @Tandatangan, Tglinput = @Tglinput WHERE id = @Id";
                        SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@Id", lastId);
                        updateCmd.Parameters.AddWithValue("@Nik", this.data.getNik());
                        updateCmd.Parameters.AddWithValue("@Nama", this.data.getNama());
                        updateCmd.Parameters.AddWithValue("@Tempatlhr", this.data.getTtl().Split(',')[0]);
                        updateCmd.Parameters.AddWithValue("@Tgllahir", Convert.ToDateTime(this.data.getTtl().Split(',')[1]));
                        updateCmd.Parameters.AddWithValue("@Jnskelamin", this.data.getJenisKelamin());
                        updateCmd.Parameters.AddWithValue("@Alamat", this.data.getAlamat());
                        updateCmd.Parameters.AddWithValue("@Rtrw", this.data.getRtRw());
                        updateCmd.Parameters.AddWithValue("@Kel", this.data.getKelDesa());
                        updateCmd.Parameters.AddWithValue("@Kec", this.data.getKecamatan());
                        updateCmd.Parameters.AddWithValue("@Agama", this.data.getAgama());
                        updateCmd.Parameters.AddWithValue("@Statuskawin", this.data.getStatusPerkawinan());
                        updateCmd.Parameters.AddWithValue("@Pekerjaan", this.data.getPekerjaan());
                        updateCmd.Parameters.AddWithValue("@Kewarganegaraan", this.data.getKewarganegaraan());
                        updateCmd.Parameters.AddWithValue("@Berlaku", Convert.ToDateTime(this.data.getBerlakuHingga()));
                        updateCmd.Parameters.AddWithValue("@Provinsi", this.data.getProvinsi());
                        updateCmd.Parameters.AddWithValue("@Goldarah", this.data.getGolDarah());
                        updateCmd.Parameters.AddWithValue("@Foto", this.util.ByteArrayToHexString(this.data.getPoto()));
                        updateCmd.Parameters.AddWithValue("@Tandatangan", this.util.ByteArrayToHexString(this.data.getTandatangan()));
                        updateCmd.Parameters.AddWithValue("@Tglinput", DateTime.Now);
                        updateCmd.ExecuteNonQuery();
                        // select data yang telah diupdate
                        string selectQuery = "SELECT * FROM Tbl_ktp WHERE id = @Id";
                        MessageBox.Show("Data berhasil diperbarui!", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        this.log.LogError(ex);
                        MessageBox.Show("Tidak dapat memperbarui data" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                    
                    MD_KTP mdKtp = new MD_KTP();
                    ++UCBacaKTP.i;
                    mdKtp.ID = UCBacaKTP.i;
                    mdKtp.NIK = this.data.getNik();
                    mdKtp.NAMA = this.data.getNama();
                    string[] strArray1 = this.data.getTtl().Split(',');
                    mdKtp.TEMPATLAHIR = strArray1[0].ToString();
                    mdKtp.TGLLAHIR = strArray1[1].ToString();
                    mdKtp.JNSKELAMIN = this.data.getJenisKelamin();
                    mdKtp.ALAMAT = this.data.getAlamat();
                    mdKtp.RTRW = this.data.getRtRw();
                    mdKtp.KEL = this.data.getKelDesa();
                    mdKtp.KEC = this.data.getKecamatan();
                    mdKtp.AGAMA = this.data.getAgama();
                    mdKtp.STATUSKAWIN = this.data.getStatusPerkawinan();
                    mdKtp.PEKERJAAN = this.data.getPekerjaan();
                    mdKtp.KEWARGANEGARAAN = this.data.getKewarganegaraan();
                    mdKtp.BERLAKU = this.data.getBerlakuHingga().Trim();
                    mdKtp.PROPINSI = this.data.getProvinsi();
                    mdKtp.FOTO = this.data.getPoto();
                    mdKtp.TANDATANGAN = this.data.getTandatangan();
                    mdKtp.GOLDARAH = this.data.getGolDarah();
                    mdKtp.TGLINPUT = DateTime.Now.ToString();
                    this.ListKTP.Add(mdKtp);
                    
                    string isifile = mdKtp.NIK.Trim() + "|" + mdKtp.NAMA.Trim() + "|" + mdKtp.TEMPATLAHIR.Trim() + "|" + (object) Convert.ToDateTime(mdKtp.TGLLAHIR) + "|" + mdKtp.JNSKELAMIN.Trim() + "|" + mdKtp.ALAMAT.Trim() + "|" + mdKtp.RTRW.Trim() + "|" + mdKtp.KEL.Trim() + "|" + mdKtp.KEC.Trim() + "|" + mdKtp.AGAMA.Trim() + "|" + mdKtp.STATUSKAWIN.Trim() + "|" + mdKtp.PEKERJAAN.Trim() + "|" + mdKtp.KEWARGANEGARAAN.Trim() + "|" + DateTime.Parse(mdKtp.BERLAKU.Trim(), (IFormatProvider) CultureInfo.CreateSpecificCulture("fr-FR")).ToShortDateString() + "|" + mdKtp.PROPINSI.Trim() + "|" + mdKtp.GOLDARAH.Trim() + "|" + mdKtp.TGLINPUT.Trim();
                    DateTime datenow = DateTime.Now;
                    
                    // Save the data to file
                    if (KSEIApp.Properties.Settings.Default.txt)
                        this.utilFile.generateFile(isifile, KSEIApp.Properties.Settings.Default.singleexportpath.ToString(), datenow);
                    if (KSEIApp.Properties.Settings.Default.pdf)
                        this.generatePDFFile(KSEIApp.Properties.Settings.Default.singleexportpath.ToString(), datenow);
                    /*if (KSEIApp.Properties.Settings.Default.excel2013)
                    {
                        Thread thread = new Thread((ParameterizedThreadStart) (param => this.exportToExcel(KSEIApp.Properties.Settings.Default.singleexportpath.ToString(), datenow, ".xlsx")));
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                    if (KSEIApp.Properties.Settings.Default.excel)
                    {
                        Thread thread = new Thread((ParameterizedThreadStart) (param => this.exportToExcel(KSEIApp.Properties.Settings.Default.singleexportpath.ToString(), datenow, ".xls")));
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }*/
                    // if (KSEIApp.Properties.Settings.Default.xml)
                    //   this.utilFile.generateXML(KSEIApp.Properties.Settings.Default.singleexportpath.ToString(), datenow, this.data);
                    
                    InitHidden();
                    //dataLoad();
                }
                else
                {
                    int num = (int) MessageBox.Show("Baca KTP-el Terlebih Dahulu!", "Informasi");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Tidak dapat menyimpan data" + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            //MessageBox.Show("🚧 System still in development. Please check back later! 🚀", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
        private void generatePDFFile(string folderpath, DateTime date)
        {
            string _folderpath = "";
            try
            {
                if (!this.utilFile.checkFolderExist(folderpath, ref _folderpath))
                    return;
                string str = this.data.getNama() + "_" + this.data.getNik() + "_" + date.ToString("yyyyMMddHHmmss") + ".pdf";
                Document document = new Document(PageSize.A4, 42f, 42f, 42f, 35f);
                PdfWriter.GetInstance(document, (Stream) new FileStream(_folderpath + "\\" + str, FileMode.Create));
                document.Open();
                iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance((System.Drawing.Image) this.PrintCardDesign(this.data)[0], ImageFormat.Bmp);
                instance.ScaleAbsoluteHeight(164f);
                instance.ScaleAbsoluteWidth(260f);
                document.Add((IElement) instance);
                document.Close();
            }
            catch (Exception ex)
            {
                this.log.LogError(ex);
            }
        }
    }
}