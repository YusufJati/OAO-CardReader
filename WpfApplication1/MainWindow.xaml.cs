using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlServerCe;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using WpfApplication1.obj.Debug;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IComponentConnector
    {
        public int SelectedBrokerId { get; set; } = -1;
        public List<Broker> SelectedBrokers { get; set; }
        private string connectionstring = "Data Source=data.sqlite;Version=3;";
        private SQLiteConnection conn;
        private SqlCeConnection sqc;
        private DBKTPContext dbc;
        public MainWindow()
        {
            InitializeComponent();
            Mouse.OverrideCursor = Cursors.Arrow;
            bacaMenu();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            BrushConverter brushConverter = new BrushConverter();
            serviceBtn.Background = (Brush)brushConverter.ConvertFrom("#E59400");

            // Check user role and set visibility of dataBtn
            //SetDataButtonVisibility();
        }
        
        public void LoadMainContent(UserControl control)
        {
            MainContentControl.Content = control;
        }

        //private void SetDataButtonVisibility()
        //{
        //    if (loginSession.Id_role == 1) // Assuming Id_role 1 is for admin
        //    {
        //        dataBtn.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        dataBtn.Visibility = Visibility.Collapsed;
        //        brokerBtn.Visibility = Visibility.Collapsed;
        //    }
        //}

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 274 && (wParam.ToInt32() & 65520) == 61456)
                handled = true;
            return IntPtr.Zero;
        }
        
        private void childWndow_CloseInitiated(ITabControl sender, EventArgs e)
        {
            this.gridMenu.Children.Clear();
            bacaMenu();
        }
        
        public void bacaMenu()
        {
            //UCMain ucMain = new UCMain(new List<Broker>());
            UCMain ucMain = new UCMain();
            this.gridMenu.Children.Clear();
            this.gridMenu.Children.Add((UIElement)ucMain);
            //this.gridMenu.Children.Add(ucMain);
        }

        private void aboutbtn_Click(object sender, RoutedEventArgs e)
        {
            new MainAbout().Show();
        }
        
        private void mainBtn_Click(object sender, RoutedEventArgs e)
        {
            //UCMain ucMain = new UCMain(new List<Broker>());
            UCMain ucMain = new UCMain();
            gridMenu.Children.Clear();
            gridMenu.Children.Add(ucMain);
        }

        private void menuBtn_Click(object sender, RoutedEventArgs e)
        {
            UCBacaKTP ucBacaKTP = new UCBacaKTP();
            gridMenu.Children.Clear();
            gridMenu.Children.Add(ucBacaKTP);
            BrushConverter brushConverter = new BrushConverter();
            this.menuBtn.Background = (Brush)brushConverter.ConvertFrom((object)"#E59400");
            this.managementUserBtn.Background = (Brush)brushConverter.ConvertFrom((object)"#373737");
            //this.settingBtn.Background = (Brush)brushConverter.ConvertFrom((object)"#373737");
            dataBtn.Background = (Brush)brushConverter.ConvertFrom((object)"#373737");
            serviceBtn.Background = (Brush)brushConverter.ConvertFrom((object)"#373737");
            brokerBtn.Background = (Brush)brushConverter.ConvertFrom((object)"#373737");
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            loginSession.Username = string.Empty;
            loginSession.Password = string.Empty;
            loginSession.Id = 0;
            loginSession.Id_role = 0;
            new MainLogin().Show();
            Application.Current.Windows[0].Close();
        }

        private void managementUserBtn_Click(object sender, RoutedEventArgs e)
        {
            UCManagementUser ucManagementUser = new UCManagementUser();
            gridMenu.Children.Clear();
            gridMenu.Children.Add(ucManagementUser);
            menuBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            managementUserBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#E59400");
            //settingBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            dataBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            serviceBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            brokerBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
        }

        private void settingBtn_Click_1(object sender, RoutedEventArgs e)
        {
            UCUpdate ucUpdate = new UCUpdate();
            gridMenu.Children.Clear();
            gridMenu.Children.Add(ucUpdate);
            menuBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            managementUserBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            //settingBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#E59400");
            dataBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            serviceBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            brokerBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
        }
        
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState != WindowState.Normal)
                return;
            this.WindowState = WindowState.Maximized;
        }

        private void mainBtn_Click1(object sender, RoutedEventArgs e)
        {
            bacaMenu();
            serviceBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#E59400");
            menuBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            managementUserBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            //settingBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            dataBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            brokerBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
        }

        private void dataBtn_Click(object sender, RoutedEventArgs e)
        {
            UCData ucData = new UCData();
            gridMenu.Children.Clear();
            gridMenu.Children.Add(ucData);
            dataBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#E59400");
            menuBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            managementUserBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            //settingBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            serviceBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            brokerBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            //MessageBox.Show("🚧 System still in development. Please check back later! 🚀", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void brokerBtn_Click(object sender, RoutedEventArgs e)
        {
            UCBroker ucBroker = new UCBroker();
            gridMenu.Children.Clear();
            gridMenu.Children.Add(ucBroker);
            brokerBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#E59400");
            menuBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            managementUserBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            //settingBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            dataBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
            serviceBtn.Background = (Brush)new BrushConverter().ConvertFrom((object)"#373737");
        }
    }
}
