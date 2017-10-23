using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Abstract;

namespace Model {
	/**
	* Класс содержит простые методы обработки изображения
	*/
	class BasicFunctions {
		// Получает двумерный массив байтов со значениями яркости соответствующего пикселя
		public static byte[,] GetGrayArray(Color[,] src) {
			int width = src.GetUpperBound(0), height = src.GetUpperBound(1);
			byte[,] dst = new byte[width + 1, height + 1];
			for (int i = width; i >= 0; --i) {
				for (int j = height; j >= 0; --j) {
					dst[i, j] = (byte)~(byte)(src[i, j].GetBrightness() * 255);
				}
			}
			return dst;
		}


		// Получает двумерный массив Color из переданного массива байтов
		public static Color[,] GetColorArray(byte[,] src) {
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
	}
}
