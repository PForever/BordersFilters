using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Model;


namespace ViewModel
{
    public class ViewModel : DependencyObject
    {
        #region Controllers
        //public static PictureViewModel PictureView { get; set; }
        //public static InputPathViewModel InputPathView { get; set; }
        //public static ChoseAlgorithmViewModel ChoseAlgorithmView { get; set; }
        #endregion

        private Initialization _model;
        public Command Start
        {
            get { return (Command)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            nameof(Start), typeof(Command), typeof(ViewModel), new PropertyMetadata(default(Command)));

        public ViewModel()
        {
            _model = new Initialization();
            //_model.DestinationChanged += (bms) =>
            //{
            //    Dispatcher.Invoke(() => OutPictureViewModel.OnStaticSetValue(bms));
            //};
            Start = new Command(str =>
            {

                var path = InputPathViewModel.OnStaticGetValue();
                if (path != null)
                {
                    InputPictureViewModel.OnStaticSetValue(path);
                    _model.Path = path;
                    _model.Operator = (OperatorsEnum)ChoseAlgorithmViewModel.OnStaticGetValue();
                    _model.Extantion = InputPictureViewModel.Extention;
                    //new Task(() => _model.Start()).Start();
                    _model.Start();
                    OutPictureViewModel.OnStaticSetValue(_model.Destination);
                }

            });
        }
    }
}
