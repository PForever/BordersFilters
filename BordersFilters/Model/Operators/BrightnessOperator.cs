using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Abstract;

namespace Model.Operators {
	/*
	 * Преобразует изображение в массив, содержащий инвертированные значения яркости. 
	 * Тупо демонстрация этого преобразования
	 */
	
	class BrightnessOperator : IOperator {
		public string GetName() {
			return "Преобразование в яркость.";
		}

		public byte[,] Transform(byte[,] src) {
			return (byte[,]) src.Clone();
		}

		public byte[,] Transform(Color[,] src) {
			return Transform(BasicFunctions.GetGrayArray(src));
		}
	}
}
