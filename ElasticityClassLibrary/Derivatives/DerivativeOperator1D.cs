using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.Derivatives
{
    /// <summary>
    /// Одномерный оператор для вычисления производных
    /// </summary>
    [XmlInclude(typeof(DerivativeOperator1D3P))]
    [XmlInclude(typeof(DerivativeOperator1D5P))]
    [XmlInclude(typeof(DerivativeOperator1D7P))]
    public abstract class DerivativeOperator1D : DerivativeOperator
    {
    }
}
