using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Model;
using ViewModel.Additional;


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

	    #region Window

	    #region WindowsHight

	    public static readonly DependencyProperty WindowsWidthProperty = DependencyProperty.Register(
	        nameof(WindowsWidth), typeof(double), typeof(ViewModel), new PropertyMetadata(300.0));

	    public double WindowsWidth
	    {
	        get { return (double) GetValue(WindowsWidthProperty); }
	        set { SetValue(WindowsWidthProperty, value); }
	    }

	    #endregion
	    #region WindowHeight

	    public static readonly DependencyProperty WindowHeightProperty = DependencyProperty.Register(
	        nameof(WindowHeight), typeof(double), typeof(ViewModel), new PropertyMetadata(300.0));

	    public double WindowHeight
	    {
	        get { return (double) GetValue(WindowHeightProperty); }
	        set { SetValue(WindowHeightProperty, value); }
	    }

        #endregion

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

                Start = new Command(() =>
                {

                    var path = InputPathView.PathValue;
                    if (File.Exists(path))
                    {
                        InputPictureView.SetImage(path);
                        model.Path = path;
                        model.Operators = new Collection<OperatorsEnum>();
                        
                        foreach (var oper in ChoseAlgorithmView.ChosedOperatorsList)
                        {
                            var searchIndex = ChoseAlgorithmView.OperatorsList
                                .Select((item, index) => new {Item = item, Index = index}).First(i => i.Item == oper)
                                .Index;
                            model.Operators.Add((OperatorsEnum)searchIndex);
                        }
                        model.Extantion = InputPictureView.Extention;
                        model.RGBOperator = ChoseAlgorithmView.RGBOperator;
                        model.ReapplyCount = ChoseAlgorithmView.ReapplyCount;
                        model.Start();

                        for (var i = 0; i < model.Destination.Count; i++)
                        {
                            OutPictureView.TabControls.Add(new TabControl(model.Destination[i], ChoseAlgorithmView.ChosedOperatorsList[i]));
                        }
                    }
                });
		}
	}
}
