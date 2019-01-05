using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.Nodes;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Геометрия моделируемого объекта
    /// </summary>
    [Serializable]
    public class Geometry2D : Geometry
    {
        
        public GridLayers2D GetGridLayers2D
        {
            get
            {
                GridLayers2D gridLayers2D = new GridLayers2D();

                foreach (var geometryElement in GeometryElements)
                {
                    var geometryElementSpecified=(GeometryElement2D)geometryElement;
                    gridLayers2D.Merge(geometryElementSpecified.GetGridLayers2D);
                }

                return gridLayers2D;
            }
        }

        public Geometry2D()
        {

        }               

        /// <summary>
        /// Сравнивает значения двух объектов Geometry
        /// </summary>
        /// <param name="g1">Первый объект</param>
        /// <param name="g2">Второй объект</param>
        /// <returns>true - значения объектов равны, 
        /// false - значения объектов не равны</returns>
        public static bool IsGeometryValuesEquals(Geometry2D g1, Geometry2D g2)
        {
            // Проверяем на равенство количество элементов
            int GeometryElementsCountG1 = g1.GeometryElements.Count;
            int GeometryElementsCountG2 = g2.GeometryElements.Count;
            if (GeometryElementsCountG1 != GeometryElementsCountG2) return false;

            // Перебирам объекты списков GeometryElements и сравниваем между собой
            for(int i = 0;i < GeometryElementsCountG1; i++)
            {
                var gEl1 = (GeometryElement2D)g1.GeometryElements[i];
                var gEl2 = (GeometryElement2D)g2.GeometryElements[i];

                // Проверяем на равенство координаты
                if (gEl1.CoordinateLocation2D.X != gEl2.CoordinateLocation2D.X) return false;
                if (gEl1.CoordinateLocation2D.Y != gEl2.CoordinateLocation2D.Y) return false;                

                // Проверяем на равенство количество элементов GeometryPrimitives
                int gEl1GeometryPrimitivesCount = gEl1.GeometryPrimitives.Count;
                int gEl2GeometryPrimitivesCount = gEl2.GeometryPrimitives.Count;
                if (gEl1GeometryPrimitivesCount != gEl2GeometryPrimitivesCount) return false;

                // Перебираем примитивы
                for (int p = 0; p < gEl1GeometryPrimitivesCount; p++)
                {
                    var curPrimitive1 = gEl1.GeometryPrimitives[p];
                    var curPrimitive2 = gEl2.GeometryPrimitives[p];

                    // 
                }
            }

            return true;
        }



        public override NodeSet GetNodeSet(GridLayers gridLayers)
        {
            throw new NotImplementedException();
        }
    }
}
