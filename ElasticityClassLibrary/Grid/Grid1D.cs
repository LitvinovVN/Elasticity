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
        /// <summary>
        /// Добавить слой сетки в заданной координате
        /// </summary>        
        /// <param name="coordinate">Координата вставки</param>
        /// <returns>Объект переходов StepTransition</returns>
        public StepTransition InsertNode(decimal coordinate)
        {
            /* Вставку слоя моделируем добавлением правила
             * перехода шагов сетки по оси с корректировкой смежных шагов
             * ----------i----------j-------K---l----------
             * 
             * Графическая интерпретация
             *                                0                 1             2  3          - индексы StepTransitions
             *                                15                23            29 30         - StepTransitions[i].Index
             *                                5                 4             3  1          - StepTransitions[i].StepValue
             * 0 - - - - 5 - - - - 10 - - - - 15 - - - 19 - - - 23 - - 26 - - 29 30 ...     - индексы слоёв
             * - координаты слоёв
             *
             * Вставка
             */

            List<StepTransition> StepTransitionList = StepTransitions;                       

            StepTransition prevStepTransition = null;
            StepTransition curStepTransition  = null;
            StepTransition nextStepTransition = null;

            int? prevStepTransitionListIndex = null;
            int? nextStepTransitionListIndex = null;
            for (int i = 0; i <= StepTransitionList.Count; i++)
            {
                // Если переданная координата совпадает с существующим переходом,
                // новый переход не создаём, а возвращаем существующий
                if (StepTransitionList[i].Coordinate == coordinate)
                {
                    return StepTransitionList[i];
                }

                // Поиск первого перехода, координата которого
                // превышает переданное значение
                if (StepTransitionList[i].Coordinate > coordinate)
                {
                    // Определяем индексы переходов слева и справа
                    nextStepTransitionListIndex = i;
                    nextStepTransition = StepTransitionList[i];
                    if (i > 0)
                    {
                        prevStepTransitionListIndex = i - 1;
                        prevStepTransition = StepTransitionList[i - 1];
                    }
                    break;
                }
            }

            // Возможны три варианта расположения нового слоя:
            // 1 - до первого перехода:              предыдущий   = null,                   следующий = 0
            // 2 - между существующими переходами:   предыдущий > = 0,                      следующий < StepTransitions.Count
            // 3 - за границей расчетной области:    предыдущий   = StepTransitions.Count,  следующий = null
            // Если координата превышает габаритные размеры расчетной области, возвращаем null

            // Обработка 3 варианта: новый слой расположен за пределами расчетной области,
            // возвращаем null
            if (nextStepTransitionListIndex == null) return null;

            // Обработка 1 варианта: новый слой расположен до первого перехода
            if (prevStepTransitionListIndex == null)
            {
                prevStepTransition = new StepTransition(0, 0, 0);
            }

            // Обработка 2 варианта: новый слой расположен между существующими переходами            

            // Добавление узла
            // Определяем узлы слева и справа от места вставки.
            // Возможны 4 варианта:
            // 1) добавляемый узел совпадает с существующим - ничего не делаем
            // 2) левый узел - переход
            // 3) левый и правый узлы - не переходы
            // 4) правый узел - переход
            StepTransition prevNodeAsStepTransition = new StepTransition();
            StepTransition nextNodeAsStepTransition = new StepTransition();

            // Расстояние от левого перехода до координаты добавляемого слоя
            decimal d = coordinate - prevStepTransition.Coordinate;
            // Количество слоёв от левого перехода до координаты
            decimal dLayers = d / nextStepTransition.StepValue;
            uint dLayersUINT = (uint)Decimal.Truncate(dLayers);
            // Детектор совпадения координаты добавляемого слоя с имеющимися
            decimal sovpadenie = d % nextStepTransition.StepValue;

            // 1 вариант. Добавляемый слой совпадает с существующим - ничего не делаем
            if (sovpadenie == 0m) return null;

            // 2 вариант. Левый слой - переход
            // - добавляем текущий переход по переданной координате
            if (dLayers < 1)
            {
                prevNodeAsStepTransition = prevStepTransition;
                curStepTransition = new StepTransition(prevNodeAsStepTransition.Index + 1,
                    coordinate - prevNodeAsStepTransition.Coordinate,
                    coordinate);                
                nextNodeAsStepTransition = new StepTransition(curStepTransition.Index + 1,
                    prevNodeAsStepTransition.Coordinate + nextStepTransition.StepValue - coordinate,
                    prevNodeAsStepTransition.Coordinate + nextStepTransition.StepValue);

                // Правый слой может оказаться переходом!
                if(nextNodeAsStepTransition.Coordinate==nextStepTransition.Coordinate)
                {
                    StepTransitionList.Insert((int)nextStepTransitionListIndex,
                    curStepTransition);
                    nextStepTransition.Coordinate = nextNodeAsStepTransition.Coordinate;
                    nextStepTransition.Index = nextNodeAsStepTransition.Index;
                    nextStepTransition.StepValue = nextNodeAsStepTransition.StepValue;

                    if (curStepTransition.Index==1)
                    {
                        CorrectIndexesInStepTransition(StepTransitionList);
                    }
                    else if(nextStepTransition.Index!=StepTransitionList[StepTransitionList.Count-1].Index)
                    {
                        IncreaseIndexesInStepTransition(StepTransitionList, nextNodeAsStepTransition.Index, 1);
                    }
                    
                }
                else
                {
                    StepTransitionList.InsertRange((int)nextStepTransitionListIndex,
                    new List<StepTransition> { curStepTransition, nextNodeAsStepTransition });
                    IncreaseIndexesInStepTransition(StepTransitionList, nextNodeAsStepTransition.Index + 1, 1);
                }

                
                return curStepTransition;
            }

            // 3) левый и правый слои - не переходы
            if (dLayers < ((nextStepTransition.Coordinate - prevStepTransition.Coordinate) / nextStepTransition.StepValue - 1))
            {
                prevNodeAsStepTransition = new StepTransition(prevStepTransition.Index + dLayersUINT,
                    nextStepTransition.StepValue,
                    prevStepTransition.Coordinate + nextStepTransition.StepValue * dLayersUINT);
                curStepTransition = new StepTransition(prevNodeAsStepTransition.Index + 1,
                    coordinate - prevNodeAsStepTransition.Coordinate,
                    coordinate);
                nextNodeAsStepTransition = new StepTransition(curStepTransition.Index + 1,
                    prevNodeAsStepTransition.Coordinate + nextStepTransition.StepValue - coordinate,
                    prevNodeAsStepTransition.Coordinate + nextStepTransition.StepValue);

                StepTransitionList.InsertRange((int)nextStepTransitionListIndex,
                    new List<StepTransition> { prevNodeAsStepTransition, curStepTransition, nextNodeAsStepTransition });
                IncreaseIndexesInStepTransition(StepTransitionList, nextNodeAsStepTransition.Index + 1, 1);
                return curStepTransition;
            }// 4) правый слой - переход
            else
            {
                prevNodeAsStepTransition = new StepTransition(prevStepTransition.Index + dLayersUINT,
                    nextStepTransition.StepValue,
                    prevStepTransition.Coordinate + nextStepTransition.StepValue * dLayersUINT);
                curStepTransition = new StepTransition(prevNodeAsStepTransition.Index + 1,
                    coordinate - prevNodeAsStepTransition.Coordinate,
                    coordinate);
                nextNodeAsStepTransition = nextStepTransition;
                nextNodeAsStepTransition.Index++;
                nextNodeAsStepTransition.StepValue = nextNodeAsStepTransition.Coordinate - curStepTransition.Coordinate;


                StepTransitionList.InsertRange((int)nextStepTransitionListIndex,
                    new List<StepTransition> { prevNodeAsStepTransition, curStepTransition });
                IncreaseIndexesInStepTransition(StepTransitionList, nextNodeAsStepTransition.Index + 1, 1);
                return curStepTransition;
            }            
        }

        /// <summary>
        /// Увеличивает индексы во всех элементах списка
        /// на указанное число, начиная с указанной позиции
        /// </summary>
        /// <param name="stepTransitions">Список переходов</param>
        /// <param name="stepTransitionIndexStart">Индекс на оси, с которого нужно увеличивать</param>
        /// <param name="value">Значение, на которое нужно увеличить индекс</param>
        private void IncreaseIndexesInStepTransition(List<StepTransition> stepTransitionList, uint stepTransitionIndexStart, uint value)
        {
            for (int i = stepTransitionList.Count-1; i >= 0; i--)
            {
                if (stepTransitionList[i].Index < stepTransitionIndexStart) return;
                stepTransitionList[i].Index += value;
            }
        }

        /// <summary>
        /// Перерасчет индексов в списке переходов
        /// </summary>
        /// <param name="stepTransitionList"></param>
        private void CorrectIndexesInStepTransition(List<StepTransition> stepTransitionList)
        {
            uint index = 0;
            for (int i = 0; i < stepTransitionList.Count; i++)
            {
                if(i==0)
                {
                    index += (uint)(stepTransitionList[i].Coordinate / stepTransitionList[i].StepValue);
                }
                else
                {
                    index += (uint)((stepTransitionList[i].Coordinate - stepTransitionList[i - 1].Coordinate) / stepTransitionList[i].StepValue);
                }
                
                stepTransitionList[i].Index = index;
            }
        }

        public void CorrectIndexes()
        {
            CorrectIndexesInStepTransition(StepTransitions);            
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
            decimal GridSizeXCalculated = StepTransitionListCalculateCoordinatesValidity(StepTransitions);
            if (GridSizeXCalculated != GridSize) IsCorrect = false;            
            #endregion

            return IsCorrect;
        }

        /// <summary>
        /// Вычисляет размер по списку переходов
        /// </summary>
        /// <param name="StepTransitionList"></param>
        /// <returns></returns>
        private decimal StepTransitionListCalculateCoordinatesValidity(List<StepTransition> StepTransitionList)
        {
            StepTransition prevStep = null;
            decimal size = 0;
            foreach (var step in StepTransitionList)
            {
                if (prevStep != null)
                {
                    size += (step.Index - prevStep.Index) * step.StepValue;
                }
                else
                {
                    size += step.Index * step.StepValue;
                }

                prevStep = step;
            }
            return size;
        }
        #endregion
    }
}
