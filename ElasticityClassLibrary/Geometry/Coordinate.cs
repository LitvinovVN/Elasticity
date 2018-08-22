using System.Xml.Serialization;

namespace ElasticityClassLibrary.GeometryNamespase
{
    /// <summary>
    /// Координата (абстрактный класс)
    /// </summary>
    [XmlInclude(typeof(Coordinate1D))]
    [XmlInclude(typeof(Coordinate2D))]
    [XmlInclude(typeof(Coordinate3D))]
    public abstract class Coordinate
    {
    }
}