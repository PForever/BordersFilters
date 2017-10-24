using System;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace Model.OperatorsHelper {
	/**
	* Класс содержит простые методы обработки изображения
	*/
	public static class BasicFunctions {
		// Получает двумерный массив байтов со значениями яркости соответствующего пикселя
		public static byte[,] GetGrayArray(this Color[,] src) {
			int width = src.GetUpperBound(0), height = src.GetUpperBound(1);
			byte[,] dst = new byte[width + 1, height + 1];
			for (int i = width; i >= 0; --i) {
				for (int j = height; j >= 0; --j)
				{
				    if (src[i, j].GetBrightness() > 255) throw new Exception();
					dst[i, j] = (byte)(src[i, j].GetBrightness() * 255);
				}
			}
			return dst;
		}


		// Получает двумерный массив Color из переданного массива байтов
		public static Color[,] GetColorArray(this byte[,] src) {
			int width = src.GetUpperBound(0), height = src.GetUpperBound(1);
			Color[,] dst = new Color[width + 1, height + 1];
			for (int i = width; i >= 0; --i) {
				for (int j = height; j >= 0; --j) {
					byte tmp = src[i, j];
					dst[i, j] = Color.FromArgb(tmp, tmp, tmp);
				}
			}
			return dst;
		}

	    public static Color[,] GetColorArray(this byte[,] src, Color[,] dst)
	    {
	        int width = src.GetUpperBound(0), height = src.GetUpperBound(1);

            for (int i = width; i >= 0; --i)
	        {
	            for (int j = height; j >= 0; --j)
	            {
                    double tmp = (dst[i, j].GetBrightness() + 1)/(src[i, j] + 1);
	                dst[i, j] = Color.FromArgb((byte)(dst[i,j].R*tmp), (byte)(dst[i, j].G*tmp), (byte)(dst[i, j].B*tmp ));
	            }
	        }
	        return dst;
	    }
    }
}
