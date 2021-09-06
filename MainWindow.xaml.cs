using System.Configuration;
using System.Windows;


namespace baseline_system
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.connectStrDef == "")
                Properties.Settings.Default.connectStrDef = Properties.Settings.Default.connectStrCur;
            if (Properties.Settings.Default.connectStrCur == "")
                Properties.Settings.Default.connectStrCur = Properties.Settings.Default.connectStrDef;
            Properties.Settings.Default.Save();
        }
    }
}
