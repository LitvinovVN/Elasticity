using ElasticityClassLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Одномерная сетка
    /// </summary>
    [Serializable]
    public class Grid1D : Grid
    {
        #region Открытые свойства
        /// <summary>
        /// Ось координат (по умолчанию - X)
        /// </summary>
        public AxisEnum AxisEnum { get; set; } = AxisEnum.X;
                
        /// <summary>
        /// Размер расчетной области
        /// </summary>
        public decimal GridSize { get; set; } 
                        
        /// <summary>
        /// Хранилище переходов шагов сетки
        /// </summary>
        public List<StepTransition> StepTransitions { get; set; } = new List<StepTransition>();
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор. Создаёт одномерную сетку с заданным по осям числом узлов
        /// </summary>
        /// <param name="gridSize">Размер расчетной области</param>        
        /// <param name="numNodes">Количество узлов</param>        
        public Grid1D(decimal gridSize, uint numNodes, AxisEnum axisEnum = AxisEnum.X)
        {
            AxisEnum = axisEnum;
            GridSize = gridSize;            

            StepTransition st = new StepTransition(numNodes, gridSize / (numNodes-1), gridSize);
            StepTransitions.Add(st);            
        }

        public Grid1D()
        {

        }
        #endregion

        #region Работа со слоями сетки 
        
        public void CorrectIndexes()
        {
            StepTransition.CorrectIndexesInStepTransition(StepTransitions);            
        }
        #endregion


        #region Вспомогательные методы
        /// <summary>
        /// Проверяет корректность сетки
        /// </summary>
        /// <returns></returns>
        public bool GridValidity()
        {
            bool IsCorrect = true;

            #region Габаритные размеры должны соответствовать расчитанным по правилам разбиения по осям 
            decimal GridSizeXCalculated = StepTransition.StepTransitionListCalculateCoordinatesValidity(StepTransitions);
            if (GridSizeXCalculated != GridSize) IsCorrect = false;            
            #endregion

            return IsCorrect;
        }                
        #endregion
    }
}
