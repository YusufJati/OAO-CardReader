using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using MessageBox = System.Windows.MessageBox;

namespace WpfApplication1
{
    public partial class UCUpdate : System.Windows.Controls.UserControl, IComponentConnector
    {
        public UCUpdate()
        { 
            InitializeComponent();
          if (KSEIApp.Properties.Settings.Default.pdf)
            this.pdfsetting.IsChecked = new bool?(true);
          else
            this.pdfsetting.IsChecked = new bool?(false);
          if (KSEIApp.Properties.Settings.Default.txt)
            this.txtsetting.IsChecked = new bool?(true);
          else
            this.txtsetting.IsChecked = new bool?(false);
          this.filenameTxt.Text = !(KSEIApp.Properties.Settings.Default.singleexportpath != string.Empty) ? "D:\\Export" : KSEIApp.Properties.Settings.Default.singleexportpath.ToString();
          this.filenameservicetxt.Text = !(KSEIApp.Properties.Settings.Default.serviceexportpath != string.Empty) ? "D:\\Export" : KSEIApp.Properties.Settings.Default.serviceexportpath.ToString();
          if (KSEIApp.Properties.Settings.Default.useridservice != string.Empty)
            this.useridtxt.Text = KSEIApp.Properties.Settings.Default.useridservice;
          if (KSEIApp.Properties.Settings.Default.pwdservice != string.Empty)
            this.passwordtxt.Text = KSEIApp.Properties.Settings.Default.pwdservice;
          if (KSEIApp.Properties.Settings.Default.ipaddressservice != string.Empty)
            this.ipaddresstxt.Text = KSEIApp.Properties.Settings.Default.ipaddressservice;
          if (!KSEIApp.Properties.Settings.Default.webservice)
          {
            this.PengaturanWebServiceGroupBox.Visibility = Visibility.Hidden;
            this.ipaddresslblx.Visibility = Visibility.Hidden;
            this.passwordlblx.Visibility = Visibility.Hidden;
            this.useridlblx.Visibility = Visibility.Hidden;
            this.ipaddresstxt.Visibility = Visibility.Hidden;
            this.passwordtxt.Visibility = Visibility.Hidden;
            this.useridtxt.Visibility = Visibility.Hidden;
            this.filenameservicetxt.Visibility = Visibility.Hidden;
            this.browseservicebtn.Visibility = Visibility.Hidden;
            this.lokasipenyimpananlblx.Visibility = Visibility.Hidden;
            this.updateservicebtn.Visibility = Visibility.Hidden;
          }
          else
          {
            this.PengaturanWebServiceGroupBox.Visibility = Visibility.Visible;
            this.ipaddresslblx.Visibility = Visibility.Visible;
            this.passwordlblx.Visibility = Visibility.Visible;
            this.useridlblx.Visibility = Visibility.Visible;
            this.ipaddresstxt.Visibility = Visibility.Visible;
            this.passwordtxt.Visibility = Visibility.Visible;
            this.useridtxt.Visibility = Visibility.Visible;
            this.filenameservicetxt.Visibility = Visibility.Visible;
            this.browseservicebtn.Visibility = Visibility.Visible;
            this.lokasipenyimpananlblx.Visibility = Visibility.Visible;
            this.updateservicebtn.Visibility = Visibility.Visible;
          }
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Apakah Setting akan disimpan?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool? isChecked = pdfsetting.IsChecked;
                    KSEIApp.Properties.Settings.Default.pdf = isChecked.GetValueOrDefault() && isChecked.HasValue;
            
                    isChecked = txtsetting.IsChecked;
                    KSEIApp.Properties.Settings.Default.txt = isChecked.GetValueOrDefault() && isChecked.HasValue;
            
                    KSEIApp.Properties.Settings.Default.singleexportpath = filenameTxt.Text;
                    KSEIApp.Properties.Settings.Default.Save();
                    KSEIApp.Properties.Settings.Default.Upgrade();
                    Console.WriteLine(KSEIApp.Properties.Settings.Default.singleexportpath.ToString());
            
                    MessageBox.Show("Data berhasil disimpan", "Information", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                // Log the exception details to a file or logging system
                string logFilePath = "error_log.txt";
                string errorMessage = $"Error: {ex.Message}\nStack Trace: {ex.StackTrace}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {ex.InnerException.Message}\nInner Stack Trace: {ex.InnerException.StackTrace}";
                }

                // Append the error message to the log file
                System.IO.File.AppendAllText(logFilePath, $"{DateTime.Now}: {errorMessage}\n");

                // Display the error message to the user
                MessageBox.Show("Terdapat kesalahan dalam penyimpanan setting, silahkan coba lagi", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void browsebtn_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    return;
                filenameTxt.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void pdfsetting_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void pdfsetting_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void txtsetting_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void txtsetting_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void updateservicebtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("System still in development");
        }
    }
}