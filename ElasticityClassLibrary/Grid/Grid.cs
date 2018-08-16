using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.GridNamespace
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
            return InsertLayer(AxisEnum.X, coordinate);
        }

        /// <summary>
        /// Добавить слой сетки XZ в заданной координате Y
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public StepTransition InsertLayerY(decimal coordinate)
        {
            return InsertLayer(AxisEnum.Y, coordinate);
        }

        /// <summary>
        /// Добавить слой сетки XY в заданной координате Z
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public StepTransition InsertLayerZ(decimal coordinate)
        {
            return InsertLayer(AxisEnum.Z, coordinate);
        }

        /// <summary>
        /// Добавить слой сетки YZ в заданной координате X
        /// </summary>
        /// <param name="CoordinateX">Координата X вставки слоя</param>
        public StepTransition InsertLayer(AxisEnum axis, decimal coordinate/*, int roundToNumSymbols=3*/)
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
            //coordinate = decimal.Round(coordinate, roundToNumSymbols);

            List<StepTransition> StepTransitionList;

            switch (axis)
            {
                case AxisEnum.X:
                    StepTransitionList = StepTransitionsX;
                    break;
                case AxisEnum.Y:
                    StepTransitionList = StepTransitionsY;
                    break;
                case AxisEnum.Z:
                    StepTransitionList = StepTransitionsZ;
                    break;
                default:
                    throw new NotSupportedException("Попытка добавления слоя в несуществующей системе координат");                    
            }

            StepTransition prevStepTransition = null;
            StepTransition curStepTransition = null;
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

            // Добавление слоя
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
                curStepTransition = new StepTransition(prevLayer.Index + 1,
                    coordinate - prevLayer.Coordinate,
                    coordinate);                
                nextLayer = new StepTransition(curStepTransition.Index + 1,
                    prevLayer.Coordinate + nextStepTransition.StepValue - coordinate,
                    prevLayer.Coordinate + nextStepTransition.StepValue);

                // Правый слой может оказаться переходом!
                if(nextLayer.Coordinate==nextStepTransition.Coordinate)
                {
                    StepTransitionList.Insert((int)nextStepTransitionListIndex,
                    curStepTransition);
                    nextStepTransition.Coordinate = nextLayer.Coordinate;
                    nextStepTransition.Index = nextLayer.Index;
                    nextStepTransition.StepValue = nextLayer.StepValue;

                    if (curStepTransition.Index==1)
                    {
                        CorrectIndexesInStepTransition(StepTransitionList);
                    }
                    else if(nextStepTransition.Index!=StepTransitionList[StepTransitionList.Count-1].Index)
                    {
                        IncreaseIndexesInStepTransition(StepTransitionList, nextLayer.Index, 1);
                    }
                    
                }
                else
                {
                    StepTransitionList.InsertRange((int)nextStepTransitionListIndex,
                    new List<StepTransition> { curStepTransition, nextLayer });
                    IncreaseIndexesInStepTransition(StepTransitionList, nextLayer.Index + 1, 1);
                }

                
                return curStepTransition;
            }

            // 3) левый и правый слои - не переходы
            if (dLayers < ((nextStepTransition.Coordinate - prevStepTransition.Coordinate) / nextStepTransition.StepValue - 1))
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

                StepTransitionList.InsertRange((int)nextStepTransitionListIndex,
                    new List<StepTransition> { prevLayer, curStepTransition, nextLayer });
                IncreaseIndexesInStepTransition(StepTransitionList, nextLayer.Index + 1, 1);
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


                StepTransitionList.InsertRange((int)nextStepTransitionListIndex,
                    new List<StepTransition> { prevLayer, curStepTransition });
                IncreaseIndexesInStepTransition(StepTransitionList, nextLayer.Index + 1, 1);
                return curStepTransition;
            }            
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

        public void CorrectIndexes()
        {
            CorrectIndexesInStepTransition(StepTransitionsX);
            CorrectIndexesInStepTransition(StepTransitionsY);
            CorrectIndexesInStepTransition(StepTransitionsZ);
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

            decimal GridSizeXCalculated = StepTransitionListCalculateCoordinatesValidity(StepTransitionsX);
            if (GridSizeXCalculated != GridSizeX) IsCorrect = false;

            decimal GridSizeYCalculated = StepTransitionListCalculateCoordinatesValidity(StepTransitionsY);
            if (GridSizeYCalculated != GridSizeY) IsCorrect = false;

            decimal GridSizeZCalculated = StepTransitionListCalculateCoordinatesValidity(StepTransitionsZ);
            if (GridSizeZCalculated != GridSizeZ) IsCorrect = false;
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
