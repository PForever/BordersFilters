using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators
{
    class GaussOperator : IOperator
    {
        private double[,] _oper;
        //static readonly int[,] _oper = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
        public GaussOperator() => Name = OperatorsEnum.GaussOperator;

        public OperatorsEnum Name { get; }

        public byte[,] Transform(byte[,] src, int reapplyСount)
        {
            _oper = new double[reapplyСount, reapplyСount];
            double sigma = (double)reapplyСount / 6;
            int halfLenght = reapplyСount / 2;
            _oper.ParallelForEach((i, j) => _oper[i, j] = GetGauss(sigma, i - halfLenght, j - halfLenght));
            double dec = Math.Abs(_oper.Determinant());
            //double max = _oper.Max();
            _oper.ParallelForEach((i, j) => _oper[i, j] = _oper[i, j]/dec);
            //_oper = new[,]
            //{
            //    {0.000789, 0.006581, 0.013347, 0.006581, 0.000789},
            //    {0.006581, 0.054901, 0.111345, 0.054901, 0.006581},
            //    {0.013347, 0.111354, 0.225821, 0.111345, 0.013347},
            //    {0.006581, 0.054901, 0.111345, 0.054901, 0.006581},
            //    {0.000789, 0.006581, 0.013347, 0.006581, 0.000789}
            //};
            byte[,] dst = new byte[src.GetLength(0),src.GetLength(1)];
            src.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, _oper));
            return dst;
        }

        private double GetGauss(double sigma, int i, int j) => (1 / (2 * Math.PI * Math.Pow(sigma, 2))) *
                                                               Math.Exp(-(Math.Pow(i, 2) + Math.Pow(j, 2)) /
                                                                        (2 * Math.Pow(sigma, 2)));
    }
}
