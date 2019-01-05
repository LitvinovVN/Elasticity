using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SparseMatrixRepSeqNamespace
{
    /// <summary>
    /// Метод конечных элементов для решения одномерных задач
    /// </summary>
    public class FiniteDifferenceMethod1D
    {
        // FunctionValueLeft                              FunctionValueRight
        // -----|------------------------------------------------|-------> AxisX
        // AxisXValueLeft                                    AxisXValueRight

        #region Закрытые поля
        /// <summary>
        /// Путь к папке с проектом
        /// </summary>
        readonly string _pathToProjectDir = "";

        /// <summary>
        /// Число узлов
        /// </summary>
        int _numNodes;

        /// <summary>
        /// Значение диапазона слева
        /// </summary>
        double _axisXValueLeft = 0;
        /// <summary>
        /// Значение диапазона справа
        /// </summary>
        double _axisXValueRight = 1;


        /// <summary>
        /// Значение функции слева (граничное условие)
        /// </summary>
        double _functionValueLeft = 0;
        /// <summary>
        /// Значение функции справа (граничное условие)
        /// </summary>
        double _functionValueRight = 1;

        /// <summary>
        /// Дифференциальный оператор
        /// </summary>
        double[] diffOperator;
        #endregion

        #region Открытые свойства
        /// <summary>
        /// Шаг равномерной сетки)
        /// </summary>
        public double GetH
        {
            get
            {
                return (_axisXValueRight - _axisXValueLeft) / (_numNodes - 1);
            }
        }
        #endregion

        #region Конструкторы
        public FiniteDifferenceMethod1D(string pathToProjectDir, int numNodes = 11)
        {
            _pathToProjectDir = pathToProjectDir;
            _numNodes = numNodes;
        }
        #endregion



        /// <summary>
        /// Вычисление
        /// </summary>
        public async Task Calculate()
        {            
            using (SLAE slae = new SLAE(_pathToProjectDir))
            {
                await slae.ClearAsync();

                // Граничные условия
                await slae.SparseMatrixRepSeq.AddRowAsync(new double[] { 1 });

                double[] secondRow = new double[_numNodes];
                secondRow[secondRow.Length - 1] = 1;
                await slae.SparseMatrixRepSeq.AddRowAsync(secondRow);
                
                await slae.SparseMatrixRepSeq.PrintMatrix();
                Console.WriteLine("-------------------\n");
                
                // Внутренние узлы
                var tuple0 = await slae.SparseMatrixRepSeq.AddRowsSequenceBandAsync(diffOperator,
                    2, 0, _numNodes - 2);
                await slae.SparseMatrixRepSeq.PrintMatrix();
                Console.WriteLine("-------------------\n");                

                try
                {
                    double[] rSide = new double[_numNodes];

                    rSide[0] = _functionValueLeft;
                    rSide[1] = _functionValueRight;

                    for (int i = 2; i < rSide.Length; i++)
                    {
                        rSide[i] = 6 * ((i-1) * GetH);
                    }
                    

                    await slae.SetRightSideAsync(rSide);
                    await slae.PrintRightSide();

                    try
                    {
                        long elapsedTime = await slae.SolveCuda();
                        Console.WriteLine($"Время решения СЛАУ: {elapsedTime} мс");
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }

                    await slae.PrintResult();

                    await slae.PrintCheckResult();
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }

            }
        }

        /// <summary>
        /// Возвращает коэфффициенты дифференциального оператора
        /// для производной второго порядка
        /// </summary>
        /// <returns></returns>
        public double[] D2Udx2()
        {
            double h2 = GetH * GetH;

            double[] res = new double[] { 1, -2, 1 };
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = res[i] / h2;
            }

            return res;
        }

        /// <summary>
        /// Установка дифференциального оператора
        /// </summary>
        /// <param name="diffEquation"></param>
        public void SetDiffOperator(double[] diffEquation)
        {
            diffOperator = diffEquation;
        }
    }
}
