using System.Collections.Generic;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.Derivatives
{
    /// <summary>
    /// Оператор для вычисления производной (абстрактный класс)
    /// </summary>
    [XmlInclude(typeof(DerivativeOperator1D))]
    [XmlInclude(typeof(DerivativeOperator2D))]
    [XmlInclude(typeof(DerivativeOperator3D))]
    public abstract class DerivativeOperator
    {
        #region Конструкторы
        public DerivativeOperator()
        {
            
        }
        #endregion
    }
}