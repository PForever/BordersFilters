using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using Model;
using ViewModel.Additional;


namespace ViewModel
{
	public class ViewModel : DependencyObject
	{
        #region Event Invokers

	    //public static event EventHandler<EventArgs> SaveCompleted;
	    //private static void OnSaveCompleted(EventArgs e)
	    //{
	    //    SaveCompleted?.Invoke(null, e);
	    //}

        #endregion

        #region Controllers
        public OutPictureViewModel OutPictureView { get; set; }
        public InputPathViewModel InputPathView { get; set; }
        public ChoseAlgorithmViewModel ChoseAlgorithmView { get; set; }
        #endregion

        #region Message

	    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
	        nameof(messageQueue), typeof(SnackbarMessageQueue), typeof(ViewModel), new PropertyMetadata(default(SnackbarMessageQueue)));

	    public SnackbarMessageQueue messageQueue
        {
	        get { return (SnackbarMessageQueue)GetValue(MessageProperty); }
	        set { SetValue(MessageProperty, value); }
	    }

        #endregion

        #region Start Command

        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
	        nameof(Start), typeof(Command), typeof(ViewModel), new PropertyMetadata(default(Command)));
	    public Command Start
	    {
	        get { return (Command)GetValue(StartProperty); }
	        set { SetValue(StartProperty, value); }
	    }

        #endregion

	    #region Save Command

	    public static readonly DependencyProperty SaveProperty = DependencyProperty.Register(
	        nameof(Save), typeof(Command), typeof(ViewModel), new PropertyMetadata(default(Command)));

	    public Command Save
        {
	        get { return (Command)GetValue(SaveProperty); }
	        set { SetValue(SaveProperty, value); }
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

		    OutPictureViewModel.InitializeViewModel += (sunder) => OutPictureView = sunder;
		    InputPathViewModel.InitializeViewModel += (sunder) => InputPathView = sunder;
		    ChoseAlgorithmViewModel.InitializeViewModel += (sunder) => ChoseAlgorithmView = sunder;
		    messageQueue = new SnackbarMessageQueue(new TimeSpan((long)Math.Pow(10, 6.3)));

            #endregion

            Start = new Command(() =>
            {
                if (ChoseAlgorithmView.ChosedOperatorsList.Count == 0)
                {                 
                    messageQueue.Enqueue("Выберите алгоритмы!");
                    return;
                }
                var model = new Initialization();          
                var path = InputPathView.PathValue;
                if (File.Exists(path))
                {
                    // Присвоение операторов и обновление параметров модели
                    model.RGBOperator = ChoseAlgorithmView.RGBOperator;
                    model.MatrixSize = ChoseAlgorithmView.MatrixSize;
                    model.Sigma = ChoseAlgorithmView.Sigma;
                    model.Path = path;
                    OutPictureView.TabControls = new ObservableCollection<TabControl>();
                    model.Operators = new Collection<OperatorsEnum>();

                    // Работа с таб контролами и наборами операторов
                    TabControl.BaseOfSavingDirectory = ChoseAlgorithmView.OutPathValue;
                    TabControl.SetInputImage(path);
                    var operatorsDictionary = TabControl.SetOperatorsDictionary(ChoseAlgorithmView.OperatorsList);
                    foreach (var oper in ChoseAlgorithmView.ChosedOperatorsList)
                    {                    
                        if (operatorsDictionary.TryGetValue(oper, out var searchIndex))
                        {
                            model.Operators.Add(searchIndex);
                        }
                    }

                    //Работа с потоками
                    Collection<Func<Dictionary<OperatorsEnum, BitmapSource>>> preDestination = new Collection<Func<Dictionary<OperatorsEnum, BitmapSource>>>();
                    model.PreDestination = preDestination;

                    Task.Run(() =>
                    {
                        model.Start();
                        Dispatcher.Invoke(() =>
                        {
                            foreach (var func in model.PreDestination)
                            {
                                OutPictureView.TabControls.Add(new TabControl(func()));
                            }
                            messageQueue.Enqueue("Обработано!");
                        });                     
                    });
                }           
            });	
            Save = new Command(() =>
            {
                if (OutPictureView.TabControls == null)
                {
                    messageQueue.Enqueue("Нет данных для сохранения!");
                    return;
                }
                TabControl.BaseOfSavingDirectory = ChoseAlgorithmView.OutPathValue;
                foreach (var tabControl in OutPictureView.TabControls)
                {
                    tabControl.SaveImage();    
                }
                messageQueue.Enqueue("Сохранено!");
            });
        }
    }
}
