using ElasticityClassLibrary.Infrastructure;
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
    public class Grid3D : Grid
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
        public Grid3D(decimal GridSizeX,
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

        public Grid3D()
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
            return StepTransition.InsertNode(coordinate, StepTransitionsX);
        }

        /// <summary>
        /// Добавить слой сетки XZ в заданной координате Y
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public StepTransition InsertLayerY(decimal coordinate)
        {
            return StepTransition.InsertNode(coordinate, StepTransitionsY);
        }

        /// <summary>
        /// Добавить слой сетки XY в заданной координате Z
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public StepTransition InsertLayerZ(decimal coordinate)
        {
            return StepTransition.InsertNode(coordinate, StepTransitionsZ);
        }

               
        

        public void CorrectIndexes()
        {
            StepTransition.CorrectIndexesInStepTransition(StepTransitionsX);
            StepTransition.CorrectIndexesInStepTransition(StepTransitionsY);
            StepTransition.CorrectIndexesInStepTransition(StepTransitionsZ);
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

            decimal GridSizeXCalculated = StepTransition.StepTransitionListCalculateCoordinatesValidity(StepTransitionsX);
            if (GridSizeXCalculated != GridSizeX) IsCorrect = false;

            decimal GridSizeYCalculated = StepTransition.StepTransitionListCalculateCoordinatesValidity(StepTransitionsY);
            if (GridSizeYCalculated != GridSizeY) IsCorrect = false;

            decimal GridSizeZCalculated = StepTransition.StepTransitionListCalculateCoordinatesValidity(StepTransitionsZ);
            if (GridSizeZCalculated != GridSizeZ) IsCorrect = false;
            #endregion

            return IsCorrect;
        }
        
        #endregion
    }
}
