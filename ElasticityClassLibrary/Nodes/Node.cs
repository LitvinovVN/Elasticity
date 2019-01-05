﻿using ElasticityClassLibrary.Derivatives;
using ElasticityClassLibrary.GeometryNamespase;
using ElasticityClassLibrary.Infrastructure;

namespace ElasticityClassLibrary.Nodes
{
    /// <summary>
    /// Узел сетки
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Расположение узла (за контуром / граничный / внутренний)
        /// </summary>
        public NodeLocationEnum NodeLocationEnum { get; set; }

        /// <summary>
        /// Индекс узла
        /// </summary>
        public ulong Index { get; set; }

        /// <summary>
        /// Координата узла
        /// </summary>
        public abstract Coordinate Coordinate { get; set; }

        /// <summary>
        /// Значение функции в узле
        /// null - игнорировать
        /// NodeValueConst - известное постоянное значение
        /// </summary>
        public NodeValue NodeValue { get; set; }

        /// <summary>
        /// Дифференциальный оператор, применяемый к данному узлу
        /// </summary>
        public DerivativeOperator DerivativeOperator { get; set; }

        /// <summary>
        /// Навигационный объект
        /// </summary>
        public abstract NodeNavigation NodeNavigation { get; set; }
    }
}