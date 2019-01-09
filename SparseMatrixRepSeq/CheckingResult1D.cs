namespace SparseMatrixRepSeqNamespace
{
    /// <summary>
    /// Модель результатов проверки расчетов для одного узла одномерной сетки
    /// </summary>
    public class CheckingResult1D
    {
        /// <summary>
        /// Координата
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Правильное значение
        /// </summary>
        public double ValueCorrect { get; set; }

        /// <summary>
        /// Рассчитанное значение
        /// </summary>
        public double ValueCalculated { get; set; }

        /// <summary>
        /// Абсолютное отклонение
        /// </summary>
        public double GetAbsoluteDeviation
        {
            get
            {
                return ValueCalculated - ValueCorrect;
            }
        }

        /// <summary>
        /// Относительное  отклонение, %
        /// </summary>
        public double GetRelativeDeviation
        {
            get
            {
                return (ValueCalculated - ValueCorrect) * 100 / ValueCorrect;
            }
        }        
    }
}