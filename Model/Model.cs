using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model.Abstract;
using Model.Operators;
using Color = System.Drawing.Color;

namespace Model

{
    public enum OperatorsEnum
    {
        Operator0 = 0, Operator1, Operator2, Operator3, Operator4
    }
    public class Initialization
    {
        #region Property

        private BitmapSource _destination;

        #region Properties
        public string Path { get; set; }
        public OperatorsEnum Operator { get; set; }
        public string Extantion { get; set; }
        public Bitmap Source { get; set; }

        public BitmapSource Destination
        {
            get { return _destination; }
            set
            {
                _destination = value;
                _destinationChanged?.Invoke(Destination);
            }
        }

        private event Action<BitmapSource> _destinationChanged;
        public event Action<BitmapSource> DestinationChanged
        {
            add { _destinationChanged += value; }
            remove { _destinationChanged -= value; }
        }

        #endregion

        #endregion

        #region Methods

        public void Start()
        {
            Source = new Bitmap(Path);
            var srcMatrix = GetBitMapColorMatrix(Source);
            //byte[] pixels = BitmapSourceToArray(Source);
            IOperator oper = null;
            switch (Operator)
            {
                case OperatorsEnum.Operator0:
                    oper = new TestOperator();
                    break;
                case OperatorsEnum.Operator1:

                    break;
                case OperatorsEnum.Operator2:

                    break;
                case OperatorsEnum.Operator3:

                    break;
                case OperatorsEnum.Operator4:

                    break;
                default:
                    break;
            }
            Bitmap bm = SetBitMapColorMatrix(oper?.Transform(srcMatrix) ?? srcMatrix);
            Destination = GetBitmapSource(bm);
        }
        #region Bitmap

        public Color[,] GetBitMapColorMatrix(Bitmap b1)
        {
            int hight = b1.Height;
            int width = b1.Width;

            Color[,] colorMatrix = new Color[width, hight];
            for (int i = 0; i < width; i++)
            {
                //colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i,j] = b1.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }

        public Bitmap SetBitMapColorMatrix(Color[,] colorMatrix)
        {
            Bitmap bm = new Bitmap(colorMatrix.GetLength(0), colorMatrix.GetLength(1));
            for (int i = 0; i < colorMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < colorMatrix.GetLength(1); j++)
                {
                    bm.SetPixel(i, j, colorMatrix[i,j]);
                }
            }
            return bm;
        }

        private Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }

        public BitmapSource GetBitmapSource(Bitmap bitmap)
        {
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap
            (
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );

            return bitmapSource;
        }

        #endregion
        #endregion
    }
}
