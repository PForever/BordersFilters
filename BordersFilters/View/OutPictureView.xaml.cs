using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using ViewModel.Additional;


namespace BordersFilters.View
{
    /// <summary>
    /// Логика взаимодействия для OutPictureView.xaml
    /// </summary>
    public partial class OutPictureView : UserControl
    {
        public LoupControl Loup;

        public OutPictureView()
        {
            InitializeComponent();
            Loup = new LoupControl();
        }

        private void OutputImage_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is Image image) || !Loup.IsActive) return;
            Loup.SetLoup(e.GetPosition(image), image);
            e.Handled = true;
        }

        private void OutputImage_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!(sender is Image image) || !Loup.IsActive) return;
            Loup.OnMouseEnter((Grid)image.Parent, image);
            e.Handled = true;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Loup.Dispose();
        }

        private void LoupActivate_Changed(object sender, RoutedEventArgs e)
        {
            Loup.IsActive = !Loup.IsActive;
        }
    }
}
