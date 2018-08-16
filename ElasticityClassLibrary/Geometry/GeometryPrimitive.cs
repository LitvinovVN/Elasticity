using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Простейший элемент геометрии
    /// </summary>
    [XmlInclude(typeof(GeometryPrimitiveParallelepiped))]
    [XmlInclude(typeof(GeometryPrimitiveCube))]
    public abstract class GeometryPrimitive
    {
        /// <summary>
        /// Полость (вырез)
        /// </summary>
        public abstract bool IsCavity { get; set; }

        /// <summary>
        /// Координата расположения примитива внутри геометрии,
        /// т.е. относительно координаты расположения элемента
        /// </summary>
        public abstract Coordinate3D CoordinateInElement { get; set; }

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public abstract GridLayers3D GetGridLayers3D { get; }
    }
}