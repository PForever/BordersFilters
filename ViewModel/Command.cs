using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
	public class Command : ICommand
	{
		public Command(Action<string> action)
		{
			_action = action;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		private readonly Action<string> _action;

		public void Execute(object parameter)
		{
			//if(parameter is string s)_action?.Invoke(s);
			_action?.Invoke(null);
		}

		public event EventHandler CanExecuteChanged;
	}
}
