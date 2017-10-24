using System;

namespace Model.OperatorsHelper {
	class Convolution {
		byte Process(byte[,] pix, int i, int j, int[,] oper, int size) {
			byte result = 0;
			for (int k = 0; k < size; k++) {
				for (int l = 0; l < size; l++) {
					result += (byte)(oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2));
				}
			}
			return result;
		}

	}

	static class ArrayHelper {
		public static byte GetPoint(this byte[,] arr, int i, int j) {
			return arr[arr.GetUpperBound(0) - i >= 0 ? Math.Abs(j) : i - arr.GetUpperBound(0),
				arr.GetUpperBound(1) - j >= 0 ? Math.Abs(j) : j - arr.GetUpperBound(0)];
		}
	}
}
