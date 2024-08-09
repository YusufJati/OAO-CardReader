/*
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using libComm;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCMain : UserControl
    {
        private string connectionstring = "Data Source=./data.sqlite;Version=3;";
        private SQLiteConnection conn;
        private DBKTPContext dbc;
        private UtilLog log = new UtilLog();
        private List<ComboboxPairs> brokerList;

        public MainWindow _mainWindow { get; set; }

        public UCMain()
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
            LoadData();
            LoadAB();
        }

        public UCMain(List<Broker> selectedBrokers) : this()
        {
            UpdateComboBox(selectedBrokers);
        }

        private void addUserSimpanbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ensure the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Check if email not empty
                if (string.IsNullOrWhiteSpace(addemailtxt.Text))
                {
                    MessageBox.Show("Email tidak boleh kosong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Get the selected broker ID
                if (abcb.SelectedValue == null)
                {
                    MessageBox.Show("Pilih broker terlebih dahulu.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string selectedBrokerID = abcb.SelectedValue.ToString();

                // Validate email format
                if (string.IsNullOrWhiteSpace(addemailtxt.Text) || !IsEmailValid(addemailtxt.Text))
                {
                    MessageBox.Show("Email tidak valid.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Confirm with the user before saving
                var confirmResult = MessageBox.Show("Apakah data sudah benar?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult != MessageBoxResult.Yes)
                {
                    return;
                }

                // SQL query to insert data
                string query = "INSERT INTO Tbl_ktp (id_broker, email) VALUES (@id_broker, @email)";

                // Create a command object
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@id_broker", selectedBrokerID);
                    cmd.Parameters.AddWithValue("@email", addemailtxt.Text);

                    // Execute the command and check if rows were affected
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Show success message
                        MessageBox.Show("Data berhasil disimpan!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        MessageBox.Show("Lanjut lakukan baca KTP", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Clear the input fields
                        addemailtxt.Clear();
                        abcb.SelectedIndex = -1;
                    }
                    else
                    {
                        // Show failure message
                        MessageBox.Show("Data tidak berhasil disimpan.", "Failure", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                string detailedError = "Terjadi kesalahan: " + ex.Message;
                if (ex.InnerException != null)
                {
                    detailedError += "\nInner Exception: " + ex.InnerException.Message;
                }
                MessageBox.Show(detailedError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void LoadData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT id_broker, kode_perusahaan_efek AS KODE_PERUSAHAAN, Nama AS NAMA_PERUSAHAAN FROM Tbl_perusahaan_efek";

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

        
        private void LoadAB()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT id_broker, Nama FROM Tbl_perusahaan_efek";
                DataTable dt = new DataTable();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                brokerList = new List<ComboboxPairs>();
                foreach (DataRow row in dt.Rows)
                {
                    brokerList.Add(new ComboboxPairs(row["id_broker"].ToString(), row["Nama"].ToString()));
                }

                abcb.ItemsSource = brokerList;
                abcb.SelectedValuePath = "_Key";
                abcb.DisplayMemberPath = "_Value";
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
        

        private void ComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.IsEditable)
            {
                var textBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;
                if (textBox != null)
                {
                    var searchText = textBox.Text.ToLower();
                    var filteredList = brokerList.Where(b => b._Value.ToLower().Contains(searchText)).ToList();
                    comboBox.ItemsSource = filteredList;
                    comboBox.IsDropDownOpen = true;
                    textBox.CaretIndex = searchText.Length;
                }
            }
        }

        private void addemailtxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidEmailCharacter(e.Text);
        }

        private void addemailtxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private bool IsValidEmailCharacter(string text)
        {
            var regex = new Regex("^[a-zA-Z0-9@._-]+$");
            return regex.IsMatch(text);
        }

        private void UpdateComboBox(List<Broker> selectedBrokers)
        {
            abcb.ItemsSource = selectedBrokers;
            abcb.SelectedValuePath = "KODE_PERUSAHAAN";
            abcb.DisplayMemberPath = "NAMA_PERUSAHAAN";
            abcb.SelectedIndex = 0;
        }
    }

    public class ComboboxPairs
    {
        public string _Key { get; set; }
        public string _Value { get; set; }

        public ComboboxPairs(string key, string value)
        {
            _Key = key;
            _Value = value;
        }

        public override string ToString()
        {
            return _Value;
        }
    }
}
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using libComm;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCMain : UserControl
    {
        private string connectionstring = "Data Source=./data.sqlite;Version=3;";
        private SQLiteConnection conn;
        private DBKTPContext dbc;
        private UtilLog log = new UtilLog();
        private List<ComboboxPairs> brokerList;

        public MainWindow _mainWindow { get; set; }

        public UCMain(List<Broker> selectedBrokers)
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
            //LoadAB(selectedBrokers);
            LoadAB();
        }
        
        private void addUserSimpanbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ensure the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                // Check if email not empty
                if (string.IsNullOrWhiteSpace(addemailtxt.Text))
                {
                    MessageBox.Show("Email tidak boleh kosong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Get the selected broker ID
                if (abcb.SelectedValue == null)
                {
                    MessageBox.Show("Pilih broker terlebih dahulu.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string selectedBrokerID = abcb.SelectedValue.ToString();

                // // Validate email format
                 if (string.IsNullOrWhiteSpace(addemailtxt.Text) || !IsEmailValid(addemailtxt.Text))
                 {
                     MessageBox.Show("Email tidak valid.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                     return;
                 }

                // Confirm with the user before saving
                var confirmResult = MessageBox.Show("Apakah data sudah benar?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult != MessageBoxResult.Yes)
                {
                    return;
                }

                // SQL query to insert data
                string query = "INSERT INTO Tbl_ktp (id_broker, email) VALUES (@id_broker, @email)";

                // Create a command object
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@id_broker", selectedBrokerID);
                    cmd.Parameters.AddWithValue("@email", addemailtxt.Text);

                    // Execute the command and check if rows were affected
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Show success message
                        MessageBox.Show("Data berhasil disimpan!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        MessageBox.Show("Lanjut lakukan baca KTP", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Clear the input fields
                        addemailtxt.Clear();
                        abcb.SelectedIndex = -1;
                    }
                    else
                    {
                        // Show failure message
                        MessageBox.Show("Data tidak berhasil disimpan.", "Failure", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                string detailedError = "Terjadi kesalahan: " + ex.Message;
                if (ex.InnerException != null)
                {
                    detailedError += "\nInner Exception: " + ex.InnerException.Message;
                }
                MessageBox.Show(detailedError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private void LoadAB()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                // Only show first 2 data
                string query = "SELECT id_broker, Nama FROM Tbl_perusahaan_efek LIMIT 2";
                //string query = "SELECT id_broker, Nama FROM Tbl_perusahaan_efek";
                DataTable dt = new DataTable();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                brokerList = new List<ComboboxPairs>();
                foreach (DataRow row in dt.Rows)
                {
                    brokerList.Add(new ComboboxPairs(row["id_broker"].ToString(), row["Nama"].ToString()));
                }

                abcb.ItemsSource = brokerList;
                abcb.SelectedValuePath = "_Key";
                abcb.DisplayMemberPath = "_Value";
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
        
        /*
        private void LoadAB(List<Broker> selectedBrokers)
        {
            brokerList = new List<ComboboxPairs>();
            foreach (var broker in selectedBrokers)
            {
                brokerList.Add(new ComboboxPairs(broker.KODE_PERUSAHAAN, broker.NAMA_PERUSAHAAN));
            }

            abcb.ItemsSource = brokerList;
            abcb.SelectedValuePath = "_Key";
            abcb.DisplayMemberPath = "_Value";
        }
        */

        private void ComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.IsEditable)
            {
                var textBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;
                if (textBox != null)
                {
                    var searchText = textBox.Text.ToLower();
                    var filteredList = brokerList.Where(b => b._Value.ToLower().Contains(searchText)).ToList();
                    comboBox.ItemsSource = filteredList;
                    comboBox.IsDropDownOpen = true;
                    textBox.CaretIndex = searchText.Length;
                }
            }
        }

        private void addemailtxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidEmailCharacter(e.Text);
        }

        private void addemailtxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private bool IsValidEmailCharacter(string text)
        {
            var regex = new Regex("^[a-zA-Z0-9@._-]+$");
            return regex.IsMatch(text);
        }
    }

    public class ComboboxPairs
    {
        public string _Key { get; set; }
        public string _Value { get; set; }

        public ComboboxPairs(string key, string value)
        {
            _Key = key;
            _Value = value;
        }

        public override string ToString()
        {
            return _Value;
        }
    }
}

