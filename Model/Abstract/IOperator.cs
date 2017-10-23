using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace Model.Abstract {
	interface IOperator {
		String GetName();

		byte[,] Transform(byte[,] src);

		byte[,] Transform(Color[,] src);
	}
=======
namespace Model.Abstract
{
    interface IOperator
    {
        byte[,] Transform(byte[,] src);
    } 
>>>>>>> pforever/master
}
