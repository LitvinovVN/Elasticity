using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary
{
    /// <summary>
    /// Методы расширения
    /// </summary>
    public static class Extensions
    {
        public static string ToStringFromList<T>(this List<T> nodes)
        {
            if(nodes==null || nodes.Count==0)
                return "Узлы отсутствуют";

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var node in nodes)
            {
                stringBuilder.Append(node.ToString() + "\n");
            }

            string result = stringBuilder.ToString();

            return result;
        }
    }
}
