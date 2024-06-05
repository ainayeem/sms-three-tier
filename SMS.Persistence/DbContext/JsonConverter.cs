using Newtonsoft.Json;

namespace SMS.Persistence.DbContext
{
    public static class JsonConverter
    {
        //Serialize
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        //Deserialize
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
