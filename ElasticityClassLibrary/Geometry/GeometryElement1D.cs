using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Nodes;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Отдельный элемент геометрии моделируемого объекта
    /// </summary>
    public class GeometryElement1D : GeometryElement
    {
        /// <summary>
        /// Координата расположения элемента
        /// относительно координат расчетной области
        /// </summary>
        public Coordinate1D CoordinateLocation1D { get; set; }

        public override Coordinate CoordinateLocation
        {
            get => CoordinateLocation1D;
            set => CoordinateLocation1D = (Coordinate1D)value;
        }

        
        /// <summary>
        /// Наборы слоёв
        /// </summary>
        public GridLayers1D GetGridLayers1D
        {
            get
            {
                GridLayers1D gridLayers1D = new GridLayers1D();

                foreach (var item in GeometryPrimitives)
                {
                    var gridLayers1DFromGeometryPrimitive = (GridLayers1D)item.GetGridLayers;
                    var coordinateLocation = (Coordinate1D)CoordinateLocation;

                    gridLayers1DFromGeometryPrimitive.GridLayers.ForEach(l => l.Coordinate += coordinateLocation.X);                    

                    gridLayers1D.Merge(gridLayers1DFromGeometryPrimitive);
                }                                                          
                
                return gridLayers1D;
            }
        }

        public override GridLayers GetGridLayers => GetGridLayers1D;

        #region Конструкторы
        /// <summary>
        /// Создаёт отдельный элемент геометрии моделируемого объекта
        /// в точке с заданными координатами
        /// </summary>
        /// <param name="сoordinateLocation">Координата расположения
        /// элемента геометрии относительно координат
        /// расчетной области</param>
        public GeometryElement1D(Coordinate1D сoordinateLocation)
        {
            CoordinateLocation = сoordinateLocation;
        }

        public GeometryElement1D()
        {
            
        }
        #endregion

        public override NodeSet GetNodeSet(GridLayers gridLayers)
        {
            NodeSet1D nodeSet1D = new NodeSet1D();

            foreach (var geometryPrimitive in GeometryPrimitives)
            {
                nodeSet1D.Merge(geometryPrimitive.GetNodeSet(gridLayers));
            }

            return nodeSet1D;
        }

        
    }
}