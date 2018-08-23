using System;
using System.Collections.Generic;
using System.Text;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Параллелепипед
    /// </summary>
    [Serializable]
    public class GeometryPrimitiveCube : GeometryPrimitiveParallelepiped
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
        public override Coordinate3D CoordinateInElement3D { get; set; }

        #region Геометрические размеры примитива
        /// <summary>
        /// Длина стороны куба
        /// </summary>
        public decimal Length { get; set; }
        #endregion

        /// <summary>
        /// Возвращает объект со списками слоёв
        /// </summary>
        public override GridLayers3D GetGridLayers3D => null;
    }
}
