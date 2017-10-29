using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModel.Additional;

namespace ViewModel
{
    public class OutPictureViewModel : DependencyObject
    {
        #region Initialize
        public static event Action<OutPictureViewModel> InitializeViewModel
        {
            add => _initializeViewModel += value;
            remove => _initializeViewModel -= value;
        }
        private static event Action<OutPictureViewModel> _initializeViewModel;
        #endregion

        #region OutImageSource

        //public static readonly DependencyProperty OutImageSourceProperty = DependencyProperty.Register(
        //    nameof(OutImageSource), typeof(ObservableCollection<BitmapSource>), typeof(OutPictureViewModel),
        //    new PropertyMetadata(default(ObservableCollection<BitmapSource>)));

        //public ObservableCollection<BitmapSource> OutImageSource
        //{
        //    get => (ObservableCollection<BitmapSource>) GetValue(OutImageSourceProperty);
        //    set => SetValue(OutImageSourceProperty, value);
        //}

        //public static readonly DependencyProperty NameOfAlgorithmProperty = DependencyProperty.Register(
        //    nameof(NameOfAlgorithm), typeof(ObservableCollection<string>), typeof(OutPictureViewModel), new PropertyMetadata(default(ObservableCollection<string>)));

        //public ObservableCollection<string> NameOfAlgorithm
        //{
        //    get => (ObservableCollection<string>)GetValue(NameOfAlgorithmProperty);
        //    set => SetValue(NameOfAlgorithmProperty, value);
        //}
    #endregion

    public static readonly DependencyProperty TabControlsProperty = DependencyProperty.Register(
        nameof(TabControls), typeof(ObservableCollection<TabControl>), typeof(OutPictureViewModel),
        new PropertyMetadata(default(ObservableCollection<TabControl>)));

    public ObservableCollection<TabControl> TabControls
    {
        get => (ObservableCollection<TabControl>)GetValue(TabControlsProperty);
        set => SetValue(TabControlsProperty, value);
    }

    public OutPictureViewModel()
        {
            //NameOfAlgorithm = new ObservableCollection<string>();
            //OutImageSource = new ObservableCollection<BitmapSource>();
            TabControls = new ObservableCollection<TabControl>();
            _initializeViewModel?.Invoke(this);
        }
    }
}
