using System.Collections.Generic;

namespace ElasticityClassLibrary.Geometry
{
    /// <summary>
    /// Отдельный элемент геометрии моделируемого объекта
    /// </summary>
    public class GeometryElement
    {
        /// <summary>
        /// Координата расположения элемента
        /// относительно координат расчетной области
        /// </summary>
        public Coordinate3D CoordinateLocation { get; set; }

        /// <summary>
        /// Набор примитивов, входящих в элемент геометрии
        /// </summary>
        public List<GeometryPrimitive> GeometryPrimitives { get; set; }
            = new List<GeometryPrimitive>();

        /// <summary>
        /// Характеристика материала (модуль Юнга
        /// и пр. физ. параметры материала моделируемого объекта)
        /// </summary>
        public MaterialCharacteristic MaterialCharacteristic { get; set; }
            = new MaterialCharacteristic();

        #region Конструкторы
        /// <summary>
        /// Создаёт отдельный элемент геометрии моделируемого объекта
        /// в точке с заданными координатами
        /// </summary>
        /// <param name="сoordinateLocation">Координата расположения
        /// элемента геометрии относительно координат
        /// расчетной области</param>
        public GeometryElement(Coordinate3D сoordinateLocation)
        {
            CoordinateLocation = сoordinateLocation;
        }

        public GeometryElement()
        {
            
        }
        #endregion
    }
}