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
        public InputPictureViewModel InputPictureView { get; set; }
        public OutPictureViewModel OutPictureView { get; set; }
        public InputPathViewModel InputPathView { get; set; }
        public ChoseAlgorithmViewModel ChoseAlgorithmView { get; set; }
        #endregion

	    #region Command

	    public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
	        nameof(Start), typeof(Command), typeof(ViewModel), new PropertyMetadata(default(Command)));
	    public Command Start
	    {
	        get { return (Command)GetValue(StartProperty); }
	        set { SetValue(StartProperty, value); }
	    }

	    #endregion

        public ViewModel()
		{
            #region Initialize

		    InputPictureViewModel.InitializeViewModel += (sunder) => InputPictureView = sunder;
		    OutPictureViewModel.InitializeViewModel += (sunder) => OutPictureView = sunder;
		    InputPathViewModel.InitializeViewModel += (sunder) => InputPathView = sunder;
		    ChoseAlgorithmViewModel.InitializeViewModel += (sunder) => ChoseAlgorithmView = sunder;
		    var model = new Initialization();
		    #endregion

            //_model.DestinationChanged += (bms) =>
            //{
            //	Dispatcher.Invoke(() => OutPictureViewModel.OnStaticSetValue(bms));
		    //};
            Start = new Command(str =>
			{

				var path = InputPathView.PathValue;
				if (path != null)
				{
				    InputPictureView.SetImage(path);
					model.Path = path;
					model.Operator = (OperatorsEnum)ChoseAlgorithmView.Operation;
					model.Extantion = InputPictureView.Extention;
				    model.RGBOperator = ChoseAlgorithmView.RGBOperator;
				    model.ReapplyCount = ChoseAlgorithmView.ReapplyCount;
                    //new Task(() => _model.Start()).Start();
                    model.Start();
				    OutPictureView.OutImageSource = model.Destination;
				}
			});
		}
	}
}
