using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticityClassLibrary.Derivatives;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.Nodes;

namespace ElasticityClassLibrary.SolverNamespace
{
    /// <summary>
    /// Решатель МКР
    /// </summary>
    public static class Solver
    {
        /// <summary>
        /// Выполняет расчет
        /// </summary>
        /// <param name="GridPreCalc">Сетка с результатами предварительных расчетов</param>
        /// <returns>Объект SolverResult с результатами расчетов</returns>
        //public static SolverResult Calculate(GridWithGeometryPreCalculated GridPreCalc)
        //{
        //    SolverResult solverResult = new SolverResult();

        //    return solverResult;
        //}

        /// <summary>
        /// Выполняет расчет
        /// </summary>
        /// <param name="task">Объект SolverTask с описанием задачи</param>
        /// <returns>Объект SolverResult с результатами расчетов</returns>
        public static SolverResult Calculate(SolverTask task)
        { 
            SolverResult solverResult = new SolverResult();

            // Определяем тип задачи и вызываем соответствующий метод
            if(task is SolverTask1D)
            {
                solverResult = CalculateTask((SolverTask1D)task);
            }
            else if (task is SolverTask2D)
            {
                solverResult = CalculateTask((SolverTask2D)task);
            }
            else if (task is SolverTask3D)
            {
                solverResult = CalculateTask((SolverTask3D)task);
            }

            return solverResult;
        }

        /// <summary>
        /// Решатель одномерных задач
        /// </summary>
        /// <param name="task"></param>
        private static SolverResult CalculateTask(SolverTask1D task)
        {
            SolverResult solverResult = new SolverResult();

            foreach (var node in task.NodeSet1D.Nodes)
            {
                var currentNodeValue = node.NodeValue;
                // Игнорируем узлы с ненастроенным свойством NodeValue
                if (currentNodeValue == null) continue;
                
                // Если значение в узле необходимо рассчитать
                if(currentNodeValue is NodeValueRequired)
                {
                    if (node.DerivativeOperator == null) continue;


                    Equation equation = new Equation();

                    if (node.DerivativeOperator is DerivativeOperator1D3P)
                    {
                        var curDerivativeOperator = (DerivativeOperator1D3P)node.DerivativeOperator;
                        var curNodeNavigation = node.NodeNavigation as NodeNavigation1D;

                        var eqElement1 = new EquationElement();
                        eqElement1.Coefficient = curDerivativeOperator.Kim1;
                        eqElement1.Node = curNodeNavigation.NodeLeft;
                        equation.EquationElementList.Add(eqElement1);

                        var eqElement2 = new EquationElement();
                        eqElement2.Coefficient = curDerivativeOperator.Ki;
                        eqElement2.Node = node;
                        equation.EquationElementList.Add(eqElement2);

                        var eqElement3 = new EquationElement();
                        eqElement3.Coefficient = curDerivativeOperator.Kip1;
                        eqElement3.Node = curNodeNavigation.NodeRight;
                        equation.EquationElementList.Add(eqElement3);
                    }

                    

                    //equation.EquationElementList.Add(new EquationElement { });
                    equation.RightSideOfEquation = 0m;
                }
                

            }

            return solverResult;
        }

        /// <summary>
        /// Решатель двумерных задач
        /// </summary>
        /// <param name="task"></param>
        private static SolverResult CalculateTask(SolverTask2D task)
        {
            SolverResult solverResult = new SolverResult();

            return solverResult;
        }

        /// <summary>
        /// Решатель трёхмерных задач
        /// </summary>
        /// <param name="task"></param>
        private static SolverResult CalculateTask(SolverTask3D task)
        {
            SolverResult solverResult = new SolverResult();

            return solverResult;
        }
    }
}
