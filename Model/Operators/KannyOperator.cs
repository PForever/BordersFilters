using System;
using Model.Abstract;
using Model.OperatorsHelper;


namespace Model.Operators
{
    class KannyOperator : IOperator
    {
        private const byte BorderMin = 20*byte.MaxValue / 100;
        private const byte BorderMax = 30*byte.MaxValue / 100;
        private const byte Middle = (BorderMin + BorderMax)/2;

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

        private byte[,] Transform(byte[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            int[,] grads = new int[src.GetLength(0), src.GetLength(1)];
            //Parallel
            dst.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, _oper)); //применяем гаусса
            src.ParallelForEach((i, j) => src[i, j] = dst.Process(i, j, _operX, _operY, out grads[i, j])); //применяем собеля со взятием градиента
            dst.ParallelForEach((i, j) => dst[i, j] = src.NonMaximum(grads, i, j, BorderMin, BorderMax, Middle)); //применяем NonMax и Двойную пороговую фильтрацию
            src.ParallelForEach((i, j) => src[i, j] = dst[i, j] == Middle ? dst.UncertaintyTracing(grads, i, j, BorderMax) : dst[i, j] == BorderMax ? byte.MaxValue : byte.MinValue); //трассировка области неоднозначности
            return src;
        }


        public byte[,] Transform(byte[,] src, int reapplyСount)
        {
            for (int i = 0; i < reapplyСount; i++)
            {
                src = Transform(src);
            }
            return src;
        }
    }
}
