using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace baseline_system.DialogBox
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Window
    {
        private static readonly string connectionString = Properties.Settings.Default.connectStrCur;
        public UserEdit(String ID)
        {
            InitializeComponent();
            deleteBtn.IsEnabled = false;
            changeBtn.IsEnabled = false;
            IDEdit.Text = ID;
        }

        public UserEdit(String ID, String Name, String Login, String Password, String Admin_state)
        {
            InitializeComponent();
            IDEdit.Text = ID;
            NameEdit.Text = Name;
            LoginEdit.Text = Login;
            PasswordEdit.Text = Password;
            AdminStateEdit.Text = Admin_state;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (IDEdit.Text.Length != 0 && NameEdit.Text.Length != 0 && LoginEdit.Text.Length != 0 && PasswordEdit.Text.Length != 0 && AdminStateEdit.Text.Length != 0)
                {
                    SqlCommand cmd = new SqlCommand("insert into Users values(@Name,@Login,@Password,@Admin_state)", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Name", NameEdit.Text);
                    cmd.Parameters.AddWithValue("@Login", LoginEdit.Text);
                    cmd.Parameters.AddWithValue("@Password", PasswordEdit.Text);
                    cmd.Parameters.AddWithValue("@Admin_state", AdminStateEdit.Text);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Введите значения");
                }
            }
            this.DialogResult = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_DeleteUsers", connection); //Вместо запроса передается название процедуры
                    command.CommandType = System.Data.CommandType.StoredProcedure; //Указывается тип команды
                    command.Parameters.Add(new SqlParameter("@ID", Int32.Parse(IDEdit.Text))); //Передаются параметры
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления, используется в других таблицах");
            }
            this.DialogResult = true;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (IDEdit.Text.Length != 0 && NameEdit.Text.Length != 0 && LoginEdit.Text.Length != 0 && PasswordEdit.Text.Length != 0 && AdminStateEdit.Text.Length != 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_UpdateUsers", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ID", Int32.Parse(IDEdit.Text)));
                    command.Parameters.Add(new SqlParameter("@Name", NameEdit.Text));
                    command.Parameters.Add(new SqlParameter("@Login", LoginEdit.Text));
                    command.Parameters.Add(new SqlParameter("@Password", PasswordEdit.Text));
                    command.Parameters.Add(new SqlParameter("@Admin_state", AdminStateEdit.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите значения");
            }
            this.DialogResult = true;
        }
    }
}
