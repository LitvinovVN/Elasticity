using System;
using System.Collections.Generic;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.NagruzkaNamespace;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Отрезок
    /// </summary>
    [Serializable]
    public class GeometryPrimitive1DLineSegment : GeometryPrimitive1D
    {
        

        #region Конструкторы
        /// <summary>
        /// Создаёт отрезок с заданными параметрами
        /// </summary>
        /// <param name="coordinateInElement">Координата стороны отрезка,
        /// ближайшей к началу координат</param>
        /// <param name="length">Длина отрезка, м</param>        
        /// <param name="numLayers">Кол-во слоёв</param>        
        public GeometryPrimitive1DLineSegment(Coordinate1D coordinateInElement,
            decimal length,            
            bool isCavity = false,
            uint numLayers=11)
        {
            IsCavity = isCavity;
            CoordinateInElement = coordinateInElement;
            Length = length;            
            SetGridLayers1D(numLayers);
        }
        
        public GeometryPrimitive1DLineSegment()
        {
                
        }
        #endregion

        /// <summary>
        /// Полость / вырез
        /// </summary>
        public override bool IsCavity { get; set; }

        /// <summary>
        /// Координата расположения примитива внутри геометрии,
        /// т.е. относительно координаты расположения элемента
        /// </summary>
        public override Coordinate1D CoordinateInElement1D { get; set; }
        
        /// <summary>
        /// Список нагрузок, действующих на примитив
        /// </summary>
        public List<Nagruzka> NagruzkaList { get; set; } = new List<Nagruzka>();


        #region Геометрические размеры примитива
        /// <summary>
        /// Длина параллелепипеда
        /// </summary>
        public decimal Length { get; set; }                
        #endregion

        /// <summary>
        /// Объект со списками слоёв
        /// </summary>
        public GridLayers1D GridLayers1D { get; set; } = new GridLayers1D();

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public override GridLayers1D GetGridLayers1D
        {
            get
            {
                return GridLayers1D;
            }
        }

        /// <summary>
        /// Заполняет список слоёв объекта
        /// </summary>
        /// <param name="numLayers">Количество слоёв по оси X</param>        
        private void SetGridLayers1D(uint numLayers)
        {
            SetGridLayerList(GridLayers1D.GridLayers, numLayers, CoordinateInElement1D.X,Length);            
        }

        /// <summary>
        /// Вычисляет и заполняет список слоёв
        /// по одной оси координат
        /// </summary>
        /// <param name="gridLayerList"></param>
        /// <param name="numLayers"></param>
        /// <param name="coordinate"></param>
        /// <param name="length"></param>
        private void SetGridLayerList(List<GridLayer> gridLayerList,
            uint numLayers,
            decimal coordinate,
            decimal length)
        {            
            decimal step = length / (numLayers - 1);
            for (uint index = 0; index < numLayers; index++)
            {
                GridLayer gridLayer = new GridLayer();
                gridLayer.Index = index;
                gridLayer.Coordinate = coordinate + index * step;
                gridLayerList.Add(gridLayer);
            }
        }
    }
}
