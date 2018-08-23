using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Списки слоёв по осям X, Y, Z
    /// </summary>
    [Serializable]
    public class GridLayers3D : GridLayers
    {
        /// <summary>
        /// Набор слоёв по оси X
        /// </summary>
        public List<GridLayer> GridLayersX { get; set; } = new List<GridLayer>();

        /// <summary>
        /// Набор слоёв по оси Y
        /// </summary>
        public List<GridLayer> GridLayersY { get; set; } = new List<GridLayer>();

        /// <summary>
        /// Набор слоёв по оси Z
        /// </summary>
        public List<GridLayer> GridLayersZ { get; set; } = new List<GridLayer>();

        public GridLayers3D()
        {

        }

        /// <summary>
        /// Добавляет слои из переданного объекта в текущий
        /// </summary>
        /// <param name="gridLayers3D">Объект GridLayers3D</param>
        public void Merge(GridLayers3D gridLayers3D)
        {
            if (gridLayers3D == null) return;

            GridLayersX = MergeGridLayerLists(GridLayersX, gridLayers3D.GridLayersX);
            GridLayersY = MergeGridLayerLists(GridLayersY, gridLayers3D.GridLayersY);
            GridLayersZ = MergeGridLayerLists(GridLayersZ, gridLayers3D.GridLayersZ);
        }
               
        /// <summary>
        /// Вычисляет параметры сетки для всех наборов слоёв
        /// </summary>
        public void CalculateGridLayers3DParameters()
        {
            CalculateGridLayerParameters(GridLayersX);
            CalculateGridLayerParameters(GridLayersY);
            CalculateGridLayerParameters(GridLayersZ);
        }                
    }
}