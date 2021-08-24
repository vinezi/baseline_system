﻿using baseline_system.DialogBox;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace baseline_system.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Page
    {
        SqlDataAdapter adapter;
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public UserPanel()
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
                adapter = new SqlDataAdapter("select * from Users", connection);
                adapter.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                connection.Close();
            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserEdit userEdit;
            try
            {
                var selectedRow = (DataRowView)datagrid.SelectedItem;
                if (selectedRow != null)
                {
                    userEdit = new UserEdit(selectedRow.Row[0].ToString(), selectedRow.Row[1].ToString(), selectedRow.Row[2].ToString(), selectedRow.Row[3].ToString(), selectedRow.Row[4].ToString());
                    if (userEdit.ShowDialog() == true)
                        DataLoad();
                }
            }
            catch (System.Exception)
            {
                userEdit = new UserEdit("auto");
                if (userEdit.ShowDialog() == true)
                    DataLoad();
            }
            Keyboard.ClearFocus();
            datagrid.SelectedIndex = -1;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) { DataLoad(); }
    }
}
