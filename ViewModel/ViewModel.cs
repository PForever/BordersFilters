using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
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

            #endregion

            Start = new Command(() =>
            {
                var model = new Initialization();
                var path = InputPathView.PathValue;
                if (File.Exists(path))
                {
                    TabControl.SaveCompleted += (sender, e) =>
                    {
                        MessageBox.Show("Завершено!");
                    };
                    TabControl.BaseOfSavingDirectory = ChoseAlgorithmView.OutPathValue;
                    TabControl.SetInputImage(path);
                    var OperatorsDictionary = TabControl.SetOperatorsDictionary(ChoseAlgorithmView.OperatorsList);
                    OutPictureView.TabControls = new ObservableCollection<TabControl>();
                    model.Path = path;
                    model.Operators = new Collection<OperatorsEnum>();
                    foreach (var oper in ChoseAlgorithmView.ChosedOperatorsList)
                    {                    
                        if (OperatorsDictionary.TryGetValue(oper, out var searchIndex))
                        {
                            model.Operators.Add(searchIndex);
                        }
                    }
                    Collection<Func<Dictionary<OperatorsEnum, BitmapSource>>> preDestination = new Collection<Func<Dictionary<OperatorsEnum, BitmapSource>>>();
                    model.PreDestination = preDestination;
                    model.RGBOperator = ChoseAlgorithmView.RGBOperator;
                    model.ReapplyCount = ChoseAlgorithmView.ReapplyCount;
                    Task.Run(() =>
                    {
                        model.Start();
                        Dispatcher.Invoke(() =>
                        {
                            foreach (var func in model.PreDestination)
                            {
                                OutPictureView.TabControls.Add(new TabControl(func()));
                            }
                        });
                    });
                    ChoseAlgorithmView.ChosedOperatorsList.Clear();
                    ChoseAlgorithmView.SelectAllOperator = false;
                }
            });
		}
	}
}
