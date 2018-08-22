using System;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Координата в двумерной системе
    /// </summary>
    [Serializable]
    public class Coordinate1D : Coordinate
    {
        public decimal X { get; set; }        

        /// <summary>
        /// Строковое представление координаты
        /// </summary>
        public string GetStringCoordinates
        {
            get
            {
                return $"[{X}]"; 
            }
        }

        #region Конструкторы
        public Coordinate1D()
        {

        }

        public Coordinate1D(decimal x)
        {
            X = x;
        }
        #endregion

        /// <summary>
        /// Фабрика объектов
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Coordinate1D Create(decimal x)
        {
            return new Coordinate1D(x);
        }
    }
}