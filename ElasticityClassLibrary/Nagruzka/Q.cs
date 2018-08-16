namespace ElasticityClassLibrary.NagruzkaNamespace
{
    /// <summary>
    /// Значение распределённой нагрузки
    /// </summary>
    public class Q
    {
        /// <summary>
        /// Проекция нагрузки на ось X
        /// </summary>
        public decimal Qx { get; set; }

        /// <summary>
        /// Проекция нагрузки на ось Y
        /// </summary>
        public decimal Qy { get; set; }

        /// <summary>
        /// Проекция нагрузки на ось Z
        /// </summary>
        public decimal Qz { get; set; }

        #region Конструкторы
        public Q(decimal qx, decimal qy, decimal qz)
        {
            Qx = qx;
            Qy = qy;
            Qz = qz;
        }

        public Q()
        {

        }
        #endregion
    }
}