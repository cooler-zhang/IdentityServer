using Newtonsoft.Json;

namespace Coo.IdentityServer.WebAPI
{
    public static class ObjectSerializerExtension
    {
        public static string ToJson(this object obj, JsonSerializerSettings jsonSerializerSettings, bool isPretty = false)
        {
            if (obj == null)
                return string.Empty;
            return JsonConvert.SerializeObject(obj, isPretty ? Formatting.Indented : Formatting.None, jsonSerializerSettings);
        }

        public static string ToJson(this object obj, bool isLoopHanding = false, bool isPretty = false)
        {
            return obj.ToJson(new JsonSerializerSettings
            {
                ReferenceLoopHandling = isLoopHanding ? ReferenceLoopHandling.Serialize : ReferenceLoopHandling.Ignore,
            }, isPretty);
        }
    }
}
