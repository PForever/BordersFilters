using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Abstract
{
    interface IOperator
    {
        Color[,] Transform(Color[,] src);
    } 
}
