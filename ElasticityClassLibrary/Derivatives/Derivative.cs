using System;

namespace ElasticityClassLibrary.Derivatives
{
    /// <summary>
    /// Фабрика производных
    /// </summary>
    public static class Derivative
    {
        /// <summary>
        /// Создаёт трёхточечный оператор вычисления
        /// первой производной 
        /// </summary>
        /// <param name="alfa1B">Отношение длины левого отрезка
        /// к величине шага</param>
        /// <param name="alfa1F">Отношение длины правого отрезка
        /// к величине шага</param>
        /// <param name="h">Шаг</param>
        /// <returns></returns>
        public static DerivativeOperator GetDerivativeOperator1D3P_Derivative1(decimal alfa1B,
            decimal alfa1F,
            decimal h)
        {
            DerivativeOperator1D3P op = new DerivativeOperator1D3P(alfa1B,
                alfa1F, h);                                             
            return op;
        }

        /// <summary>
        /// Создаёт трёхточечный оператор вычисления
        /// второй производной 
        /// </summary>
        /// <param name="alfa1B">Отношение длины левого отрезка
        /// к величине шага</param>
        /// <param name="alfa1F">Отношение длины правого отрезка
        /// к величине шага</param>
        /// <param name="h">Шаг</param>
        /// <returns></returns>
        public static DerivativeOperator GetDerivativeOperator1D3P_Derivative2(decimal alfa1B,
            decimal alfa1F,
            decimal h)
        {
            DerivativeOperator1D3P op = new DerivativeOperator1D3P(alfa1B,
                alfa1F, h, DerivativeTypeEnum.D2);
            return op;
        }

        /// <summary>
        /// Создаёт пятиточечный оператор вычисления
        /// третьей производной
        /// </summary>
        /// <param name="getH"></param>
        /// <returns></returns>
        public static DerivativeOperator GetDerivativeOperator1D5P_Derivative3(decimal h)
        {
            DerivativeOperator1D5P op = new DerivativeOperator1D5P(h, DerivativeTypeEnum.D3);
            return op;
        }
    }
}