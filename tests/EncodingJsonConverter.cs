using System;
using System.Text;
using Newtonsoft.Json;

namespace UtfUnknown.Tests
{
    public class EncodingJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Encoding).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Encoding)value).WebName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Encoding.GetEncoding((string)reader.Value);
        }
    }
}
