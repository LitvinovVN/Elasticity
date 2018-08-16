using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using ElasticityClassLibrary.GeometryNamespase;

namespace ElasticityClassLibrary.Infrastructure
{
    /// <summary>
    /// Вспомогательный класс для работы с xml
    /// </summary>
    public static class Xml
    {
        public static bool ExportToXML(Type type, Object o, string path, string fileName)
        {
            string fullPath = Path.Combine(path, fileName);

            try
            {
                XmlSerializer formatter = new XmlSerializer(type);
                using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                {
                    formatter.Serialize(fs, o);
                }
            }
            catch (Exception exc)
            {
                return false;
            }

            return true;
        }

        public static object ImportFromXML(Type type, string path, string fileName)
        {
            object importedObject;
            string fullPath = Path.Combine(path, fileName);

            try
            {
                XmlSerializer formatter = new XmlSerializer(type);
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    importedObject = formatter.Deserialize(fs);
                }
            }
            catch (Exception exc)
            {
                return null;
            }

            return importedObject;
        }
    }
}
