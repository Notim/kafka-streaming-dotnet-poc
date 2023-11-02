using System.Text.Json;
using Confluent.Kafka;

namespace Freemarket.Ordering.Infrastructure.Kafka;

public class JsonKakfaMessage<T> : ISerializer<T>, IDeserializer<T>
{

    public byte[] Serialize(T data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data.ToArray())!;
    }

}