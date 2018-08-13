using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElasticityClassLibrary
{
    /// <summary>
    /// Сетка
    /// </summary>
    [Serializable]
    public class Grid
    {
        #region Габаритные размеры расчетной области
        /// <summary>
        /// Размер расчетной области по оси X
        /// </summary>
        public decimal GridSizeX { get; set; }
        /// <summary>
        /// Размер расчетной области по оси Y
        /// </summary>
        public decimal GridSizeY { get; set; }
        /// <summary>
        /// Размер расчетной области по оси Z
        /// </summary>
        public decimal GridSizeZ { get; set; }
        #endregion
        
        #region Правила переходов шагов сетки по осям
        /// <summary>
        /// Хранилище переходов шагов сетки по оси X
        /// </summary>
        public List<StepTransition> StepTransitionsX { get; set; } = new List<StepTransition>();
        /// <summary>
        /// Хранилище переходов шагов сетки по оси Y
        /// </summary>
        public List<StepTransition> StepTransitionsY { get; set; } = new List<StepTransition>();
        /// <summary>
        /// Хранилище переходов шагов сетки по оси Z
        /// </summary>
        public List<StepTransition> StepTransitionsZ { get; set; } = new List<StepTransition>();
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор. Создаёт сетку с заданным по осям числом узлов
        /// </summary>
        /// <param name="GridSizeX">Размер расчетной области по оси X</param>
        /// <param name="GridSizeY">Размер расчетной области по оси Y</param>
        /// <param name="GridSizeZ">Размер расчетной области по оси Z</param>
        /// <param name="NumX">Количество участков по оси X</param>
        /// <param name="NumY">Количество участков по оси Y</param>
        /// <param name="NumZ">Количество участков по оси Z</param>
        public Grid(decimal GridSizeX,
            decimal GridSizeY,
            decimal GridSizeZ,
            uint NumX,
            uint NumY,
            uint NumZ)
        {
            this.GridSizeX = GridSizeX;
            this.GridSizeY = GridSizeY;
            this.GridSizeZ = GridSizeZ;

            StepTransition stX = new StepTransition(NumX, GridSizeX / NumX, GridSizeX);
            StepTransitionsX.Add(stX);

            StepTransition stY = new StepTransition(NumY, GridSizeY / NumY, GridSizeY);
            StepTransitionsY.Add(stY);

            StepTransition stZ = new StepTransition(NumZ, GridSizeZ / NumZ, GridSizeZ);
            StepTransitionsZ.Add(stZ);
        }

        public Grid()
        {

        }
        #endregion

        #region Работа со слоями сетки
        /// <summary>
        /// Добавить слой сетки YZ в заданной координате X
        /// </summary>
        /// <param name="CoordinateX">Координата X вставки слоя</param>
        public StepTransition InsertLayerX(decimal coordinate)
        {
            /* Вставку слоя моделируем добавлением правила
             * перехода шагов сетки по оси X с корректировкой смежных шагов
             * ----------i----------j-------K---l----------
             * 
             * Графическая интерпретация
             *                                0                 1             2  3          - индексы StepTransitionsX
             *                                15                23            29 30         - StepTransitionsX[i].Index
             *                                5                 4             3  1          - StepTransitionsX[i].StepValue
             * 0 - - - - 5 - - - - 10 - - - - 15 - - - 19 - - - 23 - - 26 - - 29 30 ...     - индексы слоёв
             * - координаты слоёв
             *
             * Вставка
             */
                         
            StepTransition prevStepTransition = null;
            StepTransition curStepTransition  = null;
            StepTransition nextStepTransition = null;

            int? prevStepTransitionListIndex = null;
            int? nextStepTransitionListIndex = null;
            for (int i=0; i<=StepTransitionsX.Count; i++)
            {
                // Если переданная координата совпадает с существующим переходом,
                // новый переход не создаём, а возвращаем существующий
                if(StepTransitionsX[i].Coordinate == coordinate)
                {
                    return StepTransitionsX[i];
                }

                // Поиск первого перехода, координата которого
                // превышает переданное значение
                if(StepTransitionsX[i].Coordinate > coordinate)
                {
                    // Определяем индексы переходов слева и справа
                    nextStepTransitionListIndex = i;
                    nextStepTransition = StepTransitionsX[i];
                    if (i > 0)
                    {
                        prevStepTransitionListIndex = i - 1;
                        prevStepTransition = StepTransitionsX[i-1];
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
            if(prevStepTransitionListIndex==null)
            {
                //// Определяем существующие предыдущий и последующий слои около добавляемого слоя
                // Шаг сетки до правого перехода
                decimal oldStep = nextStepTransition.StepValue;

                // Если координата добавляемого узла меньше существующего шага сетки,
                // значит добавляемый узел будет первым после 0-го узла
                if(coordinate < oldStep)
                {
                    // Добавляем переходы в добавляемой координате
                    // и переход в существующем слое справа от добавляемого
                    // (если он уже есть, то не добавляем)
                    int position = 0;           // Позиция вставки в списке                    
                    var newItem  = new StepTransition(1, coordinate, coordinate);
                    var rightItem = new StepTransition(2, oldStep - coordinate, oldStep);
                    if (nextStepTransition.Coordinate == rightItem.Coordinate)
                    {
                        // Переход справа уже есть.
                        // Добавляем только новый.
                        StepTransitionsX.Insert(position, newItem);
                        // Исправляем шаг в существующем правом переходе
                        nextStepTransition.StepValue = nextStepTransition.Coordinate - newItem.Coordinate;
                    }
                    else
                    {
                        StepTransitionsX.InsertRange(position, new List<StepTransition> { newItem, rightItem });                        
                    }

                    // Корректируем индексы в списке переходов
                    CorrectIndexesInStepTransition(StepTransitionsX);

                    return StepTransitionsX[position];
                }
            }

            // Обработка 2 варианта: новый слой расположен между существующими переходами
            if (prevStepTransitionListIndex>=0)
            {
                // Определяем слои слева и справа от места вставки.
                // Возможны 4 варианта:
                // 1) добавляемый слой совпадает с существующим - ничего не делаем
                // 2) левый слой - переход
                // 3) левый и правый слои - не переходы
                // 4) правый слой - переход
                StepTransition prevLayer = new StepTransition();
                StepTransition nextLayer = new StepTransition();

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
                    prevLayer = prevStepTransition;
                    curStepTransition = new StepTransition(prevLayer.Index+1,
                        coordinate - prevLayer.Coordinate,
                        coordinate);
                    nextLayer = new StepTransition(curStepTransition.Index + 1,
                        prevLayer.Coordinate + nextStepTransition.StepValue - coordinate,
                        prevLayer.Coordinate + nextStepTransition.StepValue);
                    StepTransitionsX.InsertRange((int)nextStepTransitionListIndex,
                        new List<StepTransition> { curStepTransition, nextLayer });
                    IncreaseIndexesInStepTransition(StepTransitionsX, nextLayer.Index+1, 1);
                    return curStepTransition;
                }

                // 3) левый и правый слои - не переходы
                if(dLayers < ((nextStepTransition.Coordinate-prevStepTransition.Coordinate)/nextStepTransition.StepValue-1))
                {
                    prevLayer = new StepTransition(prevStepTransition.Index + dLayersUINT,
                        nextStepTransition.StepValue,
                        prevStepTransition.Coordinate + nextStepTransition.StepValue * dLayersUINT);
                    curStepTransition = new StepTransition(prevLayer.Index + 1,
                        coordinate - prevLayer.Coordinate,
                        coordinate);                    
                    nextLayer = new StepTransition(curStepTransition.Index + 1,
                        prevLayer.Coordinate + nextStepTransition.StepValue - coordinate,
                        prevLayer.Coordinate + nextStepTransition.StepValue);

                    StepTransitionsX.InsertRange((int)nextStepTransitionListIndex,
                        new List<StepTransition> { prevLayer, curStepTransition, nextLayer });
                    IncreaseIndexesInStepTransition(StepTransitionsX, nextLayer.Index + 1, 1);
                    return curStepTransition;
                }// 4) правый слой - переход
                else
                {
                    prevLayer = new StepTransition(prevStepTransition.Index + dLayersUINT,
                        nextStepTransition.StepValue,
                        prevStepTransition.Coordinate + nextStepTransition.StepValue * dLayersUINT);
                    curStepTransition = new StepTransition(prevLayer.Index + 1,
                        coordinate - prevLayer.Coordinate,
                        coordinate);
                    nextLayer = nextStepTransition;
                    nextLayer.Index++;
                    nextLayer.StepValue = nextLayer.Coordinate - curStepTransition.Coordinate;


                    StepTransitionsX.InsertRange((int)nextStepTransitionListIndex,
                        new List<StepTransition> { prevLayer, curStepTransition });
                    IncreaseIndexesInStepTransition(StepTransitionsX, nextLayer.Index + 1, 1);
                    return curStepTransition;
                }
            }

            return null;
        }

        /// <summary>
        /// Увеличивает индексы во всех элементах списка
        /// на указанное число, начиная с указанной позиции
        /// </summary>
        /// <param name="stepTransitionsX">Список</param>
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
            decimal sizeX = 0;
            foreach (var step in StepTransitionsX)
            {
                sizeX += step.Index * step.StepValue;
            }
            if (sizeX != GridSizeX) IsCorrect = false;

            decimal sizeY = 0;
            foreach (var step in StepTransitionsY)
            {
                sizeY += step.Index * step.StepValue;
            }
            if (sizeY != GridSizeY) IsCorrect = false;

            decimal sizeZ = 0;
            foreach (var step in StepTransitionsZ)
            {
                sizeZ += step.Index * step.StepValue;
            }
            if (sizeZ != GridSizeZ) IsCorrect = false;
            #endregion

            return IsCorrect;
        }
        #endregion
    }
}
