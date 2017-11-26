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
        #region TabControl

        public static readonly DependencyProperty TabControlsProperty = DependencyProperty.Register(
            nameof(TabControls), typeof(ObservableCollection<TabControlVm>), typeof(OutPictureViewModel),
            new PropertyMetadata(default(ObservableCollection<TabControlVm>)));

        public ObservableCollection<TabControlVm> TabControls
        {
            get => (ObservableCollection<TabControlVm>)GetValue(TabControlsProperty);
            set => SetValue(TabControlsProperty, value);
        }

        #endregion

    public OutPictureViewModel()
        {
            _initializeViewModel?.Invoke(this);
        }
    }
}
