using Model.Abstract;
namespace Model.Operators {


	class BrightnessOperator : IOperator
	{

	    public BrightnessOperator() => Name = OperatorsEnum.BrightnessOperator;
		private byte[,] Transform(byte[,] src) {
		    //for (int i = 0; i < src.GetLength(0); i++)
		    //{
		    //    for (int j = 0; j < src.GetLength(1); j++)
		    //    {
		    //        src[i, j] = (byte) src[i, j];
		    //    }
		    //}
			return src;
		}

	    public OperatorsEnum Name { get; }

	    public byte[,] Transform(byte[,] src, int reapplyСount) {
			//for (int i = 0; i < reapplyСount; i++) {
			//	src = Transform(src);
			//}
			return src;
		}
	}
}
