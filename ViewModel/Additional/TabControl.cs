using System.Windows.Media.Imaging;

namespace ViewModel.Additional
{
    public class TabControl 
    {
        public string NameOfAlgorithm { get; set; }
        public BitmapSource OutImageSource { get; set; }

        public TabControl(BitmapSource outImageSource, string nameOfAlgorithm)
        {
            OutImageSource = outImageSource;
            NameOfAlgorithm = nameOfAlgorithm;
        }
    }
}

