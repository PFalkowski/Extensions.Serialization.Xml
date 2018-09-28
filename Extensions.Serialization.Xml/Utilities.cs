using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Extensions.Serialization.Xml
{
    public static partial class Utilities
    {
        public static XDocument SerializeToXDoc<T>(this T source)
        {
            var result = new XDocument();
            using (var writer = result.CreateWriter())
            {
                var serializer = new XmlSerializer(source.GetType());
                serializer.Serialize(writer, source);
            }
            return result;
        }

        public static XmlDocument SerializeToXmlDoc<T>(this T source)
            where T : new()
        {
            var result = new XmlDocument();
            using (var ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(source.GetType());
                serializer.Serialize(ms, source);
                ms.Flush();
                ms.Position = 0;
                result.Load(ms);
            }
            return result;
        }

        public static T Deserialize<T>(this XDocument serialized)
        {
            using (var reader = serialized.CreateReader())
            {
                var deserializer = new XmlSerializer(typeof(T));
                return (T)deserializer.Deserialize(reader);
            }
        }

        public static T Deserialize<T>(this XmlDocument serialized)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var xmlStream = new MemoryStream())
            {
                serialized.Save(xmlStream);
                xmlStream.Flush();
                xmlStream.Position = 0;
                using (TextReader reader = new StreamReader(xmlStream))
                {
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
        }
    }
}
