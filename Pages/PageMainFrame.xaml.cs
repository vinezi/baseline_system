using baseline_system.Pages.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //MainFrame.Content = new PageMainFrame();
            MainFrame.Content = new PageSetting();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Content = new ();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PageAdminPanel();
        }
    }
}
