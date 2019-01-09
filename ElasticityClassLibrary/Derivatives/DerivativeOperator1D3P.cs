using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.Derivatives
{
    /// <summary>
    /// Одномерный трёхточечный оператор для вычисления производной
    /// </summary>
    public class DerivativeOperator1D3P : DerivativeOperator1D
    {
        #region Свойства
        /// <summary>
        /// Коэффициент в точке (i-1)
        /// </summary>
        public decimal Kim1 { get; set; }

        /// <summary>
        /// Коэффициент в точке (i)
        /// </summary>
        public decimal Ki   { get; set; }

        /// <summary>
        /// Коэффициент в точке (i+1)
        /// </summary>
        public decimal Kip1 { get; set; }
        #endregion

        #region Конструкторы
        public DerivativeOperator1D3P()
        {

        }

        /// <summary>
        /// Создаёт обект на основе коэффициентов alfa1B, alfa1F
        /// и шага h
        /// </summary>
        /// <param name="alfa1B"></param>
        /// <param name="alfa1F"></param>
        /// <param name="h">Шаг</param>
        /// <param name="derivativeType">Тип производной</param>
        public DerivativeOperator1D3P(decimal alfa1B,
            decimal alfa1F,
            decimal h,
            DerivativeTypeEnum derivativeType = DerivativeTypeEnum.D1)
        {
            switch(derivativeType)
            {
                case DerivativeTypeEnum.D1:
                    CalcDerivativeOperator1D3P_D1(alfa1B, alfa1F, h);
                    break;
                case DerivativeTypeEnum.D2:
                    CalcDerivativeOperator1D3P_D2(alfa1B, alfa1F, h);
                    break;
            }            
        }
        #endregion

        #region Переопределение операторов
        public static DerivativeOperator1D3P operator + (DerivativeOperator1D3P op1, DerivativeOperator1D3P op2)
        {
            DerivativeOperator1D3P result = new DerivativeOperator1D3P();
            result.Kim1 = op1.Kim1 + op2.Kim1;
            result.Ki   = op1.Ki + op2.Ki;
            result.Kip1 = op1.Kip1 + op2.Kip1;

            return result;
        }

        public static DerivativeOperator1D3P operator + (decimal d, DerivativeOperator1D3P op)
        {
            DerivativeOperator1D3P result = new DerivativeOperator1D3P();
            result.Kim1 = d + op.Kim1;
            result.Ki = d + op.Ki;
            result.Kip1 = d + op.Kip1;

            return result;
        }

        public static DerivativeOperator1D3P operator + (DerivativeOperator1D3P op, decimal d)
        {
            DerivativeOperator1D3P result = new DerivativeOperator1D3P();
            result.Kim1 = op.Kim1 + d;
            result.Ki = op.Ki + d;
            result.Kip1 = op.Kip1 + d;

            return result;
        }

        public static DerivativeOperator1D3P operator * (decimal d, DerivativeOperator1D3P op)
        {
            DerivativeOperator1D3P result = new DerivativeOperator1D3P();
            result.Kim1 = d * op.Kim1;
            result.Ki = d * op.Ki;
            result.Kip1 = d * op.Kip1;

            return result;
        }

        public static DerivativeOperator1D3P operator *(DerivativeOperator1D3P op, decimal d)
        {
            DerivativeOperator1D3P result = new DerivativeOperator1D3P();
            result.Kim1 = d * op.Kim1;
            result.Ki = d * op.Ki;
            result.Kip1 = d * op.Kip1;

            return result;
        }
        #endregion

        #region Вычисление производных
        /// <summary>
        /// Вычисление первой производной на трёхточечном шаблоне
        /// </summary>
        /// <param name="alfa1B"></param>
        /// <param name="alfa1F"></param>
        /// <param name="h"></param>
        private void CalcDerivativeOperator1D3P_D1(decimal alfa1B, decimal alfa1F, decimal h)
        {
            decimal kh = 1 / ((alfa1B + alfa1F) * h);

            Kim1 = -kh;
            Ki = 0;
            Kip1 = kh;
        }

        /// <summary>
        ///  Вычисление второй производной на трёхточечном шаблоне
        /// </summary>
        /// <param name="alfa1B"></param>
        /// <param name="alfa1F"></param>
        /// <param name="h"></param>
        private void CalcDerivativeOperator1D3P_D2(decimal alfa1B, decimal alfa1F, decimal h)
        {
            decimal kh = 2 / ( (alfa1B + alfa1F) * alfa1B * alfa1F * h * h );

            Kim1 = alfa1F * kh;
            Ki = -(alfa1B + alfa1F) * kh;
            Kip1 = alfa1B * kh;            
        }

        /// <summary>
        /// Преобразовывает оператор в массив типа double
        /// </summary>
        /// <returns></returns>
        public override double[] ConvertToArrayDerivativeOperator()
        {
            double[] op = new double[] { (double)Kim1, (double)Ki, (double)Kip1 };
            return op;
        }
        #endregion
    }
}
