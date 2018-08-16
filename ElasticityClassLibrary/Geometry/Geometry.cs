using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.Geometry
{
    /// <summary>
    /// Геометрия моделируемого объекта
    /// </summary>
    [Serializable]
    public class Geometry
    {
        /// <summary>
        /// Составные части моделируемого объекта 
        /// </summary>
        public List<GeometryElement> GeometryElements { get; set; } = new List<GeometryElement>();

        public Geometry()
        {

        }

        /// <summary>
        /// Экспортирует объект в файл XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Флаг успешности сериализации</returns>
        public bool ExportToXML(string path, string fileName)
        {            
            string fullPath = Path.Combine(path, fileName);

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Geometry));
                using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                {
                    formatter.Serialize(fs, this);
                }
            }
            catch(Exception exc)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Импортирует объект из файла XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Импортированный объект Geometry.
        /// Null в случае неудачи.</returns>
        public static Geometry ImportFromXML(string path, string fileName)
        {
            Geometry importedGeometry;
            string fullPath = Path.Combine(path, fileName);

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Geometry));
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    importedGeometry = (Geometry)formatter.Deserialize(fs);
                }
            }
            catch (Exception exc)
            {
                return null;
            }

            return importedGeometry;
        }

        /// <summary>
        /// Сравнивает значения двух объектов Geometry
        /// </summary>
        /// <param name="g1">Первый объект</param>
        /// <param name="g2">Второй объект</param>
        /// <returns>true - значения объектов равны, 
        /// false - значения объектов не равны</returns>
        public static bool IsGeometryValuesEquals(Geometry g1, Geometry g2)
        {
            // Проверяем на равенство количество элементов
            int GeometryElementsCountG1 = g1.GeometryElements.Count;
            int GeometryElementsCountG2 = g2.GeometryElements.Count;
            if (GeometryElementsCountG1 != GeometryElementsCountG2) return false;

            // Перебирам объекты списков GeometryElements и сравниваем между собой
            for(int i = 0;i < GeometryElementsCountG1; i++)
            {
                var gEl1 = g1.GeometryElements[i];
                var gEl2 = g2.GeometryElements[i];

                // Проверяем на равенство координаты
                if (gEl1.CoordinateLocation.X != gEl2.CoordinateLocation.X) return false;
                if (gEl1.CoordinateLocation.Y != gEl2.CoordinateLocation.Y) return false;
                if (gEl1.CoordinateLocation.Z != gEl2.CoordinateLocation.Z) return false;

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
