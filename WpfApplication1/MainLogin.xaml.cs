using System;
using System.Windows;
using System.Windows.Markup;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using WpfApplication1.Tables;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class MainLogin : Window, IComponentConnector
    {
        private string constr = KSEIApp.Properties.Settings.Default.DBKTPLocalConn;
        private string connectionstring = "Data Source=data.sqlite;Version=3;";
        private SQLiteConnection conn;
        private SqlCeConnection sqc;
        private DBKTPContext dbc;
        public MainLogin()
        {
            InitializeComponent();
            try
            {
                conn = new SQLiteConnection(connectionstring);
                conn.Open();
                dbc = new DBKTPContext(conn);
                
                // check if the database is connected successfully or not (query)
                 // var query = dbc.Tbl_login.Select(x => x);
                 // foreach (var item in query)
                 // {
                 //     Console.WriteLine(item.Username);
                 // }
                
                //conn = new SQLiteConnection("Data Source=data.sqlite;Version=3;");
                //conn = new SqlCeConnection(constr);
                //conn.Open();
                //this.dbc = new DBKTPContext(conn);
                //string databasepath = "data.sqlite";
                //string connectionString = $"Data Source={databasepath};";
                //dbc = new DBKTPContext(connectionString);
                
            }
            catch (Exception e)
            {
                int num = (int) MessageBox.Show("Error: " + e.Message);
            }
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e) => login();

        private void login()
        {
            string username = usernametxt.Text;
            string password = passwordBox.Password;
            if (this.usernametxt.Text != "" && this.passwordBox.Password.ToString() != "")
            {
                Tbl_login tblLogin = this.dbc.Tbl_login.Select<Tbl_login, Tbl_login>((Expression<System.Func<Tbl_login, Tbl_login>>) (em => em)).Where<Tbl_login>((Expression<System.Func<Tbl_login, bool>>) (q => q.Username == username)).Where<Tbl_login>((Expression<System.Func<Tbl_login, bool>>) (q => q.Password == password)).SingleOrDefault<Tbl_login>();
                if (tblLogin != null)
                {
                    loginSession.Username = username;
                    loginSession.Password = password;
                    loginSession.Id = Convert.ToInt32(tblLogin.Id);
                    loginSession.Id_role = Convert.ToInt32((object) tblLogin.Id_role);
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    int num = (int) MessageBox.Show("🙈 Oops! Username atau Password salah. Coba lagi ya! 😉", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            else
            {
                int num1 = (int) MessageBox.Show("⚠️ Uh-oh! Username dan password harus diisi! 🌟 Jangan lupa ya! 😊", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            /*if (usernametxt.Text != "" && passwordBox.Password.ToString() != "")
            {
                
                if (username == "admin" && password == "admin")
                {
                    new MainWindow().Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("🙈 Oops! Username atau Password salah. Coba lagi ya! 😉", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("⚠️ Uh-oh! Username dan password harus diisi! 🌟 Jangan lupa ya! 😊", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }*/
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;
            login();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;
            login();
        }
    }
}