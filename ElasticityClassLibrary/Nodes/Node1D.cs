using System;
using System.Collections.Generic;
using System.Text;
using ElasticityClassLibrary.GeometryNamespase;

namespace ElasticityClassLibrary.Nodes
{
    /// <summary>
    /// Узел одномерной расчетной сетки
    /// </summary>
    public class Node1D : Node
    {
        /// <summary>
        /// Координата в одномерном пространстве
        /// </summary>
        public Coordinate1D Coordinate1D { get; set; }

        /// <summary>
        /// Координата
        /// (переопределение)
        /// </summary>
        public override Coordinate Coordinate
        {
            get => Coordinate1D;
            set => Coordinate1D = (Coordinate1D)value;
        }

        /// <summary>
        /// Навигационное свойство в одномерном пространстве
        /// </summary>
        public NodeNavigation1D NodeNavigation1D { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// (переопределение)
        /// </summary>
        public override NodeNavigation NodeNavigation
        {
            get => NodeNavigation1D;
            set => NodeNavigation1D = (NodeNavigation1D)value;
        }
    }
}
