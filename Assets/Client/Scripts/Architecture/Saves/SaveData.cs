using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Saves
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SaveData
    {

        [JsonProperty]
        public Dictionary<string,Dictionary<string,object>> Data { get; private set; }


        public SaveData()
        {
            Data = new Dictionary<string, Dictionary<string, object>>();
        }

        public SaveData(Dictionary<string, Dictionary<string, object>> data)
        {
            Data = new Dictionary<string, Dictionary<string, object>>(data);
        }

        public void SetObjectData(Dictionary<string, Dictionary<string, object>> obj)
        {
            Data = new Dictionary<string, Dictionary<string, object>>(obj);
        }
        public Dictionary<string, object> GetObjectData(string key)
        {
            if(Data.ContainsKey(key))
            {
                var data = Data[key];
                return data;
            }
           
            return null;
        }

    }
}
