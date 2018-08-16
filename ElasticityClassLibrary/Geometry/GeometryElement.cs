using ElasticityClassLibrary.GridNamespace;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GeometryNamespase
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

        /// <summary>
        /// Наборы слоёв
        /// </summary>
        public GridLayers3D GetGridLayers3D
        {
            get
            {
                GridLayers3D gridLayers3D = new GridLayers3D();

                foreach (var item in GeometryPrimitives)
                {
                    var gridLayers3DFromGeometryPrimitive = item.GetGridLayers3D;

                    gridLayers3DFromGeometryPrimitive.GridLayersX.ForEach(l => l.Coordinate += CoordinateLocation.X);
                    gridLayers3DFromGeometryPrimitive.GridLayersY.ForEach(l => l.Coordinate += CoordinateLocation.Y);
                    gridLayers3DFromGeometryPrimitive.GridLayersZ.ForEach(l => l.Coordinate += CoordinateLocation.Z);

                    gridLayers3D.Merge(gridLayers3DFromGeometryPrimitive);
                }                                                          
                
                return gridLayers3D;
            }
        }

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