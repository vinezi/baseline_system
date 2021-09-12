using baseline_system.DialogBox;
using baseline_system.Pages.Admin;
using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace baseline_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        private static readonly string connectionString = Properties.Settings.Default.connectStrCur;
        public PageLogin()
        {
            InitializeComponent();
            Properties.Settings.Default.currentUserID = 0;
            Properties.Settings.Default.admStatus = false;
            Properties.Settings.Default.currentUserName = "";
            Properties.Settings.Default.Save();
        }

        private void LoginUser_Click(object sender, RoutedEventArgs e)
        {
            if (Authorization())
                _ = NavigationService.Navigate(new PageMainFrame());
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new PageAdminPanel());
            if (Authorization())
            {
                if (Properties.Settings.Default.admStatus)
                    NavigationService.Navigate(new PageAdminPanel());
                else
                {
                    ErrorBox errorBox = new ErrorBox("Not enough rights");
                    if (errorBox.ShowDialog() == true)
                        MessageBox.Show("i");
                }
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new PageRegistration());
        }

        private void DefConect_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.connectStrCur = Properties.Settings.Default.connectStrDef;
            Properties.Settings.Default.Save();
            defaultDB.Visibility = Visibility.Collapsed;
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private bool isCorrectLength()
        {
            return userLoginField.Text.Length >= 3 && userPasswordField.SecurePassword.Length >= 3 &&
                userLoginField.Text.Length < 20 && userPasswordField.SecurePassword.Length < 15;
        }

        private bool Authorization()
        {
            if (isCorrectLength())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("sp_Login", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Login", userLoginField.Text));
                        command.Parameters.Add(new SqlParameter("@Password", userPasswordField.Password.ToString()));
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Properties.Settings.Default.currentUserID = reader.GetInt32(0);
                                Properties.Settings.Default.admStatus = reader.GetBoolean(1);
                                Properties.Settings.Default.currentUserName = reader.GetString(2);
                            }
                            Properties.Settings.Default.Save();
                            connection.Close();
                            Console.WriteLine("Reg --- OK");
                            return true;
                        }
                        else
                        {
                            ErrorBox errorBox = new ErrorBox("Auth error");
                            if (errorBox.ShowDialog() == true)
                                MessageBox.Show("i");
                            connection.Close();
                            Console.WriteLine("Reg --- NO");
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    //ErrorBox errorBox = new ErrorBox(ex.ToString());
                    //if (errorBox.ShowDialog() == true)
                    //    MessageBox.Show("i");
                    defaultDB.Visibility = Visibility.Visible;
                    return false;

                }
            }
            else
            {
                ErrorBox errorBox = new ErrorBox("Incorrect Input");
                if (errorBox.ShowDialog() == true)
                    MessageBox.Show("i");
                return false;
            }
        }

        private void userLoginField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(userLoginField.Text, "^[a-zA-Z0-9_]*$") && userLoginField.Text.Length > 0|| userLoginField.Text.Length > 20) //
            {
                userLoginField.Text = userLoginField.Text.Remove(userLoginField.Text.Length - 1);
                userLoginField.SelectionStart = userLoginField.Text.Length;
                IsVisibleEror(true);
            }
            else
                IsVisibleEror(false);
        }

        private void userPasswordField_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (userPasswordField.SecurePassword.Length > 15)
            {
                userPasswordField.Password = userPasswordField.Password.Remove(userPasswordField.Password.Length - 1); 
                IsVisibleEror(true);
                SetSelection(userPasswordField, userPasswordField.Password.Length);
                userPasswordField.Focus();
            }
            else
                IsVisibleEror(false);
        }

        private void SetSelection(PasswordBox passwordBox, int start)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, start });
        }

        private void IsVisibleEror(bool turnOn) //display erorr
        {
            if (errorMsg != null)
            {
                if (errorMsg.IsVisible) //if on
                {
                    if (!turnOn) //if need off //else do nothing
                        errorMsg.Visibility = Visibility.Collapsed;
                }
                else //if off
                {
                    if (turnOn) //if need on // else do nothing
                        errorMsg.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
