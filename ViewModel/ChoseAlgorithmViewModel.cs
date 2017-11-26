﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using ViewModel.Configs;
using WinForms = System.Windows.Forms;

namespace ViewModel
{
	public class ChoseAlgorithmViewModel : DependencyObject, IDisposable
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
	    private static Boolean _RGBValue;
	    public static readonly DependencyProperty RGBOperatorProperty = DependencyProperty.Register(
	        nameof(RGBOperator), typeof(Boolean), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(_RGBValue, (o, args) => _RGBValue = (Boolean) args.NewValue));

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
	    private static int _matrixSizeValue;
        public static readonly DependencyProperty MatrixSizeProperty = DependencyProperty.Register(
	        nameof(MatrixSize), typeof(int), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(_matrixSizeValue, (o, args) => _matrixSizeValue = (int)args.NewValue));

	    public int MatrixSize
        {
	        get { return (int)GetValue(MatrixSizeProperty); }
	        set { SetValue(MatrixSizeProperty, value); }
	    }
        #endregion
        #region Sigma

	    public static double _sigmaValue;
        public static readonly DependencyProperty SigmaProperty = DependencyProperty.Register(
	        nameof(Sigma), typeof(double), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(_sigmaValue, (o, args) => _sigmaValue = (double) args.NewValue));

	    public double Sigma
        {
	        get { return (double)GetValue(SigmaProperty); }
	        set { SetValue(SigmaProperty, value); }
	    }

        #endregion


        private bool _showed = true;

	    #region Configuration
	    private void InitConfig()
	    {
	        OutPathValue = Configurator.Path.PathItems["Output"].Path;
	        RGBOperator = Configurator.Logic.LogicElement.RGB;
	        MatrixSize = Configurator.Logic.LogicElement.Matrix;
	        Sigma = Configurator.Logic.LogicElement.Sigma;
	    }

	    public void Dispose()
	    {
	        Configurator.Path.PathItems["Output"].Path = OutPathValue;
	        Configurator.Logic.LogicElement.RGB = _RGBValue;
	        Configurator.Logic.LogicElement.Matrix = _matrixSizeValue;
	        Configurator.Logic.LogicElement.Sigma = _sigmaValue;
        }

	    #endregion

        public ChoseAlgorithmViewModel()
        {
            InitConfig();

            SelectAllOperatorsEvent += SelectAllItems;
            DeleteChoosedOperatorsEvent += DeleteChoose;

            #region List
            ChosedOperatorsList = new ObservableCollection<string>();
            ChosedOperatorsList.CollectionChanged += ChosedOperatorsListOnCollectionChanged;

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
            #endregion
		   
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
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = true,
                SelectedPath = Path.IsPathRooted(OutPathValue) ? OutPathValue : Directory.GetCurrentDirectory() + @"\Resours"
            };

            if (openFileDialog.ShowDialog() == WinForms.DialogResult.OK)
	        {
	            OutPathValue = openFileDialog.SelectedPath;
	        }
        }
	    private void ChosedOperatorsListOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
	    {
	        if (Equals(OperatorsList.Count, ChosedOperatorsList.Count))
	        {
	            SelectAllOperator = true;
	        }
	    }
        #endregion
    }
}
