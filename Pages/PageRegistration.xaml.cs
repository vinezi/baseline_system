using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
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
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public PageRegistration()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new PageLogin());
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool isCorrectLength()
        {
            return userLoginField.Text.Length >= 3 && userPasswordField.SecurePassword.Length >= 3 &&
                userLoginField.Text.Length < 20 && userPasswordField.SecurePassword.Length < 15;
        }

        private void Registration()
        {
            /*Properties.Settings.Default.admStatus = false;
            Properties.Settings.Default.Save();
            bool isEmpty = true;
            if (UsernameField.Text.Length >= 3 && PasswordField.Password.ToString().Length >= 3
                && ConfirmField.Password.ToString().Length >= 3
                && PasswordField.Password.ToString() == ConfirmField.Password.ToString())
                isEmpty = false;
            else
            {
                errRegistration.Text = "Минимум 3 символа";
                errRegistration.Visibility = Visibility.Visible;
            }
            if (!isEmpty)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_InsertUser", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Login", UsernameField.Text));
                    command.Parameters.Add(new SqlParameter("@Password", PasswordField.Password.ToString()));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Properties.Settings.Default.currentUserID = reader.GetInt32(0);
                                Properties.Settings.Default.currentUser = UsernameField.Text;
                                Properties.Settings.Default.Save();
                                errRegistration.Visibility = Visibility.Collapsed;
                                MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
                                if (mainWindow != null)
                                {
                                    mainWindow.Username.Visibility = Visibility.Visible;
                                    mainWindow.Username.Text = Properties.Settings.Default.currentUser;
                                }
                                NavigationService.Navigate(new MainPage());
                            }
                            catch (Exception)
                            {
                                errRegistration.Text = "Пользователь уже существует";
                                errRegistration.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    connection.Close();
                }
            }*/
        }

        private void TVControl_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PB_PasswordChanged(object sender, RoutedEventArgs e)
        {

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