using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml;
using System.Xml.Serialization;
using Confluent.Kafka;

namespace Freemarket.Ordering.Infrastructure.Kafka;

public class XmlKakfaMessage<T> : ISerializer<T>, IDeserializer<T> where T : new()
{

    public byte[] Serialize(T data, SerializationContext context)
    {
        var xsSubmit = new XmlSerializer(typeof(T));
        using (var sww = new StringWriter()) {
            using (var writer = new XmlTextWriter(sww)) {
                writer.Formatting = Formatting.None;
                xsSubmit.Serialize(writer, data);
                return Encoding.UTF8.GetBytes(sww.ToString());
            }
        }
    }

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        var serializer = new XmlSerializer(typeof(T));

        using var reader = new StringReader(Encoding.UTF8.GetString(data.ToArray()));

        return (T) serializer.Deserialize(reader)!;
    }

}