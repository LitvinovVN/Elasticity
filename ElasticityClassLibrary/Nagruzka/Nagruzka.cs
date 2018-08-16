using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.NagruzkaNamespace
{
    /// <summary>
    /// Нагрузка (абстрактный класс)
    /// </summary>
    [XmlInclude(typeof(NagruzkaRaspredRavnomern))]
    public abstract class Nagruzka
    {
        
    }
}
