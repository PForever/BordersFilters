using Model.Abstract;
using Model.OperatorsHelper;


namespace Model.Operators
{
    class KannyOperator : IOperator
    {
        private byte[,] Transform(byte[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            return dst.ForEach((i, j) => dst[i, j] = src.Process(i, j, _oper));
        }

        private readonly double[,] _oper = new double[,]
            {{2, 4, 5, 4, 2}, 
            {4, 9, 12, 9, 4}, 
            {5, 12, 15, 12, 5},
            {4, 9, 12, 9, 4}, 
            {2, 4, 5, 4, 2}}.Divide(159);

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
