using System.Collections.Generic;


public interface IJsonSerializable
{
    void SetObjectData(Dictionary<string, object> data);
    Dictionary<string, object> GetObjectData();
}

