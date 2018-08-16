using ElasticityClassLibrary.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.NagruzkaNamespace
{
    public class AreaRectangle : Area
    {
        /// <summary>
        /// Координата угла прямоугольника, ближайшего к оси координат
        /// </summary>
        public Coordinate3D Coordinate3DPointMinDistant;

        /// <summary>
        /// Координата угла прямоугольника, максимально удалённого от оси координат
        /// </summary>
        public Coordinate3D Coordinate3DPointMaxDistant;
    }
}
