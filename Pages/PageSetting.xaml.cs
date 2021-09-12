using baseline_system.DialogBox;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace baseline_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageSetting.xaml
    /// </summary>
    public partial class PageSetting : Page
    {
        private static readonly string connectionString = Properties.Settings.Default.connectStrCur;
        public PageSetting()
        {
            InitializeComponent();
            langCombo.SelectedIndex = Properties.Settings.Default.languageApp;
            conectionStr.Text = Properties.Settings.Default.connectStrCur;
            GetUserInfo();
        }

        private void GetUserInfo()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_ReturnInfoUsers", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ID", Properties.Settings.Default.currentUserID));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userNameField.Text = reader.GetString(0);
                        userLoginField.Text = reader.GetString(1);
                    }
                }
            }
        }

        private void NewData_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.currentUserID <= -1 || userLoginField.Text.Length == 0 || userNameField.Text.Length == 0 || newPasswordField.Password.ToString() != comfirmPasswordField.Password.ToString())
            {
                ErrorBox errorBox = new ErrorBox("Incorrect Data");
                if (errorBox.ShowDialog() == true)
                    MessageBox.Show("i");
            }
            else
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
                        connection.Close();
                        ChangeData();
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        ErrorBox errorBox = new ErrorBox("Auth error");
                        if (errorBox.ShowDialog() == true)
                            MessageBox.Show("i");
                        connection.Close();
                    }
                }
            }
        }

        private void ChangeData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_UpdateUsers", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ID", Properties.Settings.Default.currentUserID));
                command.Parameters.Add(new SqlParameter("@Name", userNameField.Text));
                command.Parameters.Add(new SqlParameter("@Login", userLoginField.Text));
                command.Parameters.Add(new SqlParameter("@Password", newPasswordField.Password.ToString()));
                command.Parameters.Add(new SqlParameter("@Admin_state", Properties.Settings.Default.admStatus));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void LanguageChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string lang = (cb.SelectedItem as ComboBoxItem).Tag.ToString();
            var uri = new Uri("Resources/Lang/" + lang + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Properties.Settings.Default.languageApp = cb.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void TextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ErrorBox errorBox = new ErrorBox("Warning\nThe application may be broken!\nChange this setting only if you are confident in your actions", true, true);
            if (errorBox.ShowDialog() == true)
            {
                ConectConfiguration conectConfiguration = new ConectConfiguration();
                if (conectConfiguration.ShowDialog() == true)
                {
                    conectionStr.Text = Properties.Settings.Default.connectStrNew;
                }
            }
        }

        private void NewConStr_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.connectStrCur = conectionStr.Text; 
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
