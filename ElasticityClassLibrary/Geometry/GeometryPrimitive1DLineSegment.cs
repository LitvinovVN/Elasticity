using System;
using System.Collections.Generic;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.NagruzkaNamespace;
using ElasticityClassLibrary.Nodes;

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
        /// Длина отрезка
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

        /// <summary>
        /// Возвращает набор узлов сетки, входящих в примитив
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public override NodeSet GetNodeSet(GridLayers gridLayers)
        {
            var nodeSet1D = new NodeSet1D();

            var gridLayers1D = (GridLayers1D)gridLayers;
            for (int i = 0; i < gridLayers1D.GridLayers.Count; i++)
            {
                var curGridLayer = gridLayers1D.GridLayers[i];
                decimal curCoordDecimal = curGridLayer.Coordinate;
                Coordinate curCoord = new Coordinate1D(curCoordDecimal);

                var nodeLocationEnum = IsCoordinateBelongsToGeometryPrimitive(curCoord);
                if (nodeLocationEnum != NodeLocationEnum.Outer)
                {
                    var addingNode = new Node1D();
                    addingNode.Coordinate = new Coordinate1D(curGridLayer.Coordinate);
                    addingNode.NodeLocationEnum = nodeLocationEnum;
                    //addingNode.
                    nodeSet1D.AddNode1D(addingNode);
                }
            }

            return nodeSet1D;
        }

        /// <summary>
        /// Определяет, принадлежит ли координата примитиву
        /// и возвращает соответствующий объект перечисления NodeLocationEnum
        /// </summary>
        /// <param name="curCoord"></param>
        /// <returns></returns>
        public override NodeLocationEnum IsCoordinateBelongsToGeometryPrimitive(Coordinate1D curCoord)
        {
            var geometryElement1D_CoordinateLocation1D = GeometryElement1D.CoordinateLocation1D;

            // Координата левой границы отрезка
            decimal leftBound = geometryElement1D_CoordinateLocation1D.X + CoordinateInElement1D.X;
            // Координата правой границы отрезка
            decimal rightBound = leftBound + Length;

            if (curCoord.X == leftBound || curCoord.X == rightBound)
            {
                return NodeLocationEnum.OnTheSurface;
            }

            if (curCoord.X >= leftBound && curCoord.X <= rightBound)
            {
                return NodeLocationEnum.Internal;
            }

            return NodeLocationEnum.Outer;
        }
    }
}
