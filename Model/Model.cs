using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Model.Abstract;
using Model.Operators;
using Model.OperatorsHelper;
using Color = System.Drawing.Color;

namespace Model {
	public enum OperatorsEnum {
		InvertionOperator = 0,
		IdentityOperator,
		GaussOperator,
		KannyOperator,
		SobelOperator,
		LaplasOperator,
		LaplasGaussOperator,
		RobertsOperator,
		PrewittOperator,
    }

	public class Initialization {


		#region Properties

		public string Path { get; set; }
		public Dictionary<OperatorsEnum, BitmapSource> Destination;
		public Collection<OperatorsEnum> Operators { get; set; }
		public Collection<Func<Dictionary<OperatorsEnum, BitmapSource>>> PreDestination { get; set; }
		public Bitmap Source { get; set; }
		public bool RGBOperator { get; set; }
		
	    public int MatrixSize { get; set; } // ����������� �������
        public double Sigma { get; set; } // �������� - �����

        private static readonly object SyncRoot1 = new object();

		#endregion

		public void Start() {
			PreDestination = new Collection<Func<Dictionary<OperatorsEnum, BitmapSource>>>();
			Destination = new Dictionary<OperatorsEnum, BitmapSource>();
			Parallel.ForEach(Operators, (Action<OperatorsEnum>)(Operator => {
				lock (SyncRoot1) {
					Source = new Bitmap(Path);
				}
				var srcMatrix = GetBitMapColorMatrix(Source);
				IOperator oper = null;
				switch (Operator) {
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
						oper = new LaplasOperator();
						break;
				    case OperatorsEnum.LaplasGaussOperator:
				        oper = new LaplasGaussOperator();
				        break;
                    case OperatorsEnum.RobertsOperator:
						oper = new RobertsOperator();
						break;
					case OperatorsEnum.GaussOperator:
						oper = new GaussOperator();
						break;
					case OperatorsEnum.PrewittOperator:
						oper = new PrewittOperator();
						break;
                    default:
						break;
				}
				if (oper == null) return;
				
				var result = !RGBOperator
					? oper.Transform(srcMatrix.GetGrayArray(), MatrixSize, Sigma).GetColorArray()
					: srcMatrix.GetColorArray(oper.Transform(srcMatrix.GetRedArray(), MatrixSize, Sigma),
						oper.Transform(srcMatrix.GetGreenArray(), MatrixSize, Sigma),
						oper.Transform(srcMatrix.GetBlueArray(), MatrixSize, Sigma));

				lock (SyncRoot1) {
					PreDestination.Add(() => {
						Bitmap bm = SetBitMapColorMatrix(result ?? srcMatrix);
						var variable = new Dictionary<OperatorsEnum, BitmapSource>();
						variable.Add(Operator, GetBitmapSource(bm));
						return variable;
					});
				}
				
			}));
		}

		#region Bitmap

		public Color[,] GetBitMapColorMatrix(Bitmap b1) {
			int hight = b1.Height;
			int width = b1.Width;

			Color[,] colorMatrix = new Color[width, hight];
			for (int i = 0; i < width; i++) {
				//colorMatrix[i] = new Color[hight];
				for (int j = 0; j < hight; j++) {

					colorMatrix[i, j] = b1.GetPixel(i, j);
				}
			}
			return colorMatrix;
		}

		public Bitmap SetBitMapColorMatrix(Color[,] colorMatrix) {
			Bitmap bm = new Bitmap(colorMatrix.GetLength(0), colorMatrix.GetLength(1));
			for (int i = 0; i < colorMatrix.GetLength(0); i++) {
				for (int j = 0; j < colorMatrix.GetLength(1); j++) {
					bm.SetPixel(i, j, colorMatrix[i, j]);
				}
			}
			return bm;
		}

		private Bitmap BitmapFromSource(BitmapSource bitmapsource) {
			Bitmap bitmap;
			using (MemoryStream outStream = new MemoryStream()) {
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create(bitmapsource));
				enc.Save(outStream);
				bitmap = new Bitmap(outStream);
			}
			return bitmap;
		}

		public BitmapSource GetBitmapSource(Bitmap bitmap) {
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
