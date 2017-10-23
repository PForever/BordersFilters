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
        public static void OnStaticSetValue(string path) => StaticSetValue?.Invoke(path);
        private static event Action<string> StaticSetValue;
        public static string Extention { get; set; }
        public static BitmapSource OnStaticGetValue() => StaticGetValue?.Invoke();
        private static event Func<BitmapSource> StaticGetValue;

        public static readonly DependencyProperty ImageSourseProperty = DependencyProperty.Register(
            nameof(ImageSourse), typeof(BitmapSource), typeof(InputPictureViewModel), new PropertyMetadata(default(BitmapSource)));

        public InputPictureViewModel()
        {
            StaticSetValue += path =>
            {
                if(File.Exists(path))
                {
                    var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                        Extention = path.Extension();
                        switch (Extention)
                        {
                            case "jpg":
                            case "jpeg":
                                ImageSourse = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                                    BitmapCacheOption.Default).Frames[0];
                                break;
                            case "png":
                                ImageSourse = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                                    BitmapCacheOption.Default).Frames[0];
                                break;
                            case "bmp":
                                ImageSourse = new BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                                    BitmapCacheOption.Default).Frames[0];
                                break;
                            case "tiff":
                                ImageSourse = new TiffBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                                    BitmapCacheOption.Default).Frames[0];
                                break;
                            case "gif":
                                ImageSourse = new GifBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                                    BitmapCacheOption.Default).Frames[0];
                                break;
                            default:
                                break;
                    }
                }
            };
            StaticGetValue += () => ImageSourse;
        }

        public BitmapSource ImageSourse
        {
            get { return (BitmapSource) GetValue(ImageSourseProperty); }
            set
            {
                SetValue(ImageSourseProperty, value); 
            }
        }
    }
}
