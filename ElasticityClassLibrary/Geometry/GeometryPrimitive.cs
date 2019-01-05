using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.Nodes;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Геометрический примитив
    /// (простейший элемент геометрии)
    /// </summary>    
    [XmlInclude(typeof(GeometryPrimitive1D))]
    [XmlInclude(typeof(GeometryPrimitive2D))]
    [XmlInclude(typeof(GeometryPrimitive3D))]
    public abstract class GeometryPrimitive
    {
        /// <summary>
        /// Навигационное свойство для доступа
        /// к элементу геометрии (1 уровень выше)
        /// </summary>
        public abstract GeometryElement GeometryElement { get; set; } 

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

        /// <summary>
        /// Возвращает набор узлов сетки, входящих в примитив
        /// </summary>
        /// <param name="gridLayers"></param>
        /// <returns></returns>
        public abstract NodeSet GetNodeSet(GridLayers gridLayers);

        /// <summary>
        /// Определяет, принадлежит ли координата примитиву
        /// и возвращает соответствующий объект перечисления NodeLocationEnum
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public abstract NodeLocationEnum IsCoordinateBelongsToGeometryPrimitive(Coordinate coordinate);
    }
}