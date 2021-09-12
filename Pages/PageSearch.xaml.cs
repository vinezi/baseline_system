using baseline_system.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace baseline_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageSearch.xaml
    /// </summary>
    public partial class PageSearch : Page
    {
        private static readonly string connectionString = Properties.Settings.Default.connectStrCur;
        ObservableCollection<ResultModel> Result = new ObservableCollection<ResultModel>();
        public PageSearch()
        {
            InitializeComponent();
            searchSelector.SelectedIndex = 0;


            //Result.Add(new ResultModel
            //{
            //    ID = 0,
            //    RESULT = "r)",
            //    INF = "reader.GetString(2)",
            //});
            
            //ListBooks.ItemsSource = Result;
        }

        private void searchBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            searchSel();
        }

        private void searchSel()
        {
            Result.Clear();
            string key = "";
            switch (searchSelector.SelectedIndex)
            {
                case 0: //PR_NAME
                    key = "sp_SearchProjectName";
                    break;
                case 1: //PR_AUTHOR
                    key = "sp_SearchProjectAuthor";
                    break;
                case 2: //PR_STATE
                    key = "sp_SearchProjectState";
                    break;

                case 3: //TSK_THEME
                    key = "sp_SearchTaskTheme";
                    break;
                case 4: //TSK_STATE
                    key = "sp_SearchTaskState";
                    break;
                default:
                    break;
            }
            Search(key);
        }

        private void Search(string key)
        {
            if (search.Text.Length < 45)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    BadResultBook.Visibility = Visibility.Collapsed;
                    connection.Open();
                    SqlCommand command = new SqlCommand(key, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Text", search.Text));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Result.Add(new ResultModel
                                {
                                    ID = reader.GetInt32(0),
                                    RESULT = reader.GetString(1),
                                    INF = reader.GetString(2),
                                });
                            }
                            catch (System.Exception)
                            {
                                Result.Add(new ResultModel
                                {
                                    ID = reader.GetInt32(0),
                                    RESULT = reader.GetString(1),
                                    INF = "",
                                });
                            }
                            
                        }
                    }
                    ListBooks.ItemsSource = Result;
                    connection.Close();
                }
                if (ListBooks.Items.Count == 0)
                {
                    BadResultBook.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
