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
        /// Ось координат (по умолчанию - Х)
        /// </summary>
        public AxisEnum AxisEnum { get; set; } = AxisEnum.X;

        /// <summary>
        /// Набор слоёв по оси
        /// </summary>
        public List<GridLayer> GridLayers { get; set; } = new List<GridLayer>();
                

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

            GridLayers = MergeGridLayerLists(GridLayers, gridLayers2D.GridLayers);            
        }

        

        /// <summary>
        /// Вычисляет параметры сетки для всех наборов слоёв
        /// </summary>
        public void CalculateGridLayers1DParameters()
        {
            CalculateGridLayerParameters(GridLayers);            
        }        
    }
}