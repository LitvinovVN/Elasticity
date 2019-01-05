using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Простейший элемент геометрии
    /// </summary>    
    [XmlInclude(typeof(GeometryPrimitive1DLineSegment))]
    public abstract class GeometryPrimitive1D : GeometryPrimitive
    {
        /// <summary>
        /// Навигационное свойство для доступа
        /// к элементу геометрии (на 1 уровень выше)
        /// </summary>
        public override GeometryElement GeometryElement
        {
            get
            {
                return GeometryElement1D;
            }
            set
            {
                GeometryElement1D = (GeometryElement1D)value;
            }
        }

        /// <summary>
        /// Навигационное свойство для доступа
        /// к элементу геометрии ( на 1 уровень выше)
        /// </summary>
        [XmlIgnore]
        public GeometryElement1D GeometryElement1D { get; set; }

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

        /// <summary>
        /// Определяет принадлежность координаты примитиву
        /// (переопределение абстрактного метода базового класса)
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public override NodeLocationEnum IsCoordinateBelongsToGeometryPrimitive(Coordinate coordinate)
        {
            return IsCoordinateBelongsToGeometryPrimitive((Coordinate1D)coordinate);
        }

        /// <summary>
        /// Определяет принадлежность координаты примитиву
        /// </summary>
        /// <param name="coordinate1D"></param>
        /// <returns></returns>
        public abstract NodeLocationEnum IsCoordinateBelongsToGeometryPrimitive(Coordinate1D coordinate1D);
    }
}