using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;
using ViewModel.Configs;

namespace ViewModel {
	public class InputPathViewModel : DependencyObject
	{

        #region Initialize

        public static event Action<InputPathViewModel> InitializeViewModel
        {
            add => _initializeViewModel += value;
            remove => _initializeViewModel -= value;
        }

	    private static event Action<InputPathViewModel> _initializeViewModel;

	    #endregion

		#region Browse
		public static readonly DependencyProperty BrowseProperty = DependencyProperty.Register(
			nameof(Browse), typeof(Command), typeof(InputPathViewModel), new PropertyMetadata(default(Command)));

		public Command Browse {
			get { return (Command)GetValue(BrowseProperty); }
			set { SetValue(BrowseProperty, value); }
		}
		#endregion

		#region Path
	    private static string _pathValue;
        public static readonly DependencyProperty PathValueProperty = DependencyProperty.Register(
			nameof(PathValue), typeof(string), typeof(InputPathViewModel), new PropertyMetadata(_pathValue, (o, args) => _pathValue = (string) args.NewValue));
		public string PathValue
		{
		    get => (string)GetValue(PathValueProperty);
		    set => SetValue(PathValueProperty, value);
		}

	    #endregion

	    #region Configuration
	    public void InitConfig()
	    {
	        PathValue = Configurator.Path.PathItems["Input"].Path;
	    }
	    ~InputPathViewModel()
	    {
	        Configurator.Path.PathItems["Input"].Path = _pathValue;
	    }
	    #endregion

        public InputPathViewModel()
        {
            InitConfig();
            _initializeViewModel?.Invoke(this);
        }
	}
}
