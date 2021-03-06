﻿namespace Model.OperatorsHelper
{
	public static class Formats
	{
        /// <summary>
        /// Получение расширения переданного файла
        /// </summary>
        /// <param name="name">Имя файла</param>
        public static string Extension(this string name)
		{
			int i;
			for (i = name.Length - 1; i >= 0 ; --i)
			{
				if(name[i] == '.') break;
			}
			++i;
			if (i == name.Length) return "";
			return name.Substring(i, name.Length - i).ToLower();
		}
    }
}
