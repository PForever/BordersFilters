using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel
{
	public class ChoseAlgorithmViewModel : DependencyObject
	{
	    #region Initialize
	    public static event Action<ChoseAlgorithmViewModel> InitializeViewModel
	    {
	        add => _initializeViewModel += value;
	        remove => _initializeViewModel -= value;
	    }

	    private static event Action<ChoseAlgorithmViewModel> _initializeViewModel;
	    #endregion
        #region OperatorsList
        public static readonly DependencyProperty OperatorsListProperty = DependencyProperty.Register(
			nameof(OperatorsList), typeof(ICollection<string>), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(default(ICollection<string>)));

		public ICollection<string> OperatorsList
		{
			get { return (IList<string>)GetValue(OperatorsListProperty); }
			set { SetValue(OperatorsListProperty, value); }
		}
		#endregion
		#region Operation
		public static readonly DependencyProperty OperationProperty = DependencyProperty.Register(
			nameof(Operation), typeof(int), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(default(int)));

		public int Operation
		{
			get { return (int) GetValue(OperationProperty); }
			set
			{
				SetValue(OperationProperty, value);
			}
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
	        nameof(RGBOperator), typeof(Boolean), typeof(ChoseAlgorithmViewModel), new PropertyMetadata(false));

	    public Boolean RGBOperator
        {
	        get { return (Boolean) GetValue(RGBOperatorProperty); }
	        set { SetValue(RGBOperatorProperty, value); }
	    }

	    #endregion

		public ChoseAlgorithmViewModel()
		{
			OperatorsList = new[] {"Преобразование яркости", "Тождественный оператор", "Оператор Кэнни", "Оператор Собеля", "Оператор Лапласа", "Оператор Превитта", "Оператор Робертса" };
            _initializeViewModel?.Invoke(this);
		}
	}
}
