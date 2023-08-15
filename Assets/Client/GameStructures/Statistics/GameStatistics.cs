using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpaceTraveler.GameStructures.Meteors;
using System.Collections.Generic;

public class GameStatistics : IJsonSerializable
{
    [JsonProperty]
    public int Points { get; private set; } = 0;
    [JsonProperty]
    public Dictionary<AsteroidType, int> DestroyedAsteroidsNumb { get; private set; } = new Dictionary<AsteroidType, int>();



    public void OnDestroyAsteroid(Asteroid asteroid)
    {

        if (DestroyedAsteroidsNumb.ContainsKey(asteroid.Type))
            DestroyedAsteroidsNumb[asteroid.Type]++;
        else
            DestroyedAsteroidsNumb.Add(asteroid.Type, 1);
    }




    #region SerializationJSON
    public Dictionary<string, object> GetObjectData()
    {
        var data = new Dictionary<string, object>();
        data.Add("Points", Points);
        data.Add("DestroyedAsteroidsNumb", DestroyedAsteroidsNumb);
        return data;
    }
    public void SetObjectData(Dictionary<string, object> data)
    {
        if(data == null)
        {
            Points = 0;
            DestroyedAsteroidsNumb = new Dictionary<AsteroidType, int>();
        }
        else
        {
            var points = System.Convert.ToInt32(data["Points"]);
            JObject asteroids = (JObject)data["DestroyedAsteroidsNumb"];
            var asteroidStatistics = asteroids.ToObject<Dictionary<AsteroidType,int>>();
            Points = (int)points;
            DestroyedAsteroidsNumb = asteroidStatistics;
        }

    }
    #endregion

}
