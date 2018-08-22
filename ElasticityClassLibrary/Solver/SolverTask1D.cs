using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.SolverNamespase
{
    /// <summary>
    /// Одномерная задача
    /// </summary>
    public class SolverTask1D : SolverTask
    {
        /// <summary>
        /// Список узлов сетки
        /// </summary>
        public List<Node> NodeList { get; set; }
    }
}
