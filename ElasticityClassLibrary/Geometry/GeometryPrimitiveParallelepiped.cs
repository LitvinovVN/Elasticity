using System;
using System.Collections.Generic;
using ElasticityClassLibrary.NagruzkaNamespace;

namespace ElasticityClassLibrary.Geometry
{
    /// <summary>
    /// Параллелепипед
    /// </summary>
    [Serializable]
    public class GeometryPrimitiveParallelepiped : GeometryPrimitive
    {
        #region Конструкторы
        public GeometryPrimitiveParallelepiped(Coordinate3D coordinateInElement, decimal lengthX, decimal lengthY, decimal lengthZ, bool isCavity=false)
        {
            IsCavity = isCavity;
            CoordinateInElement = coordinateInElement;
            LengthX = lengthX;
            LengthY = lengthY;
            LengthZ = lengthZ;
        }

        public GeometryPrimitiveParallelepiped()
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
        public override Coordinate3D CoordinateInElement { get; set; }

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

        #region ?????????????????????????????????????Количество слоёв примитива??????????????
        /// <summary>
        /// Количество слоёв YZ сетки,
        /// рассекающих примитив по оси X
        /// </summary>
        public uint NumLayersX { get; set; }

        /// <summary>
        /// Количество слоёв XZ сетки,
        /// рассекающих примитив по оси Y
        /// </summary>
        public uint NumLayersY { get; set; }

        /// <summary>
        /// Количество слоёв XY сетки,
        /// рассекающих примитив по оси Z
        /// </summary>
        public uint NumLayersZ { get; set; }
        #endregion
    }
}
