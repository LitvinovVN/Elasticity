using System;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Характеристика материала (модуль Юнга
    /// и пр. физ. параметры материала моделируемого объекта)
    /// </summary>
    [Serializable]
    public class MaterialCharacteristic
    {
        /// <summary>
        /// Наименование материала
        /// </summary>
        public string MaterialName { get; set; } = "Сталь";

        /// <summary>
        /// Модуль Юнга, Па
        /// </summary>
        public decimal E { get; set; } = 200_000_000_000m;

        /// <summary>
        /// Плотность, кг/м.куб.
        /// </summary>
        public decimal ro { get; set; } = 7800m;

        /// <summary>
        /// Коэффициент Пуассона, о.е.
        /// </summary>
        public decimal nu { get; set; } = 0.30m;

        /// <summary>
        /// Модуль сдвига
        /// </summary>
        public decimal G
        {
            get
            {
                return E / (2 * (1 + nu));
            }
        }

        #region Конструкторы
        public MaterialCharacteristic()
        {

        }
        #endregion
    }
}