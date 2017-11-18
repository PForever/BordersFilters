using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Abstract;
using Model.OperatorsHelper;


namespace Model.Operators
{
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
            return dst.Binary();
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
}
