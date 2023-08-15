using Newtonsoft.Json;
using System.IO;

public class JsonSerealization<T> where T : class
{

    public static void Serialize(T data, string path)
    {
        JsonSerializer serializer = new JsonSerializer();
        using (StreamWriter sw = new StreamWriter(path + ".json"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, data, typeof(T));
        }

    }
    public static T Deserialize(string path)
    {

        JsonSerializer serializer = new JsonSerializer();
        if(File.Exists(path + ".json"))
        using (StreamReader sr = new StreamReader(path + ".json"))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            var obj = serializer.Deserialize<T>(reader);
            return obj;
        }
            return null;
    }
}
