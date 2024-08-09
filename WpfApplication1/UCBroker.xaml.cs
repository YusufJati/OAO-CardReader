using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using libComm;
using Newtonsoft.Json;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCBroker : UserControl
    {
        public ObservableCollection<Broker> Brokers { get; set; } = new ObservableCollection<Broker>();
        private DBKTPContext dbc;
        private UtilLog log = new UtilLog();

        public UCBroker()
        {
            InitializeComponent();
            DataContext = this;
            loadBrokers();
            countData();
        }

        private async void loadBrokers()
        {
            try
            {
                List<Utils.Customer> customerss = await ApiClient.GetAllCustomers();
                List<Utils.Broker> brokers = await ApiClient.GetAllBrokers();
                // print data from API
                 // foreach (var broker in brokers)
                 // {
                 //     Console.WriteLine($"ID: {broker.Id}, Kode Perusahaan: {broker.kode}, Nama Perusahaan: {broker.nama}");
                 // }
                 // show data from API to datagrid
                 foreach (var broker in brokers)
                 {
                     Brokers.Add(new Broker
                     {
                         ID = broker.Id,
                         KODE_PERUSAHAAN = broker.kode,
                         NAMA_PERUSAHAAN = broker.nama,
                         Alamat = broker.alamat,
                         nomor_telepon = broker.nomor_telepon,
                         IsSelected = false
                     });
                 }
                 brokerdg.ItemsSource = Brokers;
            }
            catch (Exception e)
            {
                log.LogError(e);
                MessageBox.Show($"Error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PilihBroker_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pilih broker", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            /*List<Broker> selectedBrokers = Brokers.Where(b => b.IsSelected).ToList();

            if (selectedBrokers.Count == 0)
            {
                MessageBox.Show("Tidak ada broker yang dipilih. Silakan pilih broker terlebih dahulu.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Apakah Anda yakin dengan pilihan broker?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmResult != MessageBoxResult.Yes)
            {
                return;
            }

            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SelectedBrokers = selectedBrokers;
                mainWindow.LoadMainContent(new UCMain(selectedBrokers));
                MessageBox.Show("Broker telah dipilih dan data ditampilkan di form registrasi.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }*/
        }
        
        public async void countData()
        {
            try
            {
                List<Utils.Broker> brokers = await ApiClient.GetAllBrokers();
                var count = brokers.Count;

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

        /*public async void countData()
        {
            try
            {
                HttpResponseMessage response = await ApiClient.GetAsync("http://oao-be.koyeb.app/brokers/count");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var count = JsonConvert.DeserializeObject<int>(responseBody);

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
    }*/

        public class Broker
        {
            public int ID { get; set; }
            public string KODE_PERUSAHAAN { get; set; }
            public string NAMA_PERUSAHAAN { get; set; }
            public string Alamat { get; set; }
            public string nomor_telepon { get; set; }
            public bool IsSelected { get; set; }
        }

        public class ApiResponse<T>
        {
            public Meta meta { get; set; }
            public List<T> data { get; set; }
        }

        public class Meta
        {
            public int code { get; set; }
            public string message { get; set; }
        }
    }
}
