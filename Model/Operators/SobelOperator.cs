using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators
{
    class SobelOperator : IOperator
    {
        static readonly int[,]
            _operX = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } },
            _operY = _operX.Transponse();

        public byte[,] Transform(byte[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            return dst.ForEach((i, j) => dst[i, j] = src.Process(i, j, _operX, _operY));
        }
        public SobelOperator() => Name = OperatorsEnum.SobelOperator;

        public OperatorsEnum Name { get; }

        public byte[,] Transform(byte[,] src, int reapplyCount)
        {
            for (int i = 0; i < reapplyCount; i++)
            {
                src = Transform(src);
            }
            return src;
        }
    }
}
