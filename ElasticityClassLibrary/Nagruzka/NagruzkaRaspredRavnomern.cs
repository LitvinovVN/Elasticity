using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.NagruzkaNamespace
{
    /// <summary>
    /// Распределённая равномерная нагрузка, Н/м.кв.
    /// </summary>
    public class NagruzkaRaspredRavnomern : Nagruzka
    {
        /// <summary>
        /// Значение распределённой нагрузки
        /// </summary>
        public Q Q { get; set; }

        /// <summary>
        /// Область действия нагрузки
        /// </summary>
        public Area Area { get; set; }
    }
}
