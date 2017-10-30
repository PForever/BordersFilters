﻿using System;
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
	    #region ReapplyCount

        public static readonly DependencyProperty ReapplyCountProperty = DependencyProperty.Register(
	        nameof(ReapplyCount), typeof(int), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(1));

	    public int ReapplyCount
	    {
	        get { return (int) GetValue(ReapplyCountProperty); }
	        set { SetValue(ReapplyCountProperty, value); }
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
	        nameof(ListHeight), typeof(string), typeof(ChoseAlgorithmViewModel), new PropertyMetadata("0"));

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
	    public static readonly DependencyProperty OutPathValueProperty = DependencyProperty.Register(
	        nameof(OutPathValue), typeof(string), typeof(InputPathViewModel), new PropertyMetadata(Directory.GetCurrentDirectory() +
	                                                                                            @"\..\..\Resours\"));

	    public string OutPathValue
        {
	        get
	        {
	            return (string)GetValue(OutPathValueProperty);
	        }
	        set
	        {
	            SetValue(OutPathValueProperty, value);
	        }
	    }
        #endregion

        private bool _showed = false;


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
            OperatorsList = new[] {
                "Brightness Operator",
                "Invertion Operator",
                "Identity Operator",
                "Gauss Operator",
                "Kanny Operator",
                "Sobel Operator",
                "Laplas Operator",
                "Pruitt Operator",
                "Roberts Operator",
                "Previtt Operator"
            };
		   
            _initializeViewModel?.Invoke(this);
		}

	    #region Methods for Selecting

	    public void SelectAllItems(object sender, EventArgs e)
	    {
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

	    #endregion
    }
}
