using System.Windows;
using System.Windows.Controls;

namespace baseline_system.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для NoAuthPage.xaml
    /// </summary>
    public partial class NoAuthPage : Page
    {
        public NoAuthPage()
        {
            InitializeComponent();
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
    }
}
