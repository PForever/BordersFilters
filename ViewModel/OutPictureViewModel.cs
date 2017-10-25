using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

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

        public static readonly DependencyProperty OutImageSourceProperty = DependencyProperty.Register(
            nameof(OutImageSource), typeof(BitmapSource), typeof(OutPictureViewModel),
            new PropertyMetadata(default(BitmapSource)));

        public BitmapSource OutImageSource
        {
            get { return (BitmapSource) GetValue(OutImageSourceProperty); }
            set { SetValue(OutImageSourceProperty, value); }
        }

        #endregion

        public OutPictureViewModel()
        {
            _initializeViewModel?.Invoke(this);
        }
    }
}
