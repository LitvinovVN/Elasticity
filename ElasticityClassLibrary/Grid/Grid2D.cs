using ElasticityClassLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Одномерная сетка
    /// </summary>
    [Serializable]
    public class Grid2D : Grid
    {
        #region Открытые свойства
        /// <summary>
        /// Ось координат №1 (по умолчанию - X)
        /// </summary>
        public AxisEnum AxisEnum1 { get; set; } = AxisEnum.X;

        /// <summary>
        /// Ось координат №2 (по умолчанию - Y)
        /// </summary>
        public AxisEnum AxisEnum2 { get; set; } = AxisEnum.Y;


        /// <summary>
        /// Размер расчетной области по оси №1
        /// </summary>
        public decimal GridSize1 { get; set; }

        /// <summary>
        /// Размер расчетной области по оси №1
        /// </summary>
        public decimal GridSize2 { get; set; }

        /// <summary>
        /// Хранилище переходов шагов сетки по оси №1
        /// </summary>
        public List<StepTransition> StepTransitions1 { get; set; } = new List<StepTransition>();

        /// <summary>
        /// Хранилище переходов шагов сетки по оси №2
        /// </summary>
        public List<StepTransition> StepTransitions2 { get; set; } = new List<StepTransition>();
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор. Создаёт одномерную сетку с заданным по осям числом узлов
        /// </summary>
        /// <param name="gridSize">Размер расчетной области</param>        
        /// <param name="numNodes">Количество узлов</param>        
        public Grid2D(decimal gridSize1, decimal gridSize2, uint numNodes1, uint numNodes2, AxisEnum axisEnum1 = AxisEnum.X, AxisEnum axisEnum2 = AxisEnum.Y)
        {
            AxisEnum1 = axisEnum1;
            AxisEnum2 = axisEnum2;
            GridSize1 = gridSize1;
            GridSize2 = gridSize2;

            StepTransition st1 = new StepTransition(numNodes1, gridSize1 / (numNodes1-1), gridSize1);
            StepTransitions1.Add(st1);

            StepTransition st2 = new StepTransition(numNodes2, gridSize2 / (numNodes2 - 1), gridSize2);
            StepTransitions1.Add(st2);
        }

        public Grid2D()
        {

        }
        #endregion

        

        public void CorrectIndexes()
        {
            StepTransition.CorrectIndexesInStepTransition(StepTransitions1);
            StepTransition.CorrectIndexesInStepTransition(StepTransitions2);
        }
        


        #region Вспомогательные методы
        /// <summary>
        /// Проверяет корректность сетки
        /// </summary>
        /// <returns></returns>
        public bool GridValidity()
        {
            bool IsCorrect = true;

            #region Габаритные размеры должны соответствовать расчитанным по правилам разбиения по осям 
            decimal GridSize1Calculated = StepTransition.StepTransitionListCalculateCoordinatesValidity(StepTransitions1);
            if (GridSize1Calculated != GridSize1) IsCorrect = false;

            decimal GridSize2Calculated = StepTransition.StepTransitionListCalculateCoordinatesValidity(StepTransitions2);
            if (GridSize2Calculated != GridSize2) IsCorrect = false;
            #endregion

            return IsCorrect;
        }

        
        #endregion
    }
}
