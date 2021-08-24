using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace baseline_system.DialogBox
{
    /// <summary>
    /// Логика взаимодействия для TaskEdit.xaml
    /// </summary>
    public partial class TaskEdit : Window
    {
        SqlDataAdapter adapter;
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public TaskEdit(String ID)
        {
            InitializeComponent();
            deleteBtn.IsEnabled = false;
            changeBtn.IsEnabled = false;
            IDEdit.Text = ID;
            UpdateCombo();
        }

        public TaskEdit(String ID, String Theme, String State_task, String ID_project, String ID_users, String About)
        {
            InitializeComponent();
            IDEdit.Text = ID;
            ThemeEdit.Text = Theme;
            StateTaskEdit.Text = State_task;
            IDProjectEdit.SelectedValue = ID_project;
            IDUserEdit.SelectedValue = ID_users;
            AboutEdit.Text = About;
            UpdateCombo();
        }
        private void UpdateCombo()
        {
            using (SqlConnection connectionCb = new SqlConnection(connectionString))
            {
                connectionCb.Open();
                DataTable dtCb = new DataTable();
                adapter = new SqlDataAdapter("select ID, Name_project from Project", connectionCb);
                adapter.Fill(dtCb);
                IDProjectEdit.SelectedValuePath = "ID";
                IDProjectEdit.DisplayMemberPath = "Name_project";
                IDProjectEdit.ItemsSource = dtCb.DefaultView;
                connectionCb.Close();
            }

            using (SqlConnection connectionCb = new SqlConnection(connectionString))
            {
                connectionCb.Open();
                DataTable dtCb = new DataTable();
                adapter = new SqlDataAdapter("select ID, Name from Users", connectionCb);
                adapter.Fill(dtCb);
                IDUserEdit.SelectedValuePath = "ID";
                IDUserEdit.DisplayMemberPath = "Name";
                IDUserEdit.ItemsSource = dtCb.DefaultView;
                connectionCb.Close();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (IDEdit.Text.Length != 0 && ThemeEdit.Text.Length != 0 && StateTaskEdit.Text.Length != 0 && IDProjectEdit.Text.Length != 0)
                {
                    SqlCommand cmd = new SqlCommand("insert into Task values(@Theme,@State_task,@ID_project,@ID_users,@About)", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Theme", ThemeEdit.Text);
                    cmd.Parameters.AddWithValue("@State_task", StateTaskEdit.Text);
                    cmd.Parameters.AddWithValue("@ID_project", IDProjectEdit.SelectedValue);
                    cmd.Parameters.AddWithValue("@ID_users", IDUserEdit.SelectedValue);
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
                    SqlCommand command = new SqlCommand("sp_DeleteTask", connection); //Вместо запроса передается название процедуры
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
            if (IDEdit.Text.Length != 0 && ThemeEdit.Text.Length != 0 && StateTaskEdit.Text.Length != 0 && IDProjectEdit.Text.Length != 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_UpdateTask", connection); //Вместо запроса передается название процедуры
                    command.CommandType = System.Data.CommandType.StoredProcedure; //Указывается тип команды
                    command.Parameters.Add(new SqlParameter("@ID", Int32.Parse(IDEdit.Text)));
                    command.Parameters.Add(new SqlParameter("@Theme", ThemeEdit.Text));
                    command.Parameters.Add(new SqlParameter("@State_task", StateTaskEdit.Text));
                    command.Parameters.Add(new SqlParameter("@ID_project", IDProjectEdit.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@ID_users", IDUserEdit.SelectedValue));
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
