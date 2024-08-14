using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
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
        public ObservableCollection<Broker> SelectedBrokers { get; set; } = new ObservableCollection<Broker>();
        public static List<int> simpanId = new List<int>();
        //private DBKTPContext dbc;
        private UtilLog log = new UtilLog();
        public event EventHandler<List<Broker>> BrokersSelected;
        private string selectedBrokersFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "selectedBrokers.json");


        public UCBroker()
        {
            InitializeComponent();
            DataContext = this;
            loadBrokers();
            LoadSelectedBrokersFromFile();
            //countData();
        }

        private async void loadBrokers()
        {
            try
            {
                //List<Utils.Customer> customerss = await ApiClient.GetAllCustomers();
                List<Utils.Broker> brokers = await ApiClient.GetAllBrokers();
                // print data from API
                // foreach (var broker in brokers)
                // {
                //     Console.WriteLine($"ID: {broker.Id}, Kode Perusahaan: {broker.kode}, Nama Perusahaan: {broker.nama}");
                // }
                // show data from API to datagrid
                var limit = brokers.Take(5);
                jmlcountlbl.Content = brokers.Count();
                 foreach (var broker in brokers)
                 {
                     Brokers.Add(new Broker
                     {
                         ID = broker.Id,
                         KODE_PERUSAHAAN = broker.kode,
                         NAMA_PERUSAHAAN = broker.nama,
                         //Alamat = broker.alamat,
                         //nomor_telepon = broker.nomor_telepon,
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

        //private void PilihBroker_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedBrokers = Brokers.Where(b => b.IsSelected).ToList();
        //    simpanId = selectedBrokers.Select(b => b.ID).ToList();
        //    // Save the selectedBrokers to json file
        //    string json = JsonConvert.SerializeObject(selectedBrokers, Formatting.Indented);
        //    System.IO.File.WriteAllText("selectedBrokers.json", json);
        //    // read the json file
        //    string readJson = System.IO.File.ReadAllText("selectedBrokers.json");
        //    MessageBox.Show(readJson, "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        //    SelectedBrokers.Clear();
        //    simpanId.Clear();
        //    foreach (var broker in selectedBrokers)
        //    {
        //        SelectedBrokers.Add(broker);
        //        simpanId.Add(broker.ID);
        //    }

        //    selectedBrokerDg.ItemsSource = SelectedBrokers;

        //    BrokersSelected?.Invoke(this, SelectedBrokers.ToList());
        //}

        private void SaveSelectedBrokersToFile()
        {
            string json = JsonConvert.SerializeObject(SelectedBrokers, Formatting.Indented);
            System.IO.File.WriteAllText(selectedBrokersFilePath, json);
        }

        private void LoadSelectedBrokersFromFile()
        {
            if (System.IO.File.Exists(selectedBrokersFilePath))
            {
                string readJson = System.IO.File.ReadAllText(selectedBrokersFilePath);
                var savedBrokers = JsonConvert.DeserializeObject<List<Broker>>(readJson);

                SelectedBrokers.Clear();
                simpanId.Clear();

                foreach (var broker in savedBrokers)
                {
                    SelectedBrokers.Add(broker);
                    simpanId.Add(broker.ID);
                }
            }
        }

        private void PilihBroker_Click(object sender, RoutedEventArgs e)
        {
            var selectedBrokers = Brokers.Where(b => b.IsSelected).ToList();

            SelectedBrokers.Clear();
            simpanId.Clear();

            foreach (var broker in selectedBrokers)
            {
                SelectedBrokers.Add(broker);
                simpanId.Add(broker.ID);
            }

            SaveSelectedBrokersToFile();

            selectedBrokerDg.ItemsSource = SelectedBrokers;

            BrokersSelected?.Invoke(this, SelectedBrokers.ToList());

            MessageBox.Show("Selected brokers have been saved.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        //private void PilihBroker_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedBrokers = Brokers.Where(b => b.IsSelected).ToList();

        //    // Save the selectedBrokers to json file
        //    string json = JsonConvert.SerializeObject(selectedBrokers, Formatting.Indented);
        //    System.IO.File.WriteAllText("selectedBrokers.json", json);

        //    // Clear the current selected brokers
        //    SelectedBrokers.Clear();
        //    simpanId.Clear();

        //    // Read the json file
        //    string readJson = System.IO.File.ReadAllText("selectedBrokers.json");
        //    var savedBrokers = JsonConvert.DeserializeObject<List<Broker>>(readJson);

        //    // Fill SelectedBrokers and simpanId from the saved JSON data
        //    foreach (var broker in savedBrokers)
        //    {
        //        SelectedBrokers.Add(broker);
        //        simpanId.Add(broker.ID);
        //    }

        //    // Show information about the saved brokers
        //    MessageBox.Show(readJson, "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);

        //    selectedBrokerDg.ItemsSource = SelectedBrokers;

        //    BrokersSelected?.Invoke(this, SelectedBrokers.ToList());
        //}



        //private void PilihBroker_Click(object sender, RoutedEventArgs e)
        //{
        //    //MessageBox.Show("Pilih broker", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        //    List<Broker> selectedBrokers = Brokers.Where(b => b.IsSelected).ToList();

        //    if (selectedBrokers.Count == 0)
        //    {
        //        MessageBox.Show("Tidak ada broker yang dipilih. Silakan pilih broker terlebih dahulu.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    var confirmResult = MessageBox.Show("Apakah Anda yakin dengan pilihan broker?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (confirmResult != MessageBoxResult.Yes)
        //    {
        //        return;
        //    }


        //    // Simpan ID broker yang dipilih di MainWindow
        //    var mainWindow = Application.Current.MainWindow as MainWindow;
        //    if (mainWindow != null)
        //    {
        //        mainWindow.SelectedBrokerId = selectedBrokers.First().ID;
        //        Console.WriteLine($"Selected Broker ID: {mainWindow.SelectedBrokerId}");
        //        MessageBox.Show($"Broker dengan ID {mainWindow.SelectedBrokerId} telah dipilih.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("MainWindow is null");
        //    }

        //    //MessageBox.Show($"Broker dengan ID {selectedBrokerId} telah dipilih.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        //}

        //private void PilihBroker_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Pilih broker", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        //    List<Broker> selectedBrokers = Brokers.Where(b => b.IsSelected).ToList();

        //    if (selectedBrokers.Count == 0)
        //    {
        //        MessageBox.Show("Tidak ada broker yang dipilih. Silakan pilih broker terlebih dahulu.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    var confirmResult = MessageBox.Show("Apakah Anda yakin dengan pilihan broker?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (confirmResult != MessageBoxResult.Yes)
        //    {
        //        return;
        //    }

        //    var mainWindow = Application.Current.MainWindow as MainWindow; 
        //    //if (mainWindow != null)
        //    //{
        //    //    mainWindow.SelectedBrokers = selectedBrokers;
        //    //    mainWindow.LoadMainContent(new UCMain(selectedBrokers));
        //    //    MessageBox.Show("Broker telah dipilih dan data ditampilkan di form registrasi.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        //    //}
        //}


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

        private void brokerdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearBroker_Click(object sender, RoutedEventArgs e)
        {
            // Clear the selected brokers and simpanId
            SelectedBrokers.Clear();
            simpanId.Clear();

            // Save the cleared state to the file
            SaveSelectedBrokersToFile();

            // Clear the DataGrid's items source
            selectedBrokerDg.ItemsSource = null;

            // Show a message to the user
            MessageBox.Show("Daftar broker yang dipilih telah berhasil dihapus. Anda dapat memilih broker baru sekarang.", "Pembersihan Broker", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
