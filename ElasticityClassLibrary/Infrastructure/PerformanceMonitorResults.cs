using System;

namespace ElasticityClassLibrary
{
    /// <summary>
    /// Результат работы монитора производительности
    /// </summary>
    public class PerformanceMonitorResults
    {
        /// <summary>
        /// Использованная физическая память
        /// </summary>
        public long BytesPhysicalUsed { get; set; }

        /// <summary>
        /// Использованная виртуальная память
        /// </summary>
        public long BytesVirtualUsed { get; set; }

        /// <summary>
        /// Затрачено времени, TimeSpan
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// Затрачено времени, мс
        /// </summary>
        public long ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Переопределённый метод ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = $"Использованная физическая память: {BytesPhysicalUsed:N0} байт.\n" +
                $"Использованная виртуальная память: {BytesVirtualUsed:N0} байт.\n" +
                $"Затрачено времени: {ElapsedMilliseconds} мс";
            return result;
        }
    }
}