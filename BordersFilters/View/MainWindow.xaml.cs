using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace BordersFilters.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow //: Window
    {
        private readonly ViewModel.ViewModel _viewModel;

        public MainWindow()
        {
            DataContext = new ViewModel.ViewModel();

            _viewModel = (ViewModel.ViewModel)DataContext;

            _viewModel.SomethingHappen += ShowMessage;
            _viewModel.CalculationStarted += RunDialog;
            _viewModel.CalculationStoped += CloseDialog;

            _viewModel.DialogContent = new SimpleProgressBar();

            InitializeComponent();
        }

        private void ShowMessage(string obj)
        {
            var message = Snackbar.MessageQueue;
            Task.Factory.StartNew(() => message.Enqueue(obj));
        }

        private void RunDialog()
        {
            _viewModel.IsDialogOpen = true;
        }

        private void CloseDialog()
        {
            _viewModel.IsDialogOpen = false;
            ShowMessage("Обработано!");
        }
    }
}
