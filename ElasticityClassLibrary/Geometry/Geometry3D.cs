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
    public class Geometry3D : Geometry
    {
        
        public GridLayers3D GetGridLayers3D
        {
            get
            {
                GridLayers3D gridLayers3D = new GridLayers3D();

                foreach (var geometryElement in GeometryElements)
                {
                    var geometryElementSpecified=(GeometryElement3D)geometryElement;
                    gridLayers3D.Merge(geometryElementSpecified.GetGridLayers3D);
                }

                return gridLayers3D;
            }
        }

        public Geometry3D()
        {

        }               

        /// <summary>
        /// Сравнивает значения двух объектов Geometry
        /// </summary>
        /// <param name="g1">Первый объект</param>
        /// <param name="g2">Второй объект</param>
        /// <returns>true - значения объектов равны, 
        /// false - значения объектов не равны</returns>
        public static bool IsGeometryValuesEquals(Geometry3D g1, Geometry3D g2)
        {
            // Проверяем на равенство количество элементов
            int GeometryElementsCountG1 = g1.GeometryElements.Count;
            int GeometryElementsCountG2 = g2.GeometryElements.Count;
            if (GeometryElementsCountG1 != GeometryElementsCountG2) return false;

            // Перебирам объекты списков GeometryElements и сравниваем между собой
            for(int i = 0;i < GeometryElementsCountG1; i++)
            {
                var gEl1 = (GeometryElement3D)g1.GeometryElements[i];
                var gEl2 = (GeometryElement3D)g2.GeometryElements[i];

                // Проверяем на равенство координаты
                if (gEl1.CoordinateLocation3D.X != gEl2.CoordinateLocation3D.X) return false;
                if (gEl1.CoordinateLocation3D.Y != gEl2.CoordinateLocation3D.Y) return false;
                if (gEl1.CoordinateLocation3D.Z != gEl2.CoordinateLocation3D.Z) return false;

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
