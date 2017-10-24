using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	/*
	 * Преобразует изображение в массив, содержащий инвертированные значения яркости.
	 * Тупо демонстрация этого преобразования
	 */

	class BrightnessOperator : IOperator {

		public byte[,] Transform(byte[,] src) {
			return (byte[,]) src.Clone();
		}

		public byte[,] Transform(byte[,] src, int reapply_count) {
			for (int i = 0; i < reapply_count; i++) {
				src = Transform(src);
			}
			return src;
		}
	}
}
