using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Простейший элемент геометрии
    /// </summary>    
    [XmlInclude(typeof(GeometryPrimitive1DLineSegment))]
    public abstract class GeometryPrimitive1D : GeometryPrimitive
    {
        /// <summary>
        /// Количество измерений
        /// </summary>
        public override NumberOfDimensionsEnum NumberOfDimensionsEnum { get; set; } = NumberOfDimensionsEnum.D1;

        /// <summary>
        /// Координата расположения примитива внутри геометрии,
        /// т.е. относительно координаты расположения элемента
        /// (переопределение свойства CoordinateInElement базового класса GeometryPrimitive)
        /// </summary>
        public override Coordinate CoordinateInElement
        {
            get
            {
                return CoordinateInElement1D;
            }
            set
            {
                CoordinateInElement1D = (Coordinate1D)value;
            }
        }

        /// <summary>
        /// Координата расположения примитива внутри геометрии,
        /// т.е. относительно координаты расположения элемента
        /// </summary>
        public abstract Coordinate1D CoordinateInElement1D { get; set; }

        public override GridLayers GetGridLayers
        {
            get
            {
                return GetGridLayers1D;
            }
        }

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public abstract GridLayers1D GetGridLayers1D { get; }        
    }
}