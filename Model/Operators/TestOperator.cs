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
        public Color[,] Transform(Color[,] src)
        {
            Color[,] dst = new Color[src.GetLength(0), src.GetLength(1)];
            for (int i = src.GetLength(0) - 1; i >= 0; --i)
            {
                for (int j = src.GetLength(1) - 1; j >= 0; --j)
                {
                    byte bw = (byte)~(byte) (src[i, j].GetBrightness() * 255);
                    dst[i, j] = Color.FromArgb(bw, bw, bw); //Color.FromArgb(src[src.GetUpperBound(0) - i, src.GetUpperBound(1) - j].A, src[i, j].R, src[i, j].G, src[i, j].B);
                }
            }
            return dst;
        }
    }
}
