using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.Derivatives
{
    /// <summary>
    /// Одномерный пятиточечный оператор для вычисления производной
    /// </summary>
    public class DerivativeOperator1D5P : DerivativeOperator1D
    {
        #region Свойства
        /// <summary>
        /// Коэффициент в точке (i-2)
        /// </summary>
        public decimal Kim2 { get; set; }

        /// <summary>
        /// Коэффициент в точке (i-1)
        /// </summary>
        public decimal Kim1 { get; set; }

        /// <summary>
        /// Коэффициент в точке (i)
        /// </summary>
        public decimal Ki { get; set; }

        /// <summary>
        /// Коэффициент в точке (i+1)
        /// </summary>
        public decimal Kip1 { get; set; }

        /// <summary>
        /// Коэффициент в точке (i+2)
        /// </summary>
        public decimal Kip2 { get; set; }
        #endregion

        #region Конструкторы
        public DerivativeOperator1D5P()
        {
                
        }

        /// <summary>
        /// Конструктор одномерного пятиточечного оператора для равномерной сетки
        /// </summary>
        /// <param name="h">Шаг равномерной сетки</param>
        /// <param name="derivativeType">Тип производной</param>
        public DerivativeOperator1D5P(decimal h, DerivativeTypeEnum derivativeType)
        {
            switch (derivativeType)
            {
                case DerivativeTypeEnum.D1:
                    CalcDerivativeOperator1D5P_D1(h);
                    break;
                case DerivativeTypeEnum.D2:
                    CalcDerivativeOperator1D5P_D2(h);
                    break;
                case DerivativeTypeEnum.D3:
                    CalcDerivativeOperator1D5P_D3(h);
                    break;
                case DerivativeTypeEnum.D4:
                    CalcDerivativeOperator1D5P_D4(h);
                    break;
            }            
        }
                
        #endregion

        /// <summary>
        /// Преобразовывает оператор в массив типа double
        /// </summary>
        /// <returns></returns>
        public override double[] ConvertToArrayDerivativeOperator()
        {
            return new double[]
            {
                (double)Kim2,
                (double)Kim1,
                (double)Ki,
                (double)Kip1,
                (double)Kip2
            };
        }


        #region Переопределение операторов
        public static DerivativeOperator1D5P operator +(DerivativeOperator1D5P op1, DerivativeOperator1D5P op2)
        {
            DerivativeOperator1D5P result = new DerivativeOperator1D5P();
            result.Kim2 = op1.Kim2 + op2.Kim2;
            result.Kim1 = op1.Kim1 + op2.Kim1;
            result.Ki   = op1.Ki   + op2.Ki;
            result.Kip1 = op1.Kip1 + op2.Kip1;
            result.Kip2 = op1.Kip2 + op2.Kip2;

            return result;
        }

        public static DerivativeOperator1D5P operator +(decimal d, DerivativeOperator1D5P op)
        {
            DerivativeOperator1D5P result = new DerivativeOperator1D5P();
            result.Kim2 = d + op.Kim2;
            result.Kim1 = d + op.Kim1;
            result.Ki   = d + op.Ki;
            result.Kip1 = d + op.Kip1;
            result.Kip2 = d + op.Kip2;

            return result;
        }

        public static DerivativeOperator1D5P operator +(DerivativeOperator1D5P op, decimal d)
        {
            DerivativeOperator1D5P result = new DerivativeOperator1D5P();
            result.Kim2 = op.Kim2 + d;
            result.Kim1 = op.Kim1 + d;
            result.Ki   = op.Ki   + d;
            result.Kip1 = op.Kip1 + d;
            result.Kip2 = op.Kip2 + d;

            return result;
        }

        public static DerivativeOperator1D5P operator *(decimal d, DerivativeOperator1D5P op)
        {
            DerivativeOperator1D5P result = new DerivativeOperator1D5P();
            result.Kim2 = d * op.Kim2;
            result.Kim1 = d * op.Kim1;
            result.Ki   = d * op.Ki;
            result.Kip1 = d * op.Kip1;
            result.Kip2 = d * op.Kip2;

            return result;
        }

        public static DerivativeOperator1D5P operator *(DerivativeOperator1D5P op, decimal d)
        {
            DerivativeOperator1D5P result = new DerivativeOperator1D5P();
            result.Kim2 = d * op.Kim2;
            result.Kim1 = d * op.Kim1;
            result.Ki   = d * op.Ki;
            result.Kip1 = d * op.Kip1;
            result.Kip2 = d * op.Kip2;

            return result;
        }
        #endregion

        #region Вычисление производных

        // http://mydebianblog.blogspot.com/2009/01/latex-math-in-latex.html
        // Справочник обозначений LaTeX
        // http://mirror.macomnet.net/pub/CTAN/info/symbols/comprehensive/symbols-a4.pdf
        //tex:
        // \begin{equation} 
        //      f(x, y,\alpha, \beta) = \frac
        //        {\sum \limits_{n=1}^{\infty} A_n \cos \left( \frac{ 2 n \pi x}
        //        {\nu} \right)} {\prod \mathcal{F} {g(x, y)} } 
        // \end{equation}
        // Вторая производная:    $$\frac{d^2y}{dx^2} \approx \frac{y_{i+1} - 2y_{i} + y_{i-1}}{h^2}$$
        // Третья производная:    $$\frac{d^3y}{dx^3} \approx \frac{y_{i+2} - 2y_{i+1} + 2y_{i-1} - y_{i-2}}{2h^3}$$
        // Четвёртая производная: $$\frac{d^4y}{dx^4} \approx \frac{y_{i+2} - 4y_{i+1} + 6y_{i} - 4y_{i-1} + y_{i-2}}{h^4}$$
        
        #region 1 производная
        /// <summary>
        /// Вычисление первой производной
        /// на пятиточечном шаблоне
        /// для неравномерной сетки
        /// </summary>
        /// <param name="alfa1B"></param>
        /// <param name="alfa1F"></param>
        /// <param name="h">Базовый шаг</param>
        private void CalcDerivativeOperator1D5P_D1(decimal alfa1B,
            decimal alfa1F,
            decimal h)
        {
            decimal kh = 1 / ((alfa1B + alfa1F) * h);

            Kim1 = -kh;
            Ki = 0;
            Kip1 = kh;
        }

        /// <summary>
        /// Вычисление первой производной
        /// на пятиточечном шаблоне
        /// для равномерной сетки
        /// </summary>
        /// <param name="h">Шаг равномерной сетки</param>
        private void CalcDerivativeOperator1D5P_D1(decimal h)
        {
            CalcDerivativeOperator1D5P_D1(1, 1, h);
        }
        #endregion

        #region 2 производная
        /// <summary>
        ///  Вычисление второй производной
        ///  на пятиточечном шаблоне
        ///  для неравномерной сетки
        /// </summary>
        /// <param name="alfa1B"></param>
        /// <param name="alfa1F"></param>
        /// <param name="h">Базовый шаг</param>
        private void CalcDerivativeOperator1D5P_D2(decimal alfa1B,
            decimal alfa1F, decimal h)
        {
            decimal kh = 2 / ((alfa1B + alfa1F) * alfa1B * alfa1F * h * h);

            Kim1 = alfa1F * kh;
            Ki = -(alfa1B + alfa1F) * kh;
            Kip1 = alfa1B * kh;
        }

        /// <summary>
        /// Вычисление второй производной
        /// на пятиточечном шаблоне
        /// для равномерной сетки
        /// </summary>
        /// <param name="h">Шаг сетки</param>
        private void CalcDerivativeOperator1D5P_D2(decimal h)
        {
            CalcDerivativeOperator1D5P_D2(1, 1, h);
        }
        #endregion               

        #region 3 производная
        /// <summary>
        /// Вычисление третьей производной
        /// на пятиточечном шаблоне
        /// для равномерной сетки
        /// </summary>
        /// <param name="h">Шаг сетки</param>
        private void CalcDerivativeOperator1D5P_D3(decimal h)
        {
            decimal k = 1 / (2 * h * h * h);

            Kim2 = -k;
            Kim1 = 2 * k;
            Ki   = 0;
            Kip1 = -2 * k;
            Kip2 = k;
        }
        #endregion

        #region 4 производная
        /// <summary>
        /// Вычисление четвёртой производной
        /// на пятиточечном шаблоне
        /// для равномерной сетки
        /// </summary>
        /// <param name="h">Шаг сетки</param>
        private void CalcDerivativeOperator1D5P_D4(decimal h)
        {
            decimal k = 1 / (h * h * h * h);

            Kim2 = k;
            Kim1 = -4 * k;
            Ki   = 6;
            Kip1 = -4 * k;
            Kip2 = k;
        }
        #endregion
        
        #endregion

    }
}
