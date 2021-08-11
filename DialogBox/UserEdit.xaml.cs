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
        SqlDataAdapter adapter;
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public UserEdit(bool isAuto, String ID)
        {
            InitializeComponent();
            if (isAuto)
            {
                deleteBtn.IsEnabled = false;
                changeBtn.IsEnabled = false;
            }
            IDEdit.Text = ID;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            /*using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (GenresMW.Text.Length != 0)
                {
                    SqlCommand cmd = new SqlCommand("insert into Project values(@Genres)", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Genres", GenresMW.Text);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    ShowProject();
                }
                else
                {
                    MessageBox.Show("Введите значения");
                }
            }
            */
            this.DialogResult = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    /*SqlCommand command = new SqlCommand("sp_DeleteGenres", connection); //Вместо запроса передается название процедуры
                    command.CommandType = System.Data.CommandType.StoredProcedure; //Указывается тип команды
                    command.Parameters.Add(new SqlParameter("@ID", Int32.Parse(IDEdit.Text))); //Передаются параметры
                    command.ExecuteNonQuery();*/
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
            /*if (IdMW.Text.Length > 0 && GenresMW.Text.Length != 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_UpdateGenres", connection); //Вместо запроса передается название процедуры
                    command.CommandType = System.Data.CommandType.StoredProcedure; //Указывается тип команды
                    command.Parameters.Add(new SqlParameter("@ID", Int32.Parse(IdMW.Text))); //Передаются параметры
                    command.Parameters.Add(new SqlParameter("@Genres", GenresMW.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            */
            this.DialogResult = true;
        }
    }
}
