namespace ElasticityClassLibrary
{
    /// <summary>
    /// Модель перехода шага сетки
    /// </summary>
    public class StepTransition
    {
        /// <summary>
        ///  Индекс перехода
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// Шаг сетки до предыдущего перехода либо узла 0
        /// </summary>
        public decimal StepValue { get; set; }
        
        /// <summary>
        /// Координата перехода
        /// </summary>
        public decimal Coordinate { get; set; }

        #region Конструкторы
        public StepTransition(uint index, decimal stepValue, decimal coordinate)
        {
            Index = index;
            StepValue = stepValue;
            Coordinate = coordinate;
        }

        public StepTransition()
        {

        }
        #endregion
    }
}