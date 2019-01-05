using ElasticityClassLibrary.Nodes;
using System.Collections.Generic;

namespace ElasticityClassLibrary.SolverNamespace
{
    /// <summary>
    /// Результаты расчетов
    /// </summary>
    public class SolverResult
    {
        /// <summary>
        /// Список узлов сетки
        /// </summary>
        public List<Node> NodeList { get; set; }
    }
}