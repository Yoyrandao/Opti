using Newtonsoft.Json;

namespace Utils.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object @object) => JsonConvert.SerializeObject(@object);

        public T Deserialize<T>(string objectString) => JsonConvert.DeserializeObject<T>(objectString);
    }
}