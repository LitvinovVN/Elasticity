using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Двумерные элементы геометрии
    /// </summary>        
    public abstract class GeometryPrimitive2D : GeometryPrimitive
    {
        /// <summary>
        /// Количество измерений
        /// </summary>
        public override NumberOfDimensionsEnum NumberOfDimensionsEnum { get; set; } = NumberOfDimensionsEnum.D2;

        /// <summary>
        /// Полость (вырез)
        /// </summary>
        //public abstract bool IsCavity { get; set; }

        /// <summary>
        /// Координата расположения примитива внутри геометрии,
        /// т.е. относительно координаты расположения элемента
        /// </summary>
        //public abstract Coordinate1D CoordinateInElement { get; set; }

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public abstract GridLayers1D GetGridLayers2D { get; }
    }
}