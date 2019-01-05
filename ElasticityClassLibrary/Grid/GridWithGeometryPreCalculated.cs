using ElasticityClassLibrary.GeometryNamespase;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.Nodes;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Сетка с учетом геометрии исследуемого объекта
    /// с предварительно рассчитанными параметрами слоёв
    /// (абстрактный класс)
    /// </summary>
    [XmlInclude(typeof(GridWithGeometryPreCalculated1D))]
    [XmlInclude(typeof(GridWithGeometryPreCalculated2D))]
    [XmlInclude(typeof(GridWithGeometryPreCalculated3D))]
    public abstract class GridWithGeometryPreCalculated
    {
        #region Импорт/экспорт
        /// <summary>
        /// Экспортирует объект в файл XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Флаг успешности сериализации</returns>
        public bool ExportToXML(string path, string fileName)
        {
            return Xml.ExportToXML(typeof(GridWithGeometryPreCalculated), this, path, fileName);
        }

        /// <summary>
        /// Импортирует объект из файла XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Импортированный объект GridWithGeometryPreCalculated.
        /// Null в случае неудачи.</returns>
        public static GridWithGeometryPreCalculated ImportFromXML(string path, string fileName)
        {
            GridWithGeometryPreCalculated importedGeometry = Xml.ImportFromXML(typeof(GridWithGeometryPreCalculated), path, fileName) as GridWithGeometryPreCalculated;
            return importedGeometry;
        }
        #endregion

        /// <summary>
        /// Возвращает набор узлов сетки, входящих в геометрию
        /// </summary>
        /// <param name="gridLayers"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public abstract NodeSet GenerateNodeSetFromGridLayersAndGeometry(GridLayers gridLayers, Geometry geometry);
    }
}