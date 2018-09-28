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

        public static T Deserialize<T>(this XDocument serialized)
        {
            using (var reader = serialized.CreateReader())
            {
                var deserializer = new XmlSerializer(typeof(T));
                return (T)deserializer.Deserialize(reader);
            }
        }
    }
}
