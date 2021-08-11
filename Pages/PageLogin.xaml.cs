using baseline_system.Pages.Admin;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace baseline_system.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void LoginUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageMainFrame());
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminPanel());
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageRegistration());
        }
    }
}
