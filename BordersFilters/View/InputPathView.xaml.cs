using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace BordersFilters.View
{
    /// <summary>
    /// Логика взаимодействия для InputPathView.xaml
    /// </summary>
    public partial class InputPathView : UserControl
    {
        public InputPathView()
        {
            InitializeComponent();
        }

        private void OnPathChoose(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                InitialDirectory = Directory.GetCurrentDirectory() + @"\Resours",
                Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp;*.gif;*tiff)|*.png;*.jpeg;*.jpg;*.bmp;*.gif;*tiff|All files (*.*)|*.*"
                
            };
            if (openFileDialog.ShowDialog() == true)
            {
                InputFilePath.Text = openFileDialog.FileName;
            }
        }
    }
}
