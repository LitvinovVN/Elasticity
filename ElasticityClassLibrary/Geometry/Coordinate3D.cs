using System;

namespace ElasticityClassLibrary.Geometry
{
    /// <summary>
    /// Координата в трёхмерной системе
    /// </summary>
    [Serializable]
    public class Coordinate3D
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }

        #region Конструкторы
        public Coordinate3D()
        {

        }

        public Coordinate3D(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion

        /// <summary>
        /// Фабрика объектов
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Coordinate3D Create(decimal x, decimal y, decimal z)
        {
            return new Coordinate3D(x, y, z);
        }
    }
}