using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace ViewModel
{
	public class ChoseAlgorithmViewModel : DependencyObject
	{

	    #region Events and Invocators

	    public static event EventHandler SelectAllOperatorsEvent;
	    public static void OnSelectAllOperatorsEvent()
	    {
	        SelectAllOperatorsEvent?.Invoke(null, EventArgs.Empty);
	    }

	    public static event EventHandler DeleteChoosedOperatorsEvent;
	    public static void OnDeleteChoosedOperatorsEvent()
	    {
	        DeleteChoosedOperatorsEvent?.Invoke(null, EventArgs.Empty);
	    }

	    #endregion
        #region Initialize
        public static event Action<ChoseAlgorithmViewModel> InitializeViewModel
	    {
	        add => _initializeViewModel += value;
	        remove => _initializeViewModel -= value;
	    }

	    private static event Action<ChoseAlgorithmViewModel> _initializeViewModel;
	    #endregion
        #region OperatorsList and OperatorListChoosed
        public static readonly DependencyProperty OperatorsListProperty = DependencyProperty.Register(
			nameof(OperatorsList), typeof(ICollection<string>), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(default(ICollection<string>)));

		public ICollection<string> OperatorsList
		{
			get { return (IList<string>)GetValue(OperatorsListProperty); }
			set { SetValue(OperatorsListProperty, value); }
		}


	    public static readonly DependencyProperty ChoosedOperatorsListProperty = DependencyProperty.Register(
	        nameof(ChosedOperatorsList), typeof(ObservableCollection<string>), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(default(ObservableCollection<string>)));

	    public ObservableCollection<string> ChosedOperatorsList
	    {
	        get => (ObservableCollection<string>)GetValue(ChoosedOperatorsListProperty);
	        set => SetValue(ChoosedOperatorsListProperty, value);
	    }
        #endregion
        #region UsageCount

        public static readonly DependencyProperty UsageCountProperty = DependencyProperty.Register(
	        nameof(UsageCount), typeof(int), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(1));

	    public int UsageCount
        {
	        get { return (int) GetValue(UsageCountProperty); }
	        set { SetValue(UsageCountProperty, value); }
	    }

	    #endregion
	    #region RGBOperator

	    public static readonly DependencyProperty RGBOperatorProperty = DependencyProperty.Register(
	        nameof(RGBOperator), typeof(Boolean), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(false, (o, args) =>
	        {
	            int p = 0;
                
	        }));

	    public Boolean RGBOperator
        {
	        get { return (Boolean) GetValue(RGBOperatorProperty); }
	        set
	        {
	            SetValue(RGBOperatorProperty, value);
	        }
	    }

        #endregion
        #region ListHeight

        public static readonly DependencyProperty ListHeightProperty = DependencyProperty.Register(
	        nameof(ListHeight), typeof(string), typeof(ChoseAlgorithmViewModel), new PropertyMetadata("auto"));

	    public string ListHeight
	    {
	        get { return (string) GetValue(ListHeightProperty); }
	        set { SetValue(ListHeightProperty, value); }
	    }

        #endregion
        #region Show

        public static readonly DependencyProperty ShowProperty = DependencyProperty.Register(
	        nameof(Show), typeof(Command), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(default(Command)));

	    public Command Show
	    {
	        get { return (Command) GetValue(ShowProperty); }
	        set { SetValue(ShowProperty, value); }
	    }

        #endregion
        #region SelectAllOperator

        public static readonly DependencyProperty SelectAllProperty = DependencyProperty.Register(
	        nameof(SelectAllOperator), typeof(Boolean), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(false, (o, args) =>
	        {
	            if ((bool)args.NewValue)
	            {
	                OnSelectAllOperatorsEvent();
                }
	            else
	            {
	                OnDeleteChoosedOperatorsEvent();
	            }
	        }));
        public Boolean SelectAllOperator
	    {
	        get { return (Boolean)GetValue(SelectAllProperty); }
	        set
	        {
	            SetValue(SelectAllProperty, value);
	        }
	    }



        #endregion
        #region SelectAllText
	    public static readonly DependencyProperty SelectAllTextProperty = DependencyProperty.Register(
	        nameof(SelectAllText), typeof(string), typeof(ChoseAlgorithmViewModel), new PropertyMetadata("Выбрать все"));

	    public string SelectAllText
        {
	        get { return (string)GetValue(SelectAllTextProperty); }
	        set
	        {
	            SetValue(SelectAllTextProperty, value);
	        }
	    }


        #endregion
        #region BrowseOutPath
        public string OutPathValue { get; set; }
        #endregion
        #region SetCatalogCommand

        public static readonly DependencyProperty SetCatalogProperty = DependencyProperty.Register(
	        nameof(SetCatalog), typeof(Command), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(default(Command)));

	    public Command SetCatalog
        {
	        get { return (Command)GetValue(SetCatalogProperty); }
	        set { SetValue(SetCatalogProperty, value); }
	    }

        #endregion
        #region MatrixSize

	    public static readonly DependencyProperty MatrixSizeProperty = DependencyProperty.Register(
	        nameof(MatrixSize), typeof(int), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(3));

	    public int MatrixSize
        {
	        get { return (int)GetValue(MatrixSizeProperty); }
	        set { SetValue(MatrixSizeProperty, value); }
	    }

        #endregion
        #region Sigma

        public static readonly DependencyProperty SigmaProperty = DependencyProperty.Register(
	        nameof(Sigma), typeof(double), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(1.41));

	    public double Sigma
        {
	        get { return (double)GetValue(SigmaProperty); }
	        set { SetValue(SigmaProperty, value); }
	    }

	    #endregion

        private bool _showed = true;

        public ChoseAlgorithmViewModel()
        {
            SelectAllOperatorsEvent += SelectAllItems;
            DeleteChoosedOperatorsEvent += DeleteChoose;

            ChosedOperatorsList = new ObservableCollection<string>();
            Show = new Command(() =>
            {
                ListHeight = _showed ? "0" : "auto";
                _showed =! _showed;
            });
            SetCatalog = new Command(ChooseCatalog);
      
            OperatorsList = new[] {
                "Invertion Operator",
                "Identity Operator",
                "Gauss Operator",
                "Kanny Operator",
                "Sobel Operator",
                "Laplas Operator",
                "Laplas-Gauss Operator",
                "Roberts Operator",
                "Prewitt Operator",
            };
		   
            _initializeViewModel?.Invoke(this);
		}

	    #region Methods for Selecting/Choosing

	    public void SelectAllItems(object sender, EventArgs e)
	    {
	        ChosedOperatorsList.Clear();
            foreach (var operation in OperatorsList)
	        {
	            ChosedOperatorsList.Add(operation);
	        }
	        SelectAllText = "Убрать выделение";
	    }

	    public void DeleteChoose(object sender, EventArgs e)
	    {
	        ChosedOperatorsList.Clear();
	        SelectAllText = "Выбрать все";
	    }

	    public void ChooseCatalog()
	    {
	        var openFileDialog = new WinForms.FolderBrowserDialog
	        {
	            RootFolder = System.Environment.SpecialFolder.DesktopDirectory,
	            ShowNewFolderButton = true
	        };
	        if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
	        {
	            OutPathValue = openFileDialog.SelectedPath;
	        }
        }
	    #endregion
    }
}
