using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.NagruzkaNamespace;
using ElasticityClassLibrary.Nodes;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Параллелепипед
    /// </summary>
    [Serializable]
    public class GeometryPrimitive3DParallelepiped : GeometryPrimitive3D
    {
        #region Конструкторы
        /// <summary>
        /// Создаёт параллелепипед с заданными параметрами
        /// </summary>
        /// <param name="coordinateInElement">Координата угла параллелепипеда,
        /// ближайшего к началу координат</param>
        /// <param name="lengthX">Длина по оси X</param>
        /// <param name="lengthY">Длина по оси Y</param>
        /// <param name="lengthZ">Длина по оси Z</param>
        /// <param name="isCavity">Флаг выреза / полости</param>
        /// <param name="numLayersX">Кол-во слоёв YZ по оси X</param>
        /// <param name="numLayersY">Кол-во слоёв XZ по оси Y</param>
        /// <param name="numLayersZ">Кол-во слоёв XY по оси Z</param>
        public GeometryPrimitive3DParallelepiped(Coordinate3D coordinateInElement,
            decimal lengthX,
            decimal lengthY,
            decimal lengthZ,
            bool isCavity=false,
            uint numLayersX=11,
            uint numLayersY=11,
            uint numLayersZ=11)
        {
            IsCavity = isCavity;
            CoordinateInElement = coordinateInElement;
            LengthX = lengthX;
            LengthY = lengthY;
            LengthZ = lengthZ;
            SetGridLayers3D(numLayersX, numLayersY, numLayersZ);
        }
        
        public GeometryPrimitive3DParallelepiped()
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
        public override Coordinate3D CoordinateInElement3D { get; set; }

        /// <summary>
        /// Список нагрузок, действующих на примитив
        /// </summary>
        public List<Nagruzka> NagruzkaList { get; set; } = new List<Nagruzka>();


        #region Геометрические размеры примитива
        /// <summary>
        /// Длина параллелепипеда по оси X
        /// </summary>
        public decimal LengthX { get; set; }

        /// <summary>
        /// Длина параллелепипеда по оси Y
        /// </summary>
        public decimal LengthY { get; set; }

        /// <summary>
        /// Длина параллелепипеда по оси Z
        /// </summary>
        public decimal LengthZ { get; set; }
        #endregion

        /// <summary>
        /// Объект со списками слоёв
        /// </summary>
        public GridLayers3D GridLayers3D { get; set; } = new GridLayers3D();

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public override GridLayers3D GetGridLayers3D
        {
            get
            {
                return GridLayers3D;
            }
        }

        [XmlIgnore]
        public override GeometryElement GeometryElement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Заполняет список слоёв объекта
        /// </summary>
        /// <param name="numLayersX">Количество слоёв по оси X</param>
        /// <param name="numLayersY">Количество слоёв по оси Y</param>
        /// <param name="numLayersZ">Количество слоёв по оси Z</param>
        private void SetGridLayers3D(uint numLayersX,
            uint numLayersY,
            uint numLayersZ)
        {
            SetGridLayerList(GridLayers3D.GridLayersX, numLayersX, CoordinateInElement3D.X, LengthX);
            SetGridLayerList(GridLayers3D.GridLayersY, numLayersY, CoordinateInElement3D.Y, LengthX);
            SetGridLayerList(GridLayers3D.GridLayersZ, numLayersZ, CoordinateInElement3D.Z, LengthX);
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

        public override NodeSet GetNodeSet(GridLayers gridLayers)
        {
            throw new NotImplementedException();
        }

        public override NodeLocationEnum IsCoordinateBelongsToGeometryPrimitive(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }
    }
}
