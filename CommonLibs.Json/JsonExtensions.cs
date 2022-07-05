using Newtonsoft.Json;

namespace System
{
#pragma warning disable CS1591
    public static class JsonExtensions
    {
        private static JsonSerializerSettings _defaultSetting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Include,
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };
        private static JsonSerializerSettings _ignoreNullValueSetting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };

        public static string ToJson(this object obj)
        {
            return ToJson(obj, _defaultSetting);
        }

        public static string ToJson(this object obj, bool mapNullValue = true)
        {
            if (obj == null)
                return string.Empty;
            if (!mapNullValue)
            {
                return ToJson(obj, _ignoreNullValueSetting);
            }
            else
            {
                return ToJson(obj, _defaultSetting);
            }
        }

        public static string ToJson(this object obj, JsonSerializerSettings settings = null)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj, settings ?? _defaultSetting);
        }

        public static T FromJson<T>(this string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return default(T);
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static T FromJson<T>(this string str, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                return default(T);
            return JsonConvert.DeserializeObject<T>(str, settings);
        }
    }
#pragma warning restore CS1591
}
