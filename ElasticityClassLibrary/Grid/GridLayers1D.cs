using ElasticityClassLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Список слоёв по одной оси
    /// </summary>
    [Serializable]
    public class GridLayers1D : GridLayers
    {
        /// <summary>
        /// Ось координат (по умолчанию - Х)
        /// </summary>
        public AxisEnum AxisEnum { get; set; } = AxisEnum.X;

        /// <summary>
        /// Набор слоёв по оси
        /// </summary>
        public List<GridLayer> GridLayers { get; set; } = new List<GridLayer>();

        #region Конструкторы
        public GridLayers1D()
        {

        }
        #endregion

        /// <summary>
        /// Добавляет слои из переданного объекта в текущий
        /// </summary>
        /// <param name="gridLayers3D">Объект GridLayers3D</param>
        public void Merge(GridLayers1D gridLayers1D)
        {
            if (gridLayers1D == null) return;

            GridLayers = MergeGridLayerLists(GridLayers, gridLayers1D.GridLayers);            
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