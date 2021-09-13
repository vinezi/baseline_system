using baseline_system.DialogBox;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace baseline_system.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для NoAuthPage.xaml
    /// </summary>
    public partial class NoAuthPage : Page
    {
        SqlDataAdapter adapter;
        private static readonly string connectionString = Properties.Settings.Default.connectStrCur;
        public NoAuthPage()
        {
            InitializeComponent();
            GetTable();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.connectStrCur = Properties.Settings.Default.connectStrDef;
            Properties.Settings.Default.skip = false;
            Properties.Settings.Default.Save();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void DataLoad(string BDName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("select * from " + BDName, connection);
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                connection.Close();
            }
        }

        private void GetTable()
        {
            string comString = "USE " + GetCatalog() + ";SELECT * FROM sys.Tables;";

            List<string> TableList = new List<string>{ };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(comString, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TableList.Add(reader.GetString(0));
                        }
                        connection.Close();
                    }
                    else
                    {
                        ErrorBox errorBox = new ErrorBox("23 error");
                        if (errorBox.ShowDialog() == true)
                            MessageBox.Show("i");
                        connection.Close();
                    }

                }
                catch (System.Exception ex)
                {
                    ErrorBox errorBox = new ErrorBox("Connection error");
                    if (errorBox.ShowDialog() == true)
                        MessageBox.Show("i");
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            TableList.RemoveAt(0);
            cBox.ItemsSource = TableList;
        }

        private string GetCatalog ()
        {
            Match match = Regex.Match(Properties.Settings.Default.connectStrCur, "Catalog=(.*?);");
            return match.Groups[1].Value;
        }

        private void cBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataLoad(cBox.SelectedValue.ToString());
        }
    }
}
