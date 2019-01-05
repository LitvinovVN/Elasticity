using System;
using System.Collections.Generic;
using System.Text;
using ElasticityClassLibrary.GeometryNamespase;

namespace ElasticityClassLibrary.Nodes
{
    /// <summary>
    /// Узел трёхмерной расчетной сетки
    /// </summary>
    public class Node3D : Node
    {
        /// <summary>
        /// Координата в трёхмерном пространстве
        /// </summary>
        public Coordinate3D Coordinate3D { get; set; }

        /// <summary>
        /// Координата
        /// (переопределение)
        /// </summary>
        public override Coordinate Coordinate
        {
            get => Coordinate3D;
            set => Coordinate3D = (Coordinate3D)value;
        }

        /// <summary>
        /// Навигационное свойство в одномерном пространстве
        /// </summary>
        public NodeNavigation3D NodeNavigation3D { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// (переопределение)
        /// </summary>
        public override NodeNavigation NodeNavigation
        {
            get => NodeNavigation3D;
            set => NodeNavigation3D = (NodeNavigation3D)value;
        }
    }
}
