using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlServerCe;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using libComm;
using WpfApplication1.Tables;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCData : UserControl
    {
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
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
        public UCData()
        {
            try
            {
                conn = new SQLiteConnection(connectionstring);
                conn.Open();
                dbc = DBContextSingleton.Instance;
                // print count of data email in tbl_ktp table using manual query 
                // using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(email) FROM tbl_ktp", conn))
                // {
                //     int count = Convert.ToInt32(command.ExecuteScalar());
                //     Console.WriteLine("Count of data email in tbl_ktp table: " + count);
                // }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            UCData.i = 0;
            InitializeComponent();
            this.Loaded += UCData_Loaded;

        }
        
        private void UCData_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            dataLoad();
        }
        
        /*private void dataLoad()
        {
            try
            {
                // Ensure there is data in ListKTP
                if (this.ListKTP == null || !this.ListKTP.Any())
                {
                    MessageBox.Show("Tidak ada data untuk ditampilkan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Bind data to DataGrid
                this.listktpdg.ItemsSource = this.ListKTP.Select(em => new
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
                    TGLINPUT = em.TGLINPUT,
                    EMAIL = em.EMAIL,
                    BROKER = em.BROKER,
                }).ToList(); // ToList ensures the query is executed immediately

                // Optional: Log the number of items loaded
                Console.WriteLine($"{this.ListKTP.Count} records loaded.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // Count the data
                this.countData();
            }
            catch (Exception ex)
            {
                this.log.LogError(ex);
                MessageBox.Show("Terjadi kesalahan saat memuat data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
        private async void dataLoad()
        {
            try
            {
                List<Customer> customers = await ApiClient.GetAllCustomers();

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

                this.listktpdg.ItemsSource = customers.OrderBy(c => c.Id).Select(c => new
                {
                    ID = c.Id,
                    NIK = c.Nik,
                    NAMA = c.Nama,
                    //TEMPATLAHIR = c.tempat_lahir,
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
                    //FOTO = c.Foto, // Display base64 image as needed
                    //TANDATANGAN = c.TandaTangan, // Display base64 signature as needed
                    TGLINPUT = c.tanggal_input?.ToString("yyyy-MM-dd"),
                    EMAIL = c.Email,
                    //BROKER = c.Broker != null ? string.Join(", ", c.Broker.Select(b => b.Nama)) : string.Empty
                }).ToList();

                // Cetak jumlah data yang dimuat ke DataGrid
                Console.WriteLine($"{customers.Count} records loaded.");

                this.countData();
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                Console.WriteLine("Terjadi kesalahan saat memuat data: " + ex.Message);
                MessageBox.Show("Terjadi kesalahan saat memuat data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public async void countData()
        {
            try
            {
                List<Utils.Customer> customers = await ApiClient.GetAllCustomers();
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
        
        /*public void loadData()
        {
            try
            {
                // Ensure the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Query to join tbl_ktp with tbl_broker to get the broker name
                string query = @"
                    SELECT k.id, k.nik, k.nama, k.tempatlhr, k.tgllahir, k.jnskelamin, k.goldarah, k.alamat, k.rtrw, 
                           k.kel, k.kec, k.agama, k.statuskawin, k.pekerjaan, k.kewarganegaraan, k.berlaku, k.provinsi, 
                           k.foto, k.tandatangan, k.tglinput, k.email, b.nama AS broker 
                    FROM tbl_ktp k
                    JOIN tbl_perusahaan_efek b ON k.id_broker = b.id_broker";

                using (SQLiteCommand command = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        ListKTP.Clear(); // Clear existing data
                        while (reader.Read())
                        {
                            MD_KTP mdKtp = new MD_KTP();
                            mdKtp.ID = Convert.ToInt32(reader["id"]);
                            mdKtp.NIK = reader["nik"].ToString();
                            mdKtp.NAMA = reader["nama"].ToString();
                            mdKtp.TEMPATLAHIR = reader["tempatlhr"].ToString();
                            mdKtp.TGLLAHIR = reader["tgllahir"].ToString();
                            mdKtp.JNSKELAMIN = reader["jnskelamin"].ToString();
                            mdKtp.GOLDARAH = reader["goldarah"].ToString();
                            mdKtp.ALAMAT = reader["alamat"].ToString();
                            mdKtp.RTRW = reader["rtrw"].ToString();
                            mdKtp.KEL = reader["kel"].ToString();
                            mdKtp.KEC = reader["kec"].ToString();
                            mdKtp.AGAMA = reader["agama"].ToString();
                            mdKtp.STATUSKAWIN = reader["statuskawin"].ToString();
                            mdKtp.PEKERJAAN = reader["pekerjaan"].ToString();
                            mdKtp.KEWARGANEGARAAN = reader["kewarganegaraan"].ToString();
                            mdKtp.BERLAKU = reader["berlaku"].ToString();
                            mdKtp.PROPINSI = reader["provinsi"].ToString();
                            mdKtp.FOTO = util.HexStringToByteArray(reader["foto"].ToString());
                            mdKtp.TANDATANGAN = util.HexStringToByteArray(reader["tandatangan"].ToString());
                            mdKtp.TGLINPUT = reader["tglinput"].ToString();
                            mdKtp.EMAIL = reader["email"].ToString();
                            mdKtp.BROKER = reader["broker"].ToString();
                            this.ListKTP.Add(mdKtp);
                        }
                    }
                }

                // Bind the list to the DataGrid
                listktpdg.ItemsSource = ListKTP;

                // Log the number of items loaded
                Console.WriteLine($"{ListKTP.Count} records loaded.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }*/


        public async void loadData()
        {
            try
            {
                List<Customer> customers = await ApiClient.GetAllCustomers();

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

                this.Customers = new ObservableCollection<Customer>(customers);

                // Bind data ke DataGrid
                listktpdg.ItemsSource = Customers.Select(c => new
                {
                    ID = c.Id,
                    NIK = c.Nik,
                    NAMA = c.Nama,
                    //TEMPATLAHIR = c.tempat_lahir,
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
                    //FOTO = c.Foto, // Display base64 image as needed
                    //TANDATANGAN = c.TandaTangan, // Display base64 signature as needed
                    TGLINPUT = c.tanggal_input?.ToString("yyyy-MM-dd"),
                    EMAIL = c.Email,
                    //BROKER = c.Broker != null ? string.Join(", ", c.Broker.Select(b => b.Nama)) : string.Empty
                }).ToList();

                // Cetak jumlah data yang dimuat ke DataGrid
                Console.WriteLine($"{Customers.Count} records loaded.");
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


        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void exportallBtn_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void simpanBtn_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}