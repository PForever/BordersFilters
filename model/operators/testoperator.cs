using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Abstract;

namespace Model.Operators
{
    class TestOperator : IOperator
    {
        // Задаёт собственное значение яркости каждому пикселю
        public byte[,] Transform(byte[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            for (int i = src.GetLength(0) - 1; i >= 0; --i)
            {
                for (int j = src.GetLength(1) - 1; j >= 0; --j)
                {
                    byte bw = (byte)~(byte) (src[i, j] * 255);
                    dst[i, j] = bw; //Color.FromArgb(bw, bw, bw);
                }
            }
            return dst;
        }
    }
}
