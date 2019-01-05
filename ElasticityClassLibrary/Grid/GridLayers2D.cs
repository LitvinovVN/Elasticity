using ElasticityClassLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Список слоёв по одной оси
    /// </summary>
    [Serializable]
    public class GridLayers2D : GridLayers
    {
        /// <summary>
        /// Ось координат 1 (по умолчанию - Х)
        /// </summary>
        public AxisEnum AxisEnum1 { get; set; } = AxisEnum.X;

        /// <summary>
        /// Ось координат 2 (по умолчанию - Y)
        /// </summary>
        public AxisEnum AxisEnum2 { get; set; } = AxisEnum.Y;

        /// <summary>
        /// Набор слоёв по оси 1
        /// </summary>
        public List<GridLayer> GridLayers1 { get; set; } = new List<GridLayer>();

        /// <summary>
        /// Набор слоёв по оси 2
        /// </summary>
        public List<GridLayer> GridLayers2 { get; set; } = new List<GridLayer>();


        public GridLayers2D()
        {

        }

        /// <summary>
        /// Добавляет слои из переданного объекта в текущий
        /// </summary>
        /// <param name="gridLayers2D">Объект GridLayers2D</param>
        public void Merge(GridLayers2D gridLayers2D)
        {
            if (gridLayers2D == null) return;

            GridLayers1 = MergeGridLayerLists(GridLayers1, gridLayers2D.GridLayers1);
            GridLayers2 = MergeGridLayerLists(GridLayers2, gridLayers2D.GridLayers2);
        }

        

        /// <summary>
        /// Вычисляет параметры сетки для всех наборов слоёв
        /// </summary>
        public void CalculateGridLayers2DParameters()
        {
            CalculateGridLayerParameters(GridLayers1);
            CalculateGridLayerParameters(GridLayers2);
        }        
    }
}