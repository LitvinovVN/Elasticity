using System;
using System.Collections.Generic;
using System.Text;
using ElasticityClassLibrary.GeometryNamespase;

namespace ElasticityClassLibrary.Nodes
{
    /// <summary>
    /// Узел двумерной расчетной сетки
    /// </summary>
    public class Node2D : Node
    {
        /// <summary>
        /// Координата в одномерном пространстве
        /// </summary>
        public Coordinate2D Coordinate2D { get; set; }

        /// <summary>
        /// Координата
        /// (переопределение)
        /// </summary>
        public override Coordinate Coordinate
        {
            get => Coordinate2D;
            set => Coordinate2D = (Coordinate2D)value;
        }

        /// <summary>
        /// Навигационное свойство в одномерном пространстве
        /// </summary>
        public NodeNavigation2D NodeNavigation2D { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// (переопределение)
        /// </summary>
        public override NodeNavigation NodeNavigation
        {
            get => NodeNavigation2D;
            set => NodeNavigation2D = (NodeNavigation2D)value;
        }
    }
}
