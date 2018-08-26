// taken from https://github.com/NeelBhatt/DotNetCoreSessionSample/blob/master/NeelSessionExample/Utility/SessionExtension.cs
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BankingApp.Utility
{
    public static class SessionSerializer
    {
        public static void SetObjectAsJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}