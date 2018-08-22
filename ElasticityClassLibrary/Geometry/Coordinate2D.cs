using System;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Координата в двумерной системе
    /// </summary>
    [Serializable]
    public class Coordinate2D : Coordinate
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }        

        /// <summary>
        /// Строковое представление координаты
        /// </summary>
        public string GetStringCoordinates
        {
            get
            {
                return $"[{X},{Y}]"; 
            }
        }

        #region Конструкторы
        public Coordinate2D()
        {

        }

        public Coordinate2D(decimal x, decimal y)
        {
            X = x;
            Y = y;            
        }
        #endregion

        /// <summary>
        /// Фабрика объектов
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Coordinate2D Create(decimal x, decimal y)
        {
            return new Coordinate2D(x, y);
        }
    }
}