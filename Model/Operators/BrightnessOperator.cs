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
			//return (byte[,]) src.Clone();
		    for (int i = 0; i < src.GetLength(0); i++)
		    {
		        for (int j = 0; j < src.GetLength(1); j++)
		        {
		            src[i, j] = (byte)~src[i, j];
		        }
		    }
		    return src;
		}
	}
}
