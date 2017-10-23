using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OperatorsHelper
{
    public static class Formats
    {
        public static string Extension(this string name)
        {
            int i;
            for (i = name.Length - 1; i >= 0 ; --i)
            {
                if(name[i] == '.') break;
            }
            ++i;
            if (i == name.Length) return "";
            return name.Substring(i, name.Length - i);
        }
    }
}
