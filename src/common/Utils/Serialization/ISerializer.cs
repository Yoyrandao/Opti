namespace Utils.Serialization
{
    public interface ISerializer
    {
        string Serialize(object @object);

        T Deserialize<T>(string objectString);
    }
}