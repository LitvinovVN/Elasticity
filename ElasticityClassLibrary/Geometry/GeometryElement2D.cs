using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Nodes;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Отдельный элемент геометрии моделируемого объекта
    /// </summary>
    public class GeometryElement2D : GeometryElement
    {
        /// <summary>
        /// Координата расположения элемента
        /// относительно координат расчетной области
        /// </summary>
        public Coordinate2D CoordinateLocation2D { get; set; }

        public override Coordinate CoordinateLocation
        {
            get => CoordinateLocation2D;
            set => CoordinateLocation2D = (Coordinate2D)value;
        }

        
        /// <summary>
        /// Наборы слоёв
        /// </summary>
        public GridLayers2D GetGridLayers2D
        {
            get
            {
                GridLayers2D gridLayers2D = new GridLayers2D();

                foreach (var item in GeometryPrimitives)
                {
                    var gridLayers2DFromGeometryPrimitive = (GridLayers2D)item.GetGridLayers;
                    var coordinateLocation = (Coordinate2D)CoordinateLocation;

                    gridLayers2DFromGeometryPrimitive.GridLayers1.ForEach(l => l.Coordinate += coordinateLocation.X);
                    gridLayers2DFromGeometryPrimitive.GridLayers2.ForEach(l => l.Coordinate += coordinateLocation.Y);

                    gridLayers2D.Merge(gridLayers2DFromGeometryPrimitive);
                }                                                          
                
                return gridLayers2D;
            }
        }

        public override GridLayers GetGridLayers => GetGridLayers2D;

        #region Конструкторы
        /// <summary>
        /// Создаёт отдельный элемент геометрии моделируемого объекта
        /// в точке с заданными координатами
        /// </summary>
        /// <param name="сoordinateLocation">Координата расположения
        /// элемента геометрии относительно координат
        /// расчетной области</param>
        public GeometryElement2D(Coordinate2D сoordinateLocation)
        {
            CoordinateLocation = сoordinateLocation;
        }

        public GeometryElement2D()
        {
            
        }

        public override NodeSet GetNodeSet(GridLayers gridLayers)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}