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
    public class Geometry1D : Geometry
    {
        
        public GridLayers1D GetGridLayers1D
        {
            get
            {
                GridLayers1D gridLayers1D = new GridLayers1D();

                foreach (var geometryElement in GeometryElements)
                {
                    var geometryElementSpecified=(GeometryElement1D)geometryElement;
                    gridLayers1D.Merge(geometryElementSpecified.GetGridLayers1D);
                }

                return gridLayers1D;
            }
        }

        public override NodeSet GetNodeSet(GridLayers gridLayers)
        {            
            NodeSet1D nodeSet1D = new NodeSet1D();

            foreach (var geometryElement in GeometryElements)
            {
                nodeSet1D.Merge(geometryElement.GetNodeSet(gridLayers));
            }

            return nodeSet1D;
        }

        public Geometry1D()
        {

        }               

        /// <summary>
        /// Сравнивает значения двух объектов Geometry
        /// </summary>
        /// <param name="g1">Первый объект</param>
        /// <param name="g2">Второй объект</param>
        /// <returns>true - значения объектов равны, 
        /// false - значения объектов не равны</returns>
        public static bool IsGeometryValuesEquals(Geometry1D g1, Geometry1D g2)
        {
            // Проверяем на равенство количество элементов
            int GeometryElementsCountG1 = g1.GeometryElements.Count;
            int GeometryElementsCountG2 = g2.GeometryElements.Count;
            if (GeometryElementsCountG1 != GeometryElementsCountG2) return false;

            // Перебирам объекты списков GeometryElements и сравниваем между собой
            for(int i = 0;i < GeometryElementsCountG1; i++)
            {
                var gEl1 = (GeometryElement1D)g1.GeometryElements[i];
                var gEl2 = (GeometryElement1D)g2.GeometryElements[i];

                // Проверяем на равенство координаты
                if (gEl1.CoordinateLocation1D.X != gEl2.CoordinateLocation1D.X) return false;                

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

        
    }
}
