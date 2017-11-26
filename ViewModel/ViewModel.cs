using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Model;
using ViewModel.Additional;


namespace ViewModel
{
	public class ViewModel : DependencyObject, INotifyPropertyChanged
    {

        #region Dialog 

        private bool _isDialogOpen = false;
        private object _dialogContent;

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                if (_isDialogOpen == value) return;
                _isDialogOpen = value;
                OnPropertyChanged();
            }
        }

        public object DialogContent
        {
            get { return _dialogContent; }
            set
            {
                if (_dialogContent == value) return;
                _dialogContent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Event Invokers

        public event Action CalculationStoped;

        protected virtual void OnCalculationStoped()
        {
            CalculationStoped?.Invoke();
        }

        public event Action CalculationStarted;
	    protected virtual void OnCalculationStarted()
	    {
	        CalculationStarted?.Invoke();
	    }

        public event Action<string> SomethingHappen;
	    protected virtual void OnSomethingHappen(string obj)
	    {
	        SomethingHappen?.Invoke(obj);
	    }

        #endregion

        #region Controllers
        public OutPictureViewModel OutPictureView { get; set; }
        public InputPathViewModel InputPathView { get; set; }
        public ChoseAlgorithmViewModel ChoseAlgorithmView { get; set; }
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

        public ViewModel()
		{
            #region Initialize

		    OutPictureViewModel.InitializeViewModel += (sunder) => OutPictureView = sunder;
		    InputPathViewModel.InitializeViewModel += (sunder) => InputPathView = sunder;
		    ChoseAlgorithmViewModel.InitializeViewModel += (sunder) => ChoseAlgorithmView = sunder;
		   // messageQueue = new SnackbarMessageQueue(new TimeSpan((long)Math.Pow(10, 6.3)));

            #endregion

            Start = new Command(() =>
            {
                //Инициализация 

                if (ChoseAlgorithmView.ChosedOperatorsList.Count == 0)
                {
                    OnSomethingHappen("Выберите алгоритмы!");
                    return;
                }
                var path = InputPathView.PathValue;
                if (!File.Exists(path))
                {
                    OnSomethingHappen("Выберите путь!");
                    return;                        
                }

                var model = new Initialization
                {
                    RGBOperator = ChoseAlgorithmView.RGBOperator,
                    MatrixSize = ChoseAlgorithmView.MatrixSize,
                    Sigma = ChoseAlgorithmView.Sigma,
                    Path = path
                };

                // Присвоение операторов и обновление параметров модели
                OutPictureView.TabControls = new ObservableCollection<TabControlVm>();
                model.Operators = new Collection<OperatorsEnum>();

                // Работа с таб контролами и наборами операторов
                TabControlVm.BaseOfSavingDirectory = ChoseAlgorithmView.OutPathValue;
                TabControlVm.SetInputImage(path);
                var operatorsDictionary = TabControlVm.SetOperatorsDictionary(ChoseAlgorithmView.OperatorsList);
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

                OnCalculationStarted();

                Task.Run(() =>
                {
                    model.Start();
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var func in model.PreDestination)
                        {
                            OutPictureView.TabControls.Add(new TabControlVm(func()));
                        }
                        OnCalculationStoped();
                    });                     
                });
            });	

            Save = new Command(() =>
            {
                if (OutPictureView.TabControls == null)
                {
                    OnSomethingHappen("Нет данных для сохранения!");
                    return;
                }
                TabControlVm.BaseOfSavingDirectory = ChoseAlgorithmView.OutPathValue;
                foreach (var tabControl in OutPictureView.TabControls)
                {
                    tabControl.SaveImage();    
                }
                OnSomethingHappen("Сохранено!");
            });
        }

        #region Notify

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
