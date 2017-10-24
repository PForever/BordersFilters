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
		public static int OnStaticGetValue() => StaticGetValue?.Invoke() ?? 0;
		private static event Func<int> StaticGetValue; 
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

		public ChoseAlgorithmViewModel()
		{
			OperatorsList = new[] {"Преобразование яркости", "Тождественный оператор", "Оператор Кэнни", "Оператор Собеля", "Оператор Лапласа", "Оператор Превитта", "Оператор Робертса" };
			Operation = 0;
			StaticGetValue += () => Operation;
		}
	}
}
