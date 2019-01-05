using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Nodes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Элемент геометрии (абстрактный класс)
    /// </summary>
    [XmlInclude(typeof(GeometryElement1D))]
    [XmlInclude(typeof(GeometryElement2D))]
    [XmlInclude(typeof(GeometryElement3D))]
    public abstract class GeometryElement
    {
        /// <summary>
        /// Координата расположения элемента
        /// относительно координат расчетной области
        /// </summary>
        public abstract Coordinate CoordinateLocation { get; set; }

        /// <summary>
        /// Набор примитивов, входящих в элемент геометрии
        /// </summary>
        public List<GeometryPrimitive> GeometryPrimitives { get; set; } = new List<GeometryPrimitive>();
        
        /// <summary>
        /// Наборы слоёв
        /// </summary>
        public abstract GridLayers GetGridLayers { get; }

        /// <summary>
        /// Характеристика материала (модуль Юнга
        /// и пр. физ. параметры материала моделируемого объекта)
        /// </summary>
        public MaterialCharacteristic MaterialCharacteristic { get; set; }
            = new MaterialCharacteristic();

        /// <summary>
        /// Возвращает набор узлов геометрии,
        /// совпадающих с узлами сетки
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public abstract NodeSet GetNodeSet(GridLayers gridLayers);

        /// <summary>
        /// Добавляет примитив в элемент геометрии
        /// </summary>
        /// <param name="geometryPrimitive"></param>
        public void AddGeometryPrimitive(GeometryPrimitive geometryPrimitive)
        {
            geometryPrimitive.GeometryElement = this;
            GeometryPrimitives.Add(geometryPrimitive);
        }
    }
}