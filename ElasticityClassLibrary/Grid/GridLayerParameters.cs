namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Расчетные параметры слоя
    /// </summary>
    public class GridLayerParameters
    {
        /* 
         *      |                 |                |                 |               |               |              |
         *     i-3               i-2              i-1                i              i+1             i+2            i+3
         *         Step3Backwards   Step2Backwards   Step1Backwards    Step1Forwards    Step2Forwards   Step3Forwards
         */

        /// <summary>
        /// Величина первого шага вперёд по сетке
        /// (расстояние от текущего слоя (i) до следующего (i+1)
        /// </summary>
        public decimal? Step1Forwards { get; set; }

        /// <summary>
        /// Величина второго шага вперёд по сетке
        /// (расстояние от i+1 до i+2)
        /// </summary>
        public decimal? Step2Forwards { get; set; }

        /// <summary>
        /// Величина третьего шага вперёд по сетке
        /// (расстояние от i+2 до i+3)
        /// </summary>
        public decimal? Step3Forwards { get; set; }

        /// <summary>
        /// Величина первого шага назад по сетке
        /// (расстояние от текущего слоя (i) до предыдущего (i-1)
        /// </summary>
        public decimal? Step1Backwards { get; set; }
        
        /// <summary>
        /// Величина второго шага назад по сетке
        /// (расстояние от i-1 до i-2)
        /// </summary>
        public decimal? Step2Backwards { get; set; }
        
        /// <summary>
        /// Величина третьего шага назад по сетке
        /// (расстояние от i-2 до i-3)
        /// </summary>
        public decimal? Step3Backwards { get; set; }

        /// <summary>
        /// Оператор для вычисления первой производной
        /// </summary>
        public DerivativeComputingOperator1D Derivative1 { get; set; }
    }
}