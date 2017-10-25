using System;

namespace Model.OperatorsHelper {
	 public static class Convolution {
		public static byte Process(this byte[,] pix, int i, int j, int[,] oper) {
			byte result = 0;
		    int size = oper.GetLength(0);

            oper.ForEach((k, l) => result += (byte)(oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2)));

            return result;
		}
	    public static byte Process(this byte[,] pix, int i, int j, double[,] oper)
	    {
	        byte result = 0;
	        int size = oper.GetLength(0);

            oper.ForEach((k, l) => result += (byte)(oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2)));

	        return result;
	    }
    }

	static class ArrayHelper {
		public static byte GetPoint(this byte[,] arr, int i, int j)
        {
			return arr[arr.GetUpperBound(0) - i < 0 ? arr.GetUpperBound(0) : (i > 0 ? i : 0),
				arr.GetUpperBound(1) - j < 0 ? arr.GetUpperBound(1) : (j > 0? j : 0)];
		}
    }
}
