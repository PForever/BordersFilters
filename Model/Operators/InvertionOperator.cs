using Model.Abstract;

namespace Model.Operators {


	class InvertionOperator : IOperator {

		private byte[,] Transform(byte[,] src) {
		    for (int i = 0; i < src.GetLength(0); i++)
		    {
		        for (int j = 0; j < src.GetLength(1); j++)
		        {
		            src[i, j] = (byte) ~src[i, j];
		        }
		    }
		    return src;
		}

		public byte[,] Transform(byte[,] src, int reapplyСount) {
			for (int i = 0; i < reapplyСount; i++) {
				src = Transform(src);
			}
			return src;
		}
	}
}
