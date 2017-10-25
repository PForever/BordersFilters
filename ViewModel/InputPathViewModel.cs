using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;

namespace ViewModel {
	public class InputPathViewModel : DependencyObject {

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
		public static readonly DependencyProperty PathValueProperty = DependencyProperty.Register(
			nameof(PathValue), typeof(string), typeof(InputPathViewModel), new PropertyMetadata(Directory.GetCurrentDirectory() +  @"\..\..\Resours\wallhaven-452501.jpg"));

		public string PathValue {
			get {
				return (string)GetValue(PathValueProperty);
			}
			set {
				SetValue(PathValueProperty, value);
			}
		}
		#endregion

		public InputPathViewModel()
        {
            _initializeViewModel?.Invoke(this);
        }
	}
}
