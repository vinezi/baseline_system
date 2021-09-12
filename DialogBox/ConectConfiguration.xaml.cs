using System.Windows;

namespace baseline_system.DialogBox
{
    /// <summary>
    /// Логика взаимодействия для ConectConfiguration.xaml
    /// </summary>
    public partial class ConectConfiguration : Window
    {
        public ConectConfiguration()
        {
            InitializeComponent();
            SecurityField.SelectedIndex = 1;
            CurrentConecttiotStr.Text = "";
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentConecttiotStr.Text.Length > 13)
            {
                Properties.Settings.Default.connectStrNew = CurrentConecttiotStr.Text;
                Properties.Settings.Default.connectStrCur = Properties.Settings.Default.connectStrNew;
                Properties.Settings.Default.skip = skipCheckBox.IsChecked.Value;
                Properties.Settings.Default.Save();
                this.DialogResult = true;
            }
            else
            {
                ErrorBox errorBox = new ErrorBox("Incorrect Data");
                if (errorBox.ShowDialog() == true)
                    MessageBox.Show("i");
            }
        }

        private void MaskRB_Checked(object sender, RoutedEventArgs e)
        {
            if(MyStrField.IsVisible)
                MyStrField.Visibility = Visibility.Collapsed;
            if(!mask.IsVisible)
                mask.Visibility = Visibility.Visible;
        }

        private void MyStrRB_Checked(object sender, RoutedEventArgs e)
        {
            if (mask.IsVisible)
                mask.Visibility = Visibility.Collapsed;
            if (!MyStrField.IsVisible)
                MyStrField.Visibility = Visibility.Visible;
        }

        private void Field_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CurrentConecttiotStr.Text = "Data Source=" + SourseField.Text + "; Initial Catalog =" + CatalogField.Text + ";Integrated Security=" + SecurityField.Text + "\"";
        }

        private void SecurityField_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CurrentConecttiotStr.Text = "Data Source=" + SourseField.Text + "; Initial Catalog =" + CatalogField.Text + ";Integrated Security=" + SecurityField.Text + "\"";
        }

        private void MyStrField_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CurrentConecttiotStr.Text = MyStrField.Text;
        }
    }
}
