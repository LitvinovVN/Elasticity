﻿using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Простейший элемент геометрии
    /// </summary>    
    [XmlInclude(typeof(GeometryPrimitive1D))]
    [XmlInclude(typeof(GeometryPrimitive2D))]
    [XmlInclude(typeof(GeometryPrimitive3D))]
    public abstract class GeometryPrimitive
    {
        /// <summary>
        /// Количество измерений объекта
        /// </summary>
        public abstract NumberOfDimensionsEnum NumberOfDimensionsEnum {get;set;}

        /// <summary>
        /// Полость (вырез)
        /// </summary>
        public abstract bool IsCavity { get; set; }

        /// <summary>
        /// Координата расположения примитива внутри геометрии,
        /// т.е. относительно координаты расположения элемента
        /// </summary>
        public abstract Coordinate CoordinateInElement { get; set; }

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public abstract GridLayers GetGridLayers { get; }
    }
}