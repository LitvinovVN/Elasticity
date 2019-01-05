using ElasticityClassLibrary.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.SolverNamespace
{
    /// <summary>
    /// Одномерная задача
    /// </summary>
    public class SolverTask1D : SolverTask
    {
        /// <summary>
        /// Набор узлов сетки
        /// </summary>
        public NodeSet1D NodeSet1D { get; set; }
    }
}
