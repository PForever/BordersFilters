using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Abstract;
using Model.OperatorsHelper;


namespace Model.Operators
{
<<<<<<< HEAD
	class KannyOperator : IOperator {
		public KannyOperator() => Name = OperatorsEnum.KannyOperator;

		public OperatorsEnum Name { get; }

		private const byte BorderMin = 20 * byte.MaxValue / 100;
		private const byte BorderMax = 30 * byte.MaxValue / 100;
		private const byte Middle = (BorderMin + BorderMax) / 2;

		private Predicate<byte> BCheck => point => BorderMin < point && point < BorderMax;
		private readonly double[,] _oper = new double[,]
			{{2, 4, 5, 4, 2},
			{4, 9, 12, 9, 4},
			{5, 12, 15, 12, 5},
			{4, 9, 12, 9, 4},
			{2, 4, 5, 4, 2}}.Divide(159);

		static readonly int[,]
			_operX = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } },
			_operY = _operX.Transponse();

		public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma) {
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
			int[,] grads = new int[src.GetLength(0), src.GetLength(1)];

			double[,] gauss = GaussOperator.GetGauss(MatrixSize, Sigma);
			//double[,] gauss = _oper;

			dst.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, gauss)); //применяем гаусса
			src.ParallelForEach((i, j) => src[i, j] = dst.Process(i, j, _operX, _operY, out grads[i, j])); //применяем собеля со взятием градиента
			dst.ParallelForEach((i, j) => dst[i, j] = src.NonMaximum(grads, i, j, BorderMin, BorderMax, Middle)); //применяем NonMax и Двойную пороговую фильтрацию
			src.ParallelForEach((i, j) => src[i, j] = dst[i, j] == Middle ? dst.UncertaintyTracing(grads, i, j, BorderMax) : dst[i, j] == BorderMax ? byte.MaxValue : byte.MinValue); //трассировка области неоднозначности
			return src;
		}
	}
=======
    class KannyOperator : IOperator
    {
        private const int BorderMin = 5*byte.MaxValue / 100;
        private const int BorderMax = 25*byte.MaxValue / 100;
        private const int Middle = (BorderMin + BorderMax)/2;

        private Predicate<byte> BCheck => point => BorderMin < point && point < BorderMax;
        private readonly double[,] _oper = new double[,]
            {{2, 4, 5, 4, 2},
            {4, 9, 12, 9, 4},
            {5, 12, 15, 12, 5},
            {4, 9, 12, 9, 4},
            {2, 4, 5, 4, 2}}.Divide(159);

        static readonly int[,]
            OperX = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } },
            OperY = OperX.Transponse();

        public byte[,] Transform(byte[,] src1, int MatrixSize, double Sigma)
        {
            var src = src1.ToInt();
            int[,] dst = new int[src.GetLength(0), src.GetLength(1)];

            int[,] grads = new int[src.GetLength(0), src.GetLength(1)];
            ConcurrentBag<Point> midPoints = new ConcurrentBag<Point>();

            //применяем гаусса
            dst.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, _oper));
            //применяем собеля со взятием градиента
            src.ParallelForEach((i, j) => src[i,j] = dst.Process(i, j, OperX, OperY, out grads[i, j]));
            //применяем NonMax и Двойную пороговую фильтрацию
            dst.ParallelForEach((i, j) => dst[i, j] = src[i,j] == byte.MinValue? byte.MinValue : src.NonMaximum(grads, i, j, BorderMin, BorderMax, Middle, midPoints));
            //трассировка области неоднозначности
            midPoints.ForEach(point => { if(dst[point.X,point.Y] == Middle) dst.UncertaintyTracing(grads, point.X, point.Y); });
            return dst.ToByte();
        }


        public KannyOperator() => Name = OperatorsEnum.KannyOperator;

        public OperatorsEnum Name { get; }

        public byte[,] Transform(byte[,] src, int reapplyСount)
        {
            for (int i = 0; i < reapplyСount; i++)
            {
                //src = Transform(src);
            }
            return src;
        }
    }
>>>>>>> 9cf16ae5b64198bc132399b52cc5d9bd9bf4c869
}
