using System.Xml.Serialization;

namespace ElasticityClassLibrary.NagruzkaNamespace
{
    /// <summary>
    /// Область действия нагрузки
    /// </summary>
    [XmlInclude(typeof(AreaRectangle))]
    public abstract class Area
    {
    }
}