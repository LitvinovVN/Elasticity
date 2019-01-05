using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Nodes;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Отдельный элемент геометрии моделируемого объекта
    /// </summary>
    public class GeometryElement3D : GeometryElement
    {
        /// <summary>
        /// Координата расположения элемента
        /// относительно координат расчетной области
        /// </summary>
        public Coordinate3D CoordinateLocation3D { get; set; }

        public override Coordinate CoordinateLocation
        {
            get => CoordinateLocation3D;
            set => CoordinateLocation3D = (Coordinate3D)value;
        }

        
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
                    var gridLayers3DFromGeometryPrimitive = (GridLayers3D)item.GetGridLayers;
                    var coordinateLocation = (Coordinate3D)CoordinateLocation;

                    gridLayers3DFromGeometryPrimitive.GridLayersX.ForEach(l => l.Coordinate += coordinateLocation.X);
                    gridLayers3DFromGeometryPrimitive.GridLayersY.ForEach(l => l.Coordinate += coordinateLocation.Y);
                    gridLayers3DFromGeometryPrimitive.GridLayersZ.ForEach(l => l.Coordinate += coordinateLocation.Z);

                    gridLayers3D.Merge(gridLayers3DFromGeometryPrimitive);
                }                                                          
                
                return gridLayers3D;
            }
        }

        public override GridLayers GetGridLayers => GetGridLayers3D;

        #region Конструкторы
        /// <summary>
        /// Создаёт отдельный элемент геометрии моделируемого объекта
        /// в точке с заданными координатами
        /// </summary>
        /// <param name="сoordinateLocation">Координата расположения
        /// элемента геометрии относительно координат
        /// расчетной области</param>
        public GeometryElement3D(Coordinate3D сoordinateLocation)
        {
            CoordinateLocation = сoordinateLocation;
        }

        public GeometryElement3D()
        {
            
        }
        #endregion

        public override NodeSet GetNodeSet(GridLayers gridLayers)
        {
            throw new NotImplementedException();
        }
    }
}