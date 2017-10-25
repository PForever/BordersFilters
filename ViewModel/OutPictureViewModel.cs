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

        #region OutImageSourse

        public static readonly DependencyProperty OutImageSourseProperty = DependencyProperty.Register(
            nameof(OutImageSourse), typeof(BitmapSource), typeof(OutPictureViewModel),
            new PropertyMetadata(default(BitmapSource)));

        public BitmapSource OutImageSourse
        {
            get { return (BitmapSource) GetValue(OutImageSourseProperty); }
            set { SetValue(OutImageSourseProperty, value); }
        }

        #endregion

        public OutPictureViewModel()
        {
            _initializeViewModel?.Invoke(this);
        }
    }
}
