using baseline_system.DialogBox;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace baseline_system.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для TaskPanel.xaml
    /// </summary>
    public partial class TaskPanel : Page
    {
        SqlDataAdapter adapter;
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public TaskPanel()
        {
            InitializeComponent();
            DataLoad();
        }

        private void DataLoad()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select * from Task", connection);
                adapter.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                connection.Close();
            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedRow = (DataRowView)datagrid.SelectedItem;
                if (selectedRow != null)
                    dialog(false, selectedRow.Row[0].ToString());
            }
            catch (System.Exception)
            {
                //MessageBox.Show(ex.ToString());
                dialog(true, "auto");
            }
            Keyboard.ClearFocus();
            datagrid.SelectedIndex = -1;
        }

        private void dialog(bool isAuto, string argument)
        {
            TaskEdit taskEdit= new TaskEdit(isAuto, argument);
            if (taskEdit.ShowDialog() == true)
            {
                //MessageBox.Show("true");
                DataLoad();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataLoad();
        }
    }


}
