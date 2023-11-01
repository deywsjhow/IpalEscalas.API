using System.IO;

using System.Xml.Serialization;

namespace PortalIpalEscalas.Common.Models.Utils
{
    public class Serializer
    {
        public static string Serialize(object obj)
        {
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(sw, obj);
            return sw.ToString();
        }

        public static object Deserialize(string xml, System.Type type)
        {
            StringReader rd = new StringReader(xml);
            XmlSerializer serializer = new XmlSerializer(type);
            return serializer.Deserialize(rd);            
        }
    }
}
