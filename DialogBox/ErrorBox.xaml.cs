using System.Windows;

namespace baseline_system.DialogBox
{
    /// <summary>
    /// Логика взаимодействия для ErrorBox.xaml
    /// </summary>
    public partial class ErrorBox : Window
    {
        public ErrorBox(string text)
        {
            InitializeComponent();
            textError.Text = text;
        }

        public ErrorBox(string text, bool colorRed, bool acceptFlag)
        { 
            InitializeComponent();
            okBtn.Content = "cancel";
            if (colorRed)
            {
                textError.Visibility = Visibility.Collapsed;
                textWarnning.Visibility = Visibility.Visible;
                textWarnning.Text = text;
            }
            if (acceptFlag)
            {
                acceptBtn.Visibility = Visibility.Visible;
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
