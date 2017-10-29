using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
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
using Model.OperatorsHelper;
using Color = System.Drawing.Color;

namespace Model

{
    public enum OperatorsEnum
    {
        BrightnessOperator = 0,
        InvertionOperator,
        IdentityOperator,
        GaussOperator,
        KannyOperator,
        SobelOperator,
        LaplasOperator,
        PruittOperator,
        RobertsOperator,
        PrevittOperator
    }

    public class Initialization
    {

        private Collection<BitmapSource> _destination;

        #region Properties

        public string Path { get; set; }

        public Collection<OperatorsEnum> Operators { get; set; }

        //public OperatorsEnum Operator { get; set; }
        public string Extantion { get; set; }
        public Collection<Func<BitmapSource>> PreDestination { get; set; }
        public Bitmap Source { get; set; }
        public bool RGBOperator { get; set; }
        public int ReapplyCount { get; set; }
        private static readonly object SyncRoot1 = new object();
        private static readonly object SyncRoot2 = new object();

        public Collection<BitmapSource> Destination
        {
            get { return _destination; }
            set
            {
                _destination = value;
                _destinationChanged?.Invoke(Destination);
            }
        }

        private event Action<Collection<BitmapSource>> _destinationChanged;

        public event Action<Collection<BitmapSource>> DestinationChanged
        {
            add { _destinationChanged += value; }
            remove { _destinationChanged -= value; }
        }

        #endregion


        public void Start()
        {
            //var numberOfActiveCores = Environment.ProcessorCount - 1;
            // var i = 1;

            //while (Operators.Count - i + 1 >= numberOfActiveCores)
            //{
            //    Parallel.For(i, i + numberOfActiveCores, GetBitmapForOneOperators);
            //    i += numberOfActiveCores;
            //}
            //if (Operators.Count - i + 1 != 0)
            //{
            //    Parallel.For(i, Operators.Count, GetBitmapForOneOperators);
            //}
            PreDestination = new Collection<Func<BitmapSource>>();
            Destination = new Collection<BitmapSource>();
            //foreach (var Operator in Operators)
            //{
                Parallel.ForEach(Operators, Operator =>
                {
                    lock (SyncRoot1)
                    {
                        Source = new Bitmap(Path);
                    }
                    var srcMatrix = GetBitMapColorMatrix(Source);
                    IOperator oper = null;
                    switch (Operator)
                    {
                        case OperatorsEnum.BrightnessOperator:
                            oper = new BrightnessOperator();
                            break;
                        case OperatorsEnum.InvertionOperator:
                            oper = new InvertionOperator();
                            break;
                        case OperatorsEnum.IdentityOperator:
                            oper = new IdentityOperator();
                            break;
                        case OperatorsEnum.KannyOperator:
                            oper = new KannyOperator();
                            break;
                        case OperatorsEnum.SobelOperator:
                            oper = new SobelOperator();
                            break;
                        case OperatorsEnum.LaplasOperator:
                            break;
                        case OperatorsEnum.PruittOperator:
                            oper = new PrevittOperator();
                            break;
                        case OperatorsEnum.RobertsOperator:
                            oper = new RobertsOperator();
                            break;
                        case OperatorsEnum.GaussOperator:
                            oper = new GaussOperator();
                            break;
                        default:
                            break;
                    }
                    if (oper != null)
                    {
                        var result = !RGBOperator
                            ? oper.Transform(srcMatrix.GetGrayArray(), ReapplyCount).GetColorArray()
                            : srcMatrix.GetColorArray(oper.Transform(srcMatrix.GetRedArray(), ReapplyCount),
                                oper.Transform(srcMatrix.GetGreenArray(), ReapplyCount),
                                oper.Transform(srcMatrix.GetBlueArray(), ReapplyCount));

                        lock (SyncRoot1)
                        {
                            PreDestination.Add(() =>
                            {
                                Bitmap bm = SetBitMapColorMatrix(result ?? srcMatrix);
                                return GetBitmapSource(bm);
                            });
                        }
                        //bm.Dispose();
                    }
                });
           // }
        }

        //private void GetBitmapForOneOperators(OperatorsEnum Operator)
        //{
        //    Source = new Bitmap(Path);
        //    var srcMatrix = GetBitMapColorMatrix(Source);
        //    IOperator oper = null;
        //    switch (Operator)
        //    {
        //        case OperatorsEnum.BrightnessOperator:
        //            oper = new BrightnessOperator();
        //            break;
        //        case OperatorsEnum.InvertionOperator:
        //            oper = new InvertionOperator();
        //            break;
        //        case OperatorsEnum.IdentityOperator:
        //            oper = new IdentityOperator();
        //            break;
        //        case OperatorsEnum.KannyOperator:
        //            oper = new KannyOperator();
        //            break;
        //        case OperatorsEnum.SobelOperator:
        //            oper = new SobelOperator();
        //            break;
        //        case OperatorsEnum.LaplasOperator:
        //            break;
        //        case OperatorsEnum.PruittOperator:
        //            oper = new PrevittOperator();
        //            break;
        //        case OperatorsEnum.RobertsOperator:
        //            oper = new RobertsOperator();
        //            break;
        //        case OperatorsEnum.GaussOperator:
        //            oper = new GaussOperator();
        //            break;
        //        default:
        //            break;
        //    }
        //    if (oper != null)
        //    {
        //        var result = !RGBOperator
        //            ? oper.Transform(srcMatrix.GetGrayArray(), ReapplyCount).GetColorArray()
        //            : srcMatrix.GetColorArray(oper.Transform(srcMatrix.GetRedArray(), ReapplyCount),
        //                oper.Transform(srcMatrix.GetGreenArray(), ReapplyCount),
        //                oper.Transform(srcMatrix.GetBlueArray(), ReapplyCount));

        //        Bitmap bm = SetBitMapColorMatrix(result ?? srcMatrix);

        //        //lock (SyncRoot1)
        //        //{
        //            Destination.Add(GetBitmapSource(bm));
        //        //}
        //    }
        //}

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

				    colorMatrix[i, j] = b1.GetPixel(i, j);
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

	}
}
