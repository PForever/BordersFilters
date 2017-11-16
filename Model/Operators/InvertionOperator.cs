using Model.Abstract;

namespace Model.Operators {


	class InvertionOperator : IOperator {
		
	    public InvertionOperator() => Name = OperatorsEnum.InvertionOperator;

        public OperatorsEnum Name { get; }

	    public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma) {
			for (int i = 0; i < src.GetLength(0); i++) {
				for (int j = 0; j < src.GetLength(1); j++) {
					src[i, j] = (byte)~src[i, j];
				}
			}

			return src;
		}
	}
}
