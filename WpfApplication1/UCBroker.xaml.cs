using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using libComm;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCBroker : UserControl
    {
        public static ObservableCollection<Broker> SelectedBrokers { get; set; } = new ObservableCollection<Broker>();
        private string connectionstring = "Data Source=data.sqlite;Version=3;";
        private SQLiteConnection conn;
        private DBKTPContext dbc;
        private UtilLog log = new UtilLog();

        public UCBroker()
        {
            try
            {
                conn = new SQLiteConnection(connectionstring);
                conn.Open();
                dbc = DBContextSingleton.Instance;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            InitializeComponent();
            LoadDataBroker();
            countData();
        }

        public void LoadDataBroker()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT id_broker AS ID, kode_perusahaan_efek AS KODE_PERUSAHAAN, Nama AS NAMA_PERUSAHAAN, Alamat AS ALAMAT_PERUSAHAAN, nomor_telepon AS NOMOR_TELEPON FROM Tbl_perusahaan_efek";
                DataTable dt = new DataTable();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                brokerdg.ItemsSource = dt.DefaultView;
            }
            catch (Exception e)
            {
                log.LogError(e);
                MessageBox.Show("Error: " + e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void PilihBroker_Click(object sender, RoutedEventArgs e)
        {
            List<Broker> selectedBrokers = new List<Broker>();

            foreach (var item in brokerdg.Items)
            {
                if (item is DataRowView row && Convert.ToBoolean(row["IsSelected"]))
                {
                    selectedBrokers.Add(new Broker
                    {
                        KODE_PERUSAHAAN = row["KODE_PERUSAHAAN"].ToString(),
                        NAMA_PERUSAHAAN = row["NAMA_PERUSAHAAN"].ToString()
                    });
                }
            }

            // var mainWindow = Application.Current.MainWindow as MainWindow;
            // if (mainWindow != null)
            // {
            //     mainWindow.SelectedBrokers = selectedBrokers;
            //     mainWindow.LoadMainContent(new UCMain(selectedBrokers));
            // }
        }

        public void countData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT COUNT(*) FROM Tbl_perusahaan_efek";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    this.jmlcountlbl.Content = count;
                }
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
    }

    public class Broker
    {
        public bool IsSelected { get; set; }
        public string KODE_PERUSAHAAN { get; set; }
        public string NAMA_PERUSAHAAN { get; set; }
    }
}
