﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.Geometry
{
    /// <summary>
    /// Параллелепипед
    /// </summary>
    [Serializable]
    public class GeometryPrimitiveCube : GeometryPrimitive
    {
        #region Конструкторы
        public GeometryPrimitiveCube(Coordinate3D coordinateInElement, decimal length, bool isCavity=false)
        {
            IsCavity = isCavity;
            CoordinateInElement = coordinateInElement;
            Length = length;
        }

        public GeometryPrimitiveCube()
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

        #region Геометрические размеры примитива
        /// <summary>
        /// Длина стороны куба
        /// </summary>
        public decimal Length { get; set; }
        #endregion               
    }
}
