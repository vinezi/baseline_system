using baseline_system.Pages.Admin;
using System;
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
                Properties.Settings.Default.connectStrDef = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            if (Properties.Settings.Default.connectStrCur == "")
                Properties.Settings.Default.connectStrCur = Properties.Settings.Default.connectStrDef;
            Properties.Settings.Default.Save();

            LangSetting();
            if (Properties.Settings.Default.skip)
            {
                MainFrame.Content = new NoAuthPage();
            }
        }
        private void LangSetting()
        {
            string key;
            if (Properties.Settings.Default.languageApp == 0)
                key = "RU";
            else
                key = "EN";
            var uri = new Uri("Resources/Lang/" + key + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
