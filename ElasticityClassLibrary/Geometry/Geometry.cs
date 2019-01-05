using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.Nodes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Геометрия исследуемого объекта
    /// (абстрактный класс)
    /// </summary>
    [XmlInclude(typeof(Geometry1D))]
    [XmlInclude(typeof(Geometry2D))]
    [XmlInclude(typeof(Geometry3D))]
    public abstract class Geometry
    {
        /// <summary>
        /// Составные части моделируемого объекта 
        /// </summary>
        public List<GeometryElement> GeometryElements { get; set; } = new List<GeometryElement>();


        /// <summary>
        /// Экспортирует объект в файл XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Флаг успешности сериализации</returns>
        public bool ExportToXML(string path, string fileName)
        {
            return Xml.ExportToXML(typeof(Geometry), this, path, fileName);
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
            Geometry importedGeometry = Xml.ImportFromXML(typeof(Geometry), path, fileName) as Geometry;
            return importedGeometry;
        }

        /// <summary>
        /// Возвращает набор узлов геометрии,
        /// совпадающих с узлами сетки
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public abstract NodeSet GetNodeSet(GridLayers gridLayers);

        /// <summary>
        /// Добавляет элемент геометрии
        /// </summary>
        /// <param name="geometryElement"></param>
        public void AddGeometryElement(GeometryElement geometryElement)
        {
            GeometryElements.Add(geometryElement);
        }
    }
}