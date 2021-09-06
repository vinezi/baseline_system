using baseline_system.DialogBox;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace baseline_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        private static readonly string connectionString = Properties.Settings.Default.connectStrCur;

        //public object InputLanguage { get; }

        public PageRegistration()
        {
            InitializeComponent();
            //lang.Text = InputLanguage.CurrentInputLanguage;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new PageLogin());
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Registration();
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
            return userLoginField.Text.Length >= 3 && userLoginField.Text.Length < 20 &&
                userNameField.Text.Length >= 3 && userNameField.Text.Length < 15 &&
                userPasswordField.SecurePassword.Length >= 3 && userPasswordField.SecurePassword.Length < 15 &&
                userConfirmField.SecurePassword.Length == userPasswordField.SecurePassword.Length;
        }

        private void Registration()
        {
            if (isCorrectLength())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("sp_InsertUser", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Login", userLoginField.Text));
                        command.Parameters.Add(new SqlParameter("@Name", userNameField.Text));
                        command.Parameters.Add(new SqlParameter("@Password", userPasswordField.Password.ToString()));
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    Properties.Settings.Default.currentUserID = reader.GetInt32(0);
                                    Properties.Settings.Default.admStatus = false;
                                    Properties.Settings.Default.currentUserName = userNameField.Text;
                                    Properties.Settings.Default.Save();
                                    ErrorBox errorBox = new ErrorBox("Registration successful!\n Wait for authorization");
                                    if (errorBox.ShowDialog() == true)
                                        MessageBox.Show("i");
                                    NavigationService.Navigate(new PageMainFrame());
                                }
                                catch (Exception)
                                {
                                    ErrorBox errorBox = new ErrorBox("Registration error!\n User already exists");
                                    if (errorBox.ShowDialog() == true)
                                        MessageBox.Show("i");
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                    defaultDB.Visibility = Visibility.Visible;
                }
            }
            else
            {
                ErrorBox errorBox = new ErrorBox("Incorrect Input");
                if (errorBox.ShowDialog() == true)
                    MessageBox.Show("i");
            }
        }

        private void TVLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(userLoginField.Text, "^[a-zA-Z0-9_]*$") && userLoginField.Text.Length > 0 || userLoginField.Text.Length > 20) //
            {
                userLoginField.Text = userLoginField.Text.Remove(userLoginField.Text.Length - 1);
                userLoginField.SelectionStart = userLoginField.Text.Length;
                IsVisibleEror(true);
            }
            else
                IsVisibleEror(false);
        }

        private void TVName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(userNameField.Text, "^[a-zA-Z0-9_]*$") && userNameField.Text.Length > 0 || userNameField.Text.Length > 20) //
            {
                userNameField.Text = userNameField.Text.Remove(userNameField.Text.Length - 1);
                userNameField.SelectionStart = userNameField.Text.Length;
                IsVisibleEror(true);
            }
            else
                IsVisibleEror(false);
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
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

        private void Confirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (userConfirmField.SecurePassword.Length > 15)
            {
                userConfirmField.Password = userConfirmField.Password.Remove(userConfirmField.Password.Length - 1); IsVisibleEror(true);
                SetSelection(userConfirmField, userConfirmField.Password.Length);
                userConfirmField.Focus();
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