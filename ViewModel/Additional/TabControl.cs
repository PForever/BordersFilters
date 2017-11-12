using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Model;
using Model.OperatorsHelper;

namespace ViewModel.Additional
{
    public class TabControl
    {
        private static Dictionary<string,OperatorsEnum> OperationDictionary;
        public string NameOfAlgorithm { get; set; }
        public BitmapSource OutImageSource { get; set; }
        public static BitmapSource InputImageSource { get; set; }
        public static string BaseOfSavingDirectory { get; set; }
        public static string OutPath { get; set; }


        public TabControl(Dictionary<OperatorsEnum,BitmapSource> dictionary)
        {
            BitmapSource bm;
            foreach (var expression in OperationDictionary)
            {
                if (dictionary.TryGetValue(expression.Value,out bm))
                {
                    NameOfAlgorithm = expression.Key;
                    OutImageSource = bm;
                }
            }
        }

        public void SaveImage()
        {
            if (BaseOfSavingDirectory == null)
            {
                OutPath = Directory.GetCurrentDirectory() + @"\\" + NameOfAlgorithm + ".jpg";
            }
            else
            {
                OutPath = BaseOfSavingDirectory + @"\\" + NameOfAlgorithm + ".jpg";
            }
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(OutImageSource));
            using (var filestream = new FileStream(OutPath, FileMode.Create))
            {
                encoder.Save(filestream);
            }
        }

        public static void SetInputImage(string inputPath)
        {
            var stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            var extention = inputPath.Extension();
            switch (extention)
            {
                case "jpg":
                case "jpeg":
                    InputImageSource = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default).Frames[0];
                    break;
                case "png":
                    InputImageSource = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default).Frames[0];
                    break;
                case "bmp":
                    InputImageSource = new BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default).Frames[0];
                    break;
                case "tiff":
                    InputImageSource = new TiffBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default).Frames[0];
                    break;
                case "gif":
                    InputImageSource = new GifBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default).Frames[0];
                    break;
                default:
                    break;
            }
        }

        public static Dictionary<string, OperatorsEnum> SetOperatorsDictionary(ICollection<string> operatorsList)
        {
            var i = 0;
            OperationDictionary = new Dictionary<string, OperatorsEnum>();
            foreach (var oper in operatorsList)
            {
                OperationDictionary.Add(oper,(OperatorsEnum)i);
                i++;
            }
            return OperationDictionary;
        }

    }
}

