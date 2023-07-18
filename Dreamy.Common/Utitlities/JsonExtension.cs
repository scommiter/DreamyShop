using Newtonsoft.Json;

namespace Dreamy.Common.Utitlities
{
    public static class JsonExtension
    {
        /// <summary>
        /// Serialisation of any object to JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// The MissingMemberHandling and ReferenceLoopHandling properties are configured to help 
        /// limit problems that can occur when converting objects that are linked together or have missing properties.
        /// <returns></returns>
        public static string ToJsonString<T>(this T @object)
        {
            JsonSerializerSettings config = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(@object, config);
        }

        public static T ToJsonObject<T>(this string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
        }
    }
}
