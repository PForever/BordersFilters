using System;
using System.Windows.Input;

namespace ViewModel
{
	public class Command : ICommand
	{
	    #region Fields

	    private readonly Action execute;

	    private readonly Func<bool> canExecute;

	    public event EventHandler CanExecuteChanged;

	    #endregion

	    #region Constractors

	    public Command(Action execute) : this(execute, null) { }

	    public Command(Action execute, Func<bool> canExecute)
	    {
	        if (execute == null)
	        {
	            throw new ArgumentNullException(nameof(execute));
	        }
	        this.execute = execute;
	        this.canExecute = canExecute;
	    }

	    #endregion

	    #region Methods

	    public bool CanExecute(object parameter)
	    {
	        if (canExecute == null)
	        {
	            return true;
	        }
	        return canExecute();
	    }

	    public void Execute(object parameter)
	    {
	        execute();
	    }

        /// <summary>
        /// ћетод добавл€ет свойтвам возможность вли€ть на измен€емость доступности команды (кнопки)
        /// </summary>
	    public void RaiseCanExecuteChanged() 
	    {
	        CanExecuteChanged?.Invoke(this,EventArgs.Empty);
	    }

	    #endregion
    }
}
