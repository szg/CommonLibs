using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
#pragma warning disable CS1591

    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PostAsync(requestUri, content).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PostAsync(requestUri, content);
#endif
        }

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data, CancellationToken cancellationToken)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PostAsync(requestUri, content, cancellationToken).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PostAsync(requestUri, content, cancellationToken);
#endif
        }

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PostAsync(requestUri, content).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PostAsync(requestUri, content);
#endif
        }

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, T data, CancellationToken cancellationToken)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PostAsync(requestUri, content, cancellationToken).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PostAsync(requestUri, content, cancellationToken);
#endif
        }

        private static StringContent GetContent<T>(T data, Encoding encoding)
        {
            if (data is string)
            {
                return new StringContent(data.ToString(), encoding, "application/json");
            }
            return new StringContent(data.ToJson(false), encoding, "application/json");
        }

        public static async Task<HttpResponseMessage> GetAsync<T>(this HttpClient httpClient, Uri requestUri, T data)
        {
#if NET45
            var response = httpClient.GetAsync(requestUri + GetHttpGetString(data)).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.GetAsync(requestUri + GetHttpGetString(data));
#endif
        }

        public static async Task<HttpResponseMessage> GetAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
#if NET45
            var response = httpClient.GetAsync(requestUri + GetHttpGetString(data)).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.GetAsync(requestUri + GetHttpGetString(data));
#endif
        }

        public static async Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
            var content = GetFormContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PostAsync(requestUri, content).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PostAsync(requestUri, content);
#endif
        }

        public static async Task<HttpResponseMessage> PostFormAsync<T>(this HttpClient httpClient, Uri requestUri, T data)
        {
            var content = GetFormContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PostAsync(requestUri, content).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PostAsync(requestUri, content);
#endif
        }

        private static StringContent GetFormContent<T>(T data, Encoding encoding)
        {
            return new StringContent(GetHttpGetString(data).TrimStart('?'), encoding, "application/x-www-form-urlencoded");
        }

        private static string GetHttpGetString<T>(T data)
        {
            if (data is string)
            {
                return data.ToString();
            }

            Type t = data.GetType();
            var properties = t.GetProperties();
            if (properties.Count() > 0)
            {
                StringBuilder urlParamBuilder = new StringBuilder("?");
                foreach (var item in t.GetProperties())
                {
                    object propertyValue = item.GetValue(data, null);
                    if (propertyValue != null)
                    {
                        urlParamBuilder.AppendFormat("{0}={1}&", item.Name, propertyValue);
                    }
                }
                urlParamBuilder.Remove(urlParamBuilder.Length - 1, 1);
                return urlParamBuilder.ToString();
            }
            else
            {
                return data.ToString();
            }
        }

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PutAsync(requestUri, content).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PutAsync(requestUri, content);
#endif
        }

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data, CancellationToken cancellationToken)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PutAsync(requestUri, content, cancellationToken).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PutAsync(requestUri, content, cancellationToken);
#endif
        }

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PutAsync(requestUri, content).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PutAsync(requestUri, content);
#endif
        }

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, T data, CancellationToken cancellationToken)
        {
            var content = GetContent(data, Encoding.UTF8);

#if NET45
            var response = httpClient.PutAsync(requestUri, content, cancellationToken).GetAwaiter().GetResult();
            return await Task.FromResult(response);
#else
            return await httpClient.PutAsync(requestUri, content, cancellationToken);
#endif
        }
    }

#pragma warning restore CS1591
}