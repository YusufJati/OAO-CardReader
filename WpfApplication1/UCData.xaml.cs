using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using libComm;
using WpfApplication1.Tables;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCData : UserControl
    {
        public ObservableCollection<Customer2> Customers { get; set; } = new ObservableCollection<Customer2>();
        private string connectionstring = "Data Source=data.sqlite;Version=3;";
        // Tampilkan simpanId di UCData
        //private SQLiteConnection conn;
        //private SqlCeConnection sqc;
        //private DBKTPContext dbc;
        private DataEktp data = (DataEktp) null;
        private UtilImageText util = new UtilImageText();
        private UtilLog log = new UtilLog();
        private List<MD_KTP> ListKTP = new List<MD_KTP>();
        private static int i = 0;
        private WpfApplication1.Utils.UtilFile utilFile = new WpfApplication1.Utils.UtilFile();
        public UCData()
        {
            //try
            //{
            //    conn = new SQLiteConnection(connectionstring);
            //    conn.Open();
            //    dbc = DBContextSingleton.Instance;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Error: " + e.Message);
            //}
            UCData.i = 0;
            InitializeComponent();
            this.Loaded += UCData_Loaded;

        }
        
        private void UCData_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }
        
        //private async void dataLoad()
        //{
        //    try
        //    {
        //        List<Customer2> customers = await ApiClient.GetAllCustomers();

        //        // Cek apakah 'customers' null atau kosong
        //        if (customers == null)
        //        {
        //            Console.WriteLine("Customers is null");
        //            MessageBox.Show("Tidak ada data untuk ditampilkan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        //            return;
        //        }
        //        if (customers.Count == 0)
        //        {
        //            Console.WriteLine("Customers list is empty");
        //            MessageBox.Show("Tidak ada data untuk ditampilkan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        //            return;
        //        }

        //        // Cetak jumlah data customer yang diterima
        //        Console.WriteLine($"Number of customers received: {customers.Count}");

        //        this.listktpdg.ItemsSource = customers.OrderBy(c => c.Id).Select(c => new
        //        {
        //            ID = c.Id,
        //            NIK = c.Nik,
        //            NAMA = c.Nama,
        //            //TEMPATLAHIR = c.tempat_lahir,
        //            TGLLAHIR = c.tanggal_lahir?.ToString("yyyy-MM-dd"),
        //            JNSKELAMIN = c.jenis_kelamin,
        //            GOLDARAH = c.golongan_darah,
        //            ALAMAT = c.Alamat,
        //            RTRW = c.rt_rw,
        //            KEL = c.Kelurahan,
        //            KEC = c.Kecamatan,
        //            AGAMA = c.Agama,
        //            STATUSKAWIN = c.status_pernikahan,
        //            PEKERJAAN = c.Pekerjaan,
        //            KEWARGANEGARAAN = c.kewarganegaraan,
        //            BERLAKU = c.tanggal_berlaku?.ToString("yyyy-MM-dd"),
        //            PROPINSI = c.Provinsi,
        //            //FOTO = c.foto, // Display base64 image as needed
        //            //TANDATANGAN = c.tanda_tangan, // Display base64 signature as needed
        //            TGLINPUT = c.tanggal_input?.ToString("yyyy-MM-dd"),
        //            EMAIL = c.Email,
        //            //BROKER = c.Broker != null ? string.Join(", ", c.Broker.Select(b => b.Nama)) : string.Empty
        //        }).ToList();

        //        // Cetak jumlah data yang dimuat ke DataGrid
        //        Console.WriteLine($"{customers.Count} records loaded.");

        //        //this.countData();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError(ex);
        //        Console.WriteLine("Terjadi kesalahan saat memuat data: " + ex.Message);
        //        MessageBox.Show("Terjadi kesalahan saat memuat data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        public async void countData()
        {
            try
            {
                List<Utils.Customer2> customers = await ApiClient.GetAllCustomers();
                var count = customers.Count;
                this.jmlcountlbl.Content = count;
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                string detailedError = "Terjadi kesalahan: " + e.Message;
                if (e.InnerException != null)
                {
                    detailedError += "\nInner Exception: " + e.InnerException.Message;
                }
                MessageBox.Show(detailedError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void loadData()
        {
            try
            {
                List<Customer2> customers = await ApiClient.GetAllCustomers();

                // Cek apakah 'customers' null atau kosong
                if (customers == null)
                {
                    Console.WriteLine("Customers is null");
                    MessageBox.Show("Tidak ada data untuk ditampilkan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (customers.Count == 0)
                {
                    Console.WriteLine("Customers list is empty");
                    MessageBox.Show("Tidak ada data untuk ditampilkan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Cetak jumlah data customer yang diterima
                Console.WriteLine($"Number of customers received: {customers.Count}");

                customers = customers.OrderBy(c => c.Id).ToList();
                

                this.Customers = new ObservableCollection<Customer2>(customers);

                // Bind data ke DataGrid
                listktpdg.ItemsSource = Customers.Select(c => new
                {
                    ID = c.Id,
                    NIK = c.Nik,
                    NAMA = c.Nama,
                    TEMPATLAHIR = c.tempat_lahir,
                    TGLLAHIR = c.tanggal_lahir?.ToString("yyyy-MM-dd"),
                    JNSKELAMIN = c.jenis_kelamin,
                    GOLDARAH = c.golongan_darah,
                    ALAMAT = c.Alamat,
                    RTRW = c.rt_rw,
                    KEL = c.Kelurahan,
                    KEC = c.Kecamatan,
                    AGAMA = c.Agama,
                    STATUSKAWIN = c.status_pernikahan,
                    PEKERJAAN = c.Pekerjaan,
                    KEWARGANEGARAAN = c.kewarganegaraan,
                    BERLAKU = c.tanggal_berlaku?.ToString("yyyy-MM-dd"),
                    PROPINSI = c.Provinsi,
                    KOTA = c.Kota,
                    FOTO = LoadImage(c.foto), // Display base64 image as needed
                    TANDATANGAN = LoadImage(c.tanda_tangan), // Display base64 signature as needed
                    KTP = LoadImage(c.ktp_capture),
                    TGLINPUT = c.tanggal_input?.ToString("yyyy-MM-dd"),
                    BROKER = c.broker,
                    EMAIL = c.Email,
                }).ToList();

                // Cetak jumlah data yang dimuat ke DataGrid
                Console.WriteLine($"{Customers.Count} records loaded.");
                this.countData();
            }
            catch (Exception e)
            {
                string detailedError = "Terjadi kesalahan: " + e.Message;
                if (e.InnerException != null)
                {
                    detailedError += "\nInner Exception: " + e.InnerException.Message;
                }
                Console.WriteLine(detailedError);
                MessageBox.Show(detailedError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //private void clearBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    throw new System.NotImplementedException();
        //}

        //private static BitmapImage LoadImage(byte[] imageData)
        //{
        //    if (imageData == null || imageData.Length == 0)
        //        return (BitmapImage)null;
        //    BitmapImage bitmapImage = new BitmapImage();
        //    using (MemoryStream memoryStream = new MemoryStream(imageData))
        //    {
        //        memoryStream.Position = 0L;
        //        bitmapImage.BeginInit();
        //        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //        bitmapImage.UriSource = (Uri)null;
        //        bitmapImage.StreamSource = (Stream)memoryStream;
        //        bitmapImage.EndInit();
        //    }
        //    bitmapImage.Freeze();
        //    return bitmapImage;
        //}
        public ImageSource LoadImage(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return null;
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                BitmapImage image = new BitmapImage();
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                }
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}