using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace baseline_system.DialogBox
{
    /// <summary>
    /// Логика взаимодействия для ProjectEdit.xaml
    /// </summary>
    public partial class ProjectEdit : Window
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public ProjectEdit(String ID)
        {
            InitializeComponent();
            deleteBtn.IsEnabled = false;
            changeBtn.IsEnabled = false;
            IDEdit.Text = ID;
        }

        public ProjectEdit(String ID, String Name_project, String State_project, String Author, String About)
        {
            InitializeComponent();
            IDEdit.Text = ID;
            NameProjectEdit.Text = Name_project;
            StateProjectEdit.Text = State_project;
            AuthorEdit.Text = Author;
            AboutEdit.Text = About;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (IDEdit.Text.Length != 0 && NameProjectEdit.Text.Length != 0 && StateProjectEdit.Text.Length != 0 && AuthorEdit.Text.Length != 0)
                {
                    SqlCommand cmd = new SqlCommand("insert into Project values(@Name_project,@State_project,@Author,@About)", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Name_project", NameProjectEdit.Text);
                    cmd.Parameters.AddWithValue("@State_project", StateProjectEdit.Text);
                    cmd.Parameters.AddWithValue("@Author", AuthorEdit.Text);
                    cmd.Parameters.AddWithValue("@About", AboutEdit.Text);
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
                    SqlCommand command = new SqlCommand("sp_DeleteProject", connection); //Вместо запроса передается название процедуры
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
            if (IDEdit.Text.Length != 0 && NameProjectEdit.Text.Length != 0 && StateProjectEdit.Text.Length != 0 && AuthorEdit.Text.Length != 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_UpdateProject", connection); //Вместо запроса передается название процедуры
                    command.CommandType = System.Data.CommandType.StoredProcedure; //Указывается тип команды
                    command.Parameters.Add(new SqlParameter("@ID", Int32.Parse(IDEdit.Text)));
                    command.Parameters.Add(new SqlParameter("@Name_project", NameProjectEdit.Text));
                    command.Parameters.Add(new SqlParameter("@State_project", StateProjectEdit.Text));
                    command.Parameters.Add(new SqlParameter("@Author", AuthorEdit.Text));
                    command.Parameters.Add(new SqlParameter("@About", AboutEdit.Text));
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
