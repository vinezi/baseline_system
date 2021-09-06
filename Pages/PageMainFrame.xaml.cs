using baseline_system.DialogBox;
using baseline_system.Pages.Admin;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace baseline_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMainFrame.xaml
    /// </summary>
    public partial class PageMainFrame : Page
    {
        public PageMainFrame()
        {
            InitializeComponent();
            userName.Text = Properties.Settings.Default.currentUserName;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PageSetting();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PageSearch();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.admStatus == true)
            {
                MainFrame.Content = new PageAdminPanel();
            }
            else
            {
                ErrorBox errorBox = new ErrorBox("No access");
                if (errorBox.ShowDialog() == true)
                    _ = MessageBox.Show("i");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(new PageLogin());
        }
    }
}
