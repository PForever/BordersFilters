using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Model.OperatorsHelper;

namespace ViewModel
{
	public class InputPictureViewModel : DependencyObject
	{
	    #region Initialize
	    public static event Action<InputPictureViewModel> InitializeViewModel
	    {
	        add => _initializeViewModel += value;
	        remove => _initializeViewModel -= value;
	    }

	    private static event Action<InputPictureViewModel> _initializeViewModel;
	    #endregion

	    public string Extention { get; set; }

        #region Image

        public static readonly DependencyProperty InputImageSourseProperty = DependencyProperty.Register(
	        nameof(InputImageSourse), typeof(BitmapSource), typeof(InputPictureViewModel), new PropertyMetadata(default(BitmapSource)));
	    private BitmapSource InputImageSourse
	    {
	        get { return (BitmapSource)GetValue(InputImageSourseProperty); }
	        set { SetValue(InputImageSourseProperty, value); }
	    }
	    public BitmapSource GetImage() => InputImageSourse;
	    public void SetImage(string path) => ChooseImage?.Invoke(path);
	    private event Action<string> ChooseImage;

	    #endregion

        public InputPictureViewModel()
		{
			ChooseImage += path =>
			{
				if(File.Exists(path))
				{
					var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

						Extention = path.Extension();
						switch (Extention)
						{
							case "jpg":
							case "jpeg":
								InputImageSourse = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
									BitmapCacheOption.Default).Frames[0];
								break;
							case "png":
								InputImageSourse = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
									BitmapCacheOption.Default).Frames[0];
								break;
							case "bmp":
								InputImageSourse = new BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
									BitmapCacheOption.Default).Frames[0];
								break;
							case "tiff":
								InputImageSourse = new TiffBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
									BitmapCacheOption.Default).Frames[0];
								break;
							case "gif":
								InputImageSourse = new GifBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
									BitmapCacheOption.Default).Frames[0];
								break;
							default:
								break;
					}
				}
			};
			_initializeViewModel?.Invoke(this);
		}


	}
}
