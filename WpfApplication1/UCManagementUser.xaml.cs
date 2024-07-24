using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using libComm;
using WpfApplication1.Tables;
using WpfApplication1.Utils;

namespace WpfApplication1
{
    public partial class UCManagementUser : UserControl
    {
        private string constr = KSEIApp.Properties.Settings.Default.DBKTPLocalConn;
        private string connectionstring = "Data Source=./data.sqlite;Version=3;";
        private SQLiteConnection conn;
        private DBKTPContext dbc;
        private UtilLog log = new UtilLog();

        public MainWindow _mainWindow { get; set; }

        public UCManagementUser()
        {
            try
            {
                conn = new SQLiteConnection(connectionstring);
                conn.Open();
                dbc = new DBKTPContext(conn);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            InitializeComponent();
            loaddata();
            LoadRoles();
            ToggleVisibilityBasedOnRole();
        }

        private void LoadRoles()
        {
            IQueryable<Tbl_role> roles = dbc.Tbl_role.Select(role => role);
            rolecb.SelectedValuePath = "_Key";
            rolecb.DisplayMemberPath = "_Value";
            foreach (Tbl_role role in roles)
            {
                rolecb.Items.Add(new ComboboxPairs(Convert.ToString(role.Id_role), role.Role));
            }
        }

        private void ToggleVisibilityBasedOnRole()
        {
            if (loginSession.Id_role == 1)
            {
                addUserGroup.Visibility = Visibility.Visible;
                userdg.Visibility = Visibility.Visible;
                listuserlbl.Visibility = Visibility.Visible;
            }
            else
            {
                listuserlbl.Visibility = Visibility.Hidden;
                addUserGroup.Visibility = Visibility.Hidden;
                userdg.Visibility = Visibility.Hidden;
            }
        }

        public void loaddata()
        {
            try
            {
                var userList = dbc.Tbl_login.Select(user => new DC_Login
                {
                    ID = user.Id,
                    USERNAME = user.Username,
                    ROLE = user.Tbl_role.Role,
                    DESKRIPSI = user.Deskripsi
                }).ToList();

                userdg.ItemsSource = userList;
            }
            catch (Exception e)
            {
                log.LogError(e);
            }
        }

        private void resetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            DC_Login selectedItem = (DC_Login)userdg.SelectedItem;
            int id = Convert.ToInt32(selectedItem.ID);
            if (MessageBox.Show($"Apakah Password {selectedItem.USERNAME} akan direset?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (loginSession.Id_role == 1)
                {
                    Tbl_login user = dbc.Tbl_login.SingleOrDefault(q => q.Id == id);
                    if (user != null)
                    {
                        user.Password = selectedItem.USERNAME;
                        dbc.SubmitChanges();
                        MessageBox.Show("Reset Password berhasil!!", "Alert", MessageBoxButton.OK);
                        loaddata();
                    }
                }
                else
                {
                    MessageBox.Show("Anda tidak mempunyai privilage untuk mereset password!!", "Alert", MessageBoxButton.OK);
                }
            }
        }

        private void updateUserbtn_Click(object sender, RoutedEventArgs e)
        {
            DC_Login selectedUser = (DC_Login)userdg.SelectedItem;
            if (selectedUser != null)
            {
                int id = Convert.ToInt32(selectedUser.ID);
                Tbl_login user = dbc.Tbl_login.SingleOrDefault(q => q.Id == id);
                if (user != null)
                {
                    addusernametxt.IsEnabled = false;
                    addusernametxt.Text = user.Username;
                    addpasswordtxt.Text = user.Password;
                    deskripsitxt.Text = user.Deskripsi;
                    rolecb.SelectedValue = user.Id_role.ToString();
                    addUserSimpanbtn.Content = "Update";
                }
            }
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldPasswordtxt.Text == loginSession.Password)
                {
                    if (newPasswordtxt.Text == reNewPasswordtxt.Text)
                    {
                        // Show confirmation message box
                        if (MessageBox.Show("Apakah Anda yakin ingin mengupdate password?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            Tbl_login user = dbc.Tbl_login.SingleOrDefault(q => q.Id == loginSession.Id);
                            if (user != null)
                            {
                                user.Password = reNewPasswordtxt.Text;
                                dbc.SubmitChanges();
                                loaddata();
                                clearChangePassword();
                                MessageBox.Show("Password berhasil diupdate!", "Alert", MessageBoxButton.OK);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password baru dan konfirmasi password tidak cocok!", "Alert", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Password lama tidak cocok!", "Alert", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tidak dapat mengupdate data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void addusernametxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsAlphabetic(e.Text) && !char.IsDigit(e.Text[0]);
        }

        private void addusernametxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

private void addUserSimpanbtn_Click(object sender, RoutedEventArgs e)
{
    try
    {
        string message = addUserSimpanbtn.Content.ToString() == "Update" ? "Apakah data user akan diupdate?" : "Apakah username dan password akan disimpan?";
        if (MessageBox.Show(message, "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            if (rolecb.SelectedIndex != -1 && !string.IsNullOrEmpty(addusernametxt.Text) && !string.IsNullOrEmpty(addpasswordtxt.Text) && !string.IsNullOrEmpty(deskripsitxt.Text))
            {
                using (var conn = new SQLiteConnection(connectionstring))
                {
                    conn.Open();
                    if (addUserSimpanbtn.Content.ToString() == "Update")
                    {
                        int id = Convert.ToInt32(((DataRowView)userdg.SelectedItem)["ID"]);
                        string query = "UPDATE tbl_login SET Password = @Password, Deskripsi = @Deskripsi, Id_role = @Id_role WHERE Id = @Id";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Password", addpasswordtxt.Text);
                            cmd.Parameters.AddWithValue("@Deskripsi", deskripsitxt.Text);
                            cmd.Parameters.AddWithValue("@Id_role", Convert.ToInt32(rolecb.SelectedValue));
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Data berhasil diupdate!", "Alert", MessageBoxButton.OK);
                    }
                    else
                    {
                        string checkQuery = "SELECT COUNT(1) FROM tbl_login WHERE Username = @Username";
                        using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@Username", addusernametxt.Text);
                            int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (userExists > 0)
                            {
                                MessageBox.Show("Username tersebut sudah ada di dalam database, pilih username yang lain!", "Information", MessageBoxButton.OK);
                            }
                            else
                            {
                                string insertQuery = "INSERT INTO tbl_login (Username, Password, Deskripsi, Id_role) VALUES (@Username, @Password, @Deskripsi, @Id_role)";
                                using (var cmd = new SQLiteCommand(insertQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@Username", addusernametxt.Text);
                                    cmd.Parameters.AddWithValue("@Password", addpasswordtxt.Text);
                                    cmd.Parameters.AddWithValue("@Deskripsi", deskripsitxt.Text);
                                    cmd.Parameters.AddWithValue("@Id_role", Convert.ToInt32(rolecb.SelectedValue));
                                    cmd.ExecuteNonQuery();
                                }
                                MessageBox.Show("Data berhasil disimpan!", "Alert", MessageBoxButton.OK);
                            }
                        }
                    }
                    loaddata();
                    clearAddUser();
                    addUserSimpanbtn.Content = "Simpan";
                    addusernametxt.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Lengkapi form pendaftaran user!", "Alert", MessageBoxButton.OK);
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
        
        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            DC_Login selectedUser = (DC_Login)userdg.SelectedItem;
            if (selectedUser != null)
            {
                if (MessageBox.Show($"Apakah user {selectedUser.USERNAME} akan dihapus?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (loginSession.Id_role == 1)
                    {
                        int id = Convert.ToInt32(selectedUser.ID);
                        Tbl_login user = dbc.Tbl_login.SingleOrDefault(q => q.Id == id);
                        if (user != null)
                        {
                            dbc.Tbl_login.DeleteOnSubmit(user);
                            dbc.SubmitChanges();
                            MessageBox.Show("Data berhasil dihapus!", "Alert", MessageBoxButton.OK);
                            loaddata();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Anda tidak mempunyai privilage untuk menghapus user!!", "Alert", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            loginSession.Username = string.Empty;
            loginSession.Password = string.Empty;
            loginSession.Id = 0;
            loginSession.Id_role = 0;
            new MainLogin().Show();
            Application.Current.Windows[0].Close();
        }

        private void clearChangePassword()
        {
            oldPasswordtxt.Text = string.Empty;
            newPasswordtxt.Text = string.Empty;
            reNewPasswordtxt.Text = string.Empty;
        }

        private void clearAddUser()
        {
            addusernametxt.Text = string.Empty;
            addpasswordtxt.Text = string.Empty;
            rolecb.SelectedIndex = -1;
            deskripsitxt.Text = string.Empty;
        }

        private bool IsAlphabetic(string s) => new Regex("^[a-zA-Z]+$").IsMatch(s);
    }
}
