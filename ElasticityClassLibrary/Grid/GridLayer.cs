using System;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Слой сетки с рассчитанными параметрами
    /// </summary>
    [Serializable]
    public class GridLayer
    {
        /// <summary>
        /// Индекс слоя
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// Координата слоя
        /// </summary>
        public decimal Coordinate { get; set; }

        /// <summary>
        /// Расчетные параметры слоя
        /// </summary>
        public GridLayerParameters GridLayerParameters { get; set; }

        #region Конструкторы
        /// <summary>
        /// Создаёт слой с заданными индексом и координатой
        /// </summary>
        /// <param name="index">Индекс, uint</param>
        /// <param name="coordinate">Координата, decimal</param>
        public GridLayer(uint index, decimal coordinate)
        {
            Index = index;
            Coordinate = coordinate;
        }

        public GridLayer()
        {

        }
        #endregion
    }
}