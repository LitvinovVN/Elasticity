﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.Derivatives
{
    /// <summary>
    /// Одномерный семиточечный оператор для вычисления производной
    /// </summary>
    public class DerivativeOperator1D7P : DerivativeOperator1D
    {
        /// <summary>
        /// Преобразовать оператор в массив
        /// </summary>
        /// <returns></returns>
        public override double[] ConvertToArrayDerivativeOperator()
        {
            throw new NotImplementedException();
        }
    }
}
