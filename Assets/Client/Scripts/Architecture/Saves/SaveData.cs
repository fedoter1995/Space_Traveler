using Newtonsoft.Json;
using System.Collections.Generic;

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

            foreach (KeyValuePair<string, Dictionary<string, object>> entry in obj)
            {
                if(Data.ContainsKey(entry.Key))
                {
                    Data[entry.Key] = entry.Value;
                }
                else
                {
                    Data.Add(entry.Key, entry.Value);
                }
            }
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
