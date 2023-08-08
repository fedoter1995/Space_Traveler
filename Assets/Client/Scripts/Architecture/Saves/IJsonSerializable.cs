using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IJsonSerializable
{
    void SetObjectData(Dictionary<string, object> data);
    Dictionary<string, object> GetObjectData();
}

