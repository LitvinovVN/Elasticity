using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Простейший элемент геометрии
    /// </summary>    
    [XmlInclude(typeof(GeometryPrimitiveParallelepiped))]
    [XmlInclude(typeof(GeometryPrimitiveCube))]
    public abstract class GeometryPrimitive3D : GeometryPrimitive
    {
        /// <summary>
        /// Количество измерений
        /// </summary>
        public override NumberOfDimensionsEnum NumberOfDimensionsEnum { get; set; } = NumberOfDimensionsEnum.D3;

        public override GridLayers GetGridLayers => GetGridLayers3D;

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public abstract GridLayers3D GetGridLayers3D { get; }

        public override Coordinate CoordinateInElement { get => CoordinateInElement3D; set => CoordinateInElement3D = (Coordinate3D)value; }
        /// <summary>
        /// Координата расположения примитива внутри геометрии,
        /// т.е. относительно координаты расположения элемента
        /// </summary>
        public abstract Coordinate3D CoordinateInElement3D { get; set; }

        
    }
}