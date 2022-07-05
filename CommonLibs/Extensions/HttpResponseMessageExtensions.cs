using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System
{
#pragma warning disable CS1591

    public static class HttpResponseMessageExtensions
    {
        public async static Task<T> FromJsonAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            var exception = HttpStatusCheck(httpResponseMessage);

            var json = await httpResponseMessage.Content.ReadAsStringAsync();

            if (exception != null)
            {
                exception = new Exception(exception.Message + $"，返回内容：{json}");
                throw exception;
            }
            if (json.IsNullOrWhiteSpace())
                throw new Exception("请求返回内容为空");
            return json.FromJson<T>();
        }

        private static Exception HttpStatusCheck(HttpResponseMessage httpResponseMessage)
        {
            Exception exception = null;
            if (httpResponseMessage.StatusCode != Net.HttpStatusCode.OK)
            {
                exception = new HttpRequestException($"HTTP STATUS {(int)httpResponseMessage.StatusCode}");
            }
            return exception;
        }

        public static T FromJson<T>(this HttpResponseMessage httpResponseMessage)
        {
            var exception = HttpStatusCheck(httpResponseMessage);

            var json = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (exception != null)
            {
                exception = new Exception(exception.Message + $"，返回内容：{json}");
                throw exception;
            }
            if (json.IsNullOrWhiteSpace())
                throw new Exception("请求返回内容为空");
            return json.FromJson<T>();
        }
    }

#pragma warning restore CS1591
}