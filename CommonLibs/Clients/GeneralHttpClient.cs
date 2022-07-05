using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs.Clients
{
#pragma warning disable CS1591

    public interface IGeneralHttpClient
    {
        string Get(string uri);

        Task<string> GetAsync(string uri);

        Tout Get<Tout>(string uri) where Tout : class, new();

        Task<Tout> GetAsync<Tout>(string uri) where Tout : class, new();

        string Get<Tin>(string uri, Tin tin);

        Task<string> GetAsync<Tin>(string uri, Tin tin);

        string Get<Tin>(Uri uri, Tin tin);

        Task<string> GetAsync<Tin>(Uri uri, Tin tin);

        Tout Get<Tin, Tout>(string uri, Tin tin) where Tout : class, new();

        Task<Tout> GetAsync<Tin, Tout>(string uri, Tin tin) where Tout : class, new();

        string Post(string uri);

        Task<string> PostAsync(string uri);

        Tout Post<Tout>(string uri) where Tout : class, new();

        Task<Tout> PostAsync<Tout>(string uri) where Tout : class, new();

        string Post<Tin>(string uri, Tin tin);

        Task<string> PostAsync<Tin>(string uri, Tin tin);

        string Post<Tin>(Uri uri, Tin tin);

        Task<string> PostAsync<Tin>(Uri uri, Tin tin);

        Tout Post<Tin, Tout>(string uri, Tin tin) where Tout : class, new();

        Task<Tout> PostAsync<Tin, Tout>(string uri, Tin tin) where Tout : class, new();

        string PostForm<Tin>(string uri, Tin tin);

        Task<string> PostFormAsync<Tin>(string uri, Tin tin);

        Tout PostForm<Tin, Tout>(string uri, Tin tin);

        Task<Tout> PostFormAsync<Tin, Tout>(string uri, Tin tin);

        IEnumerable<string> GetHeaders(string name);

        bool TryGetHeaders(string name, out IEnumerable<string> values);

        void AddHeaders(string name, string value);

        void AddHeaders(string name, IEnumerable<string> values);

        bool RemoveHeader(string name);

        IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetAllHeaders();

        void ClearHeader();

        bool ContainsHeader(string name);

        string Send(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null);

        Task<string> SendAsync(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null);

        string Send<Tin>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null);

        Task<string> SendAsync<Tin>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null);

        Tout Send<Tout>(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null) where Tout : class, new();

        Task<Tout> SendAsync<Tout>(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null) where Tout : class, new();

        Tout Send<Tin, Tout>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null) where Tout : class, new();

        Task<Tout> SendAsync<Tin, Tout>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null) where Tout : class, new();
    }

    public class GeneralHttpClient : IGeneralHttpClient
    {
        private readonly HttpClient _client;

        public GeneralHttpClient(HttpClient client)
        {
            _client = client;
        }

        public IEnumerable<string> GetHeaders(string name)
        {
            return _client.DefaultRequestHeaders.GetValues(name);
        }

        public bool TryGetHeaders(string name, out IEnumerable<string> values)
        {
            return _client.DefaultRequestHeaders.TryGetValues(name, out values);
        }

        public void AddHeaders(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        public void AddHeaders(string name, IEnumerable<string> values)
        {
            _client.DefaultRequestHeaders.Add(name, values);
        }

        public bool RemoveHeader(string name)
        {
            return _client.DefaultRequestHeaders.Remove(name);
        }

        public void ClearHeader()
        {
            _client.DefaultRequestHeaders.Clear();
        }

        public bool ContainsHeader(string name)
        {
            return _client.DefaultRequestHeaders.Contains(name);
        }

        public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetAllHeaders()
        {
            return _client.DefaultRequestHeaders.GetEnumerator();
        }

        public string Get(string uri)
        {
            var response = _client.GetAsync(uri).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public Tout Get<Tout>(string uri) where Tout : class, new()
        {
            var response = _client.GetAsync(uri).GetAwaiter().GetResult();

#if NET45
            return response.FromJson<Tout>();
#else
            return response.FromJsonAsync<Tout>().GetAwaiter().GetResult();
#endif
        }

        public string Get<Tin>(string uri, Tin tin)
        {
            var response = _client.GetAsync<Tin>(uri, tin).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public string Get<Tin>(Uri uri, Tin tin)
        {
            var response = _client.GetAsync<Tin>(uri, tin).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public Tout Get<Tin, Tout>(string uri, Tin tin) where Tout : class, new()
        {
            var response = _client.GetAsync<Tin>(uri, tin).GetAwaiter().GetResult();

#if NET45
            return response.FromJson<Tout>();
#else
            return response.FromJsonAsync<Tout>().GetAwaiter().GetResult();
#endif
        }

        public async Task<string> GetAsync(string uri)
        {
            var response = await _client.GetAsync(uri);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Tout> GetAsync<Tout>(string uri) where Tout : class, new()
        {
            var response = await _client.GetAsync(uri);
#if NET45
            return response.FromJson<Tout>();
#else
            return await response.FromJsonAsync<Tout>();
#endif
        }

        public async Task<string> GetAsync<Tin>(string uri, Tin tin)
        {
            var response = await _client.GetAsync<Tin>(uri, tin);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAsync<Tin>(Uri uri, Tin tin)
        {
            var response = await _client.GetAsync<Tin>(uri, tin);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Tout> GetAsync<Tin, Tout>(string uri, Tin tin) where Tout : class, new()
        {
            var response = await _client.GetAsync<Tin>(uri, tin);

#if NET45
            return response.FromJson<Tout>();
#else
            return await response.FromJsonAsync<Tout>();
#endif
        }

        public string Post(string uri)
        {
            var response = _client.PostAsync(uri, new StringContent("")).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public Tout Post<Tout>(string uri) where Tout : class, new()
        {
            var response = _client.PostAsync(uri, new StringContent("")).GetAwaiter().GetResult();

#if NET45
            return response.FromJson<Tout>();
#else
            return response.FromJsonAsync<Tout>().GetAwaiter().GetResult();
#endif
        }

        public string Post<Tin>(string uri, Tin tin)
        {
            var response = _client.PostJsonAsync<Tin>(uri, tin).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public string Post<Tin>(Uri uri, Tin tin)
        {
            var response = _client.PostJsonAsync<Tin>(uri, tin).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public Tout Post<Tin, Tout>(string uri, Tin tin) where Tout : class, new()
        {
            var response = _client.PostJsonAsync<Tin>(uri, tin).GetAwaiter().GetResult();

#if NET45
            return response.FromJson<Tout>();
#else
            return response.FromJsonAsync<Tout>().GetAwaiter().GetResult();
#endif
        }

        public async Task<string> PostAsync(string uri)
        {
            var response = await _client.PostAsync(uri, new StringContent(""));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Tout> PostAsync<Tout>(string uri) where Tout : class, new()
        {
            var response = await _client.PostAsync(uri, new StringContent(""));

#if NET45
            return response.FromJson<Tout>();
#else
            return await response.FromJsonAsync<Tout>();
#endif
        }

        public async Task<string> PostAsync<Tin>(string uri, Tin tin)
        {
            var response = await _client.PostJsonAsync<Tin>(uri, tin);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync<Tin>(Uri uri, Tin tin)
        {
            var response = await _client.PostJsonAsync<Tin>(uri, tin);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Tout> PostAsync<Tin, Tout>(string uri, Tin tin) where Tout : class, new()
        {
            var response = await _client.PostJsonAsync<Tin>(uri, tin);

#if NET45
            return response.FromJson<Tout>();
#else
            return await response.FromJsonAsync<Tout>();
#endif
        }

        public string PostForm<Tin>(string uri, Tin tin)
        {
            var response = _client.PostFormAsync<Tin>(uri, tin).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public async Task<string> PostFormAsync<Tin>(string uri, Tin tin)
        {
            var response = await _client.PostFormAsync<Tin>(uri, tin);

            return await response.Content.ReadAsStringAsync();
        }

        public Tout PostForm<Tin, Tout>(string uri, Tin tin)
        {
            var response = _client.PostFormAsync<Tin>(uri, tin).GetAwaiter().GetResult();

#if NET45
            return response.FromJson<Tout>();
#else
            return response.FromJsonAsync<Tout>().GetAwaiter().GetResult();
#endif
        }

        public async Task<Tout> PostFormAsync<Tin, Tout>(string uri, Tin tin)
        {
            var response = await _client.PostFormAsync<Tin>(uri, tin);

#if NET45
            return response.FromJson<Tout>();
#else
            return await response.FromJsonAsync<Tout>();
#endif
        }

        #region Send 方法

        private HttpRequestMessage GetHttpRequestMessage<T>(T entity, string uri, HttpMethod method, Dictionary<string, string> headers = null, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(method, uri);
            request.Content = GetContent(entity, contentType);
            if (headers != null && headers.Keys.Count > 0)
            {
                foreach (var kvp in headers)
                {
                    if (!request.Headers.Contains(kvp.Key))
                    {
                        request.Headers.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            return request;
        }

        private StringContent GetContent<T>(T entity, string contentType = "application/json")
        {
            return new StringContent(entity.ToJson(), Encoding.UTF8, contentType);
        }

        public string Send(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null)
        {
            var request = GetHttpRequestMessage<string>(null, uri, method, headers, contentType);
            var response = _client.SendAsync(request).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public async Task<string> SendAsync(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null)
        {
            var request = GetHttpRequestMessage<string>(null, uri, method, headers, contentType);
            var response = await _client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }

        public string Send<Tin>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null)
        {
            var request = GetHttpRequestMessage(tin, uri, method, headers);
            var response = _client.SendAsync(request).GetAwaiter().GetResult();

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public async Task<string> SendAsync<Tin>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null)
        {
            var request = GetHttpRequestMessage(tin, uri, method, headers);
            var response = await _client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }

        public Tout Send<Tout>(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null) where Tout : class, new()
        {
            var request = GetHttpRequestMessage<string>(null, uri, method, headers, contentType);
            var response = _client.SendAsync(request).GetAwaiter().GetResult();

#if NET45
            return response.FromJson<Tout>();
#else
            return response.FromJsonAsync<Tout>().GetAwaiter().GetResult();
#endif
        }

        public async Task<Tout> SendAsync<Tout>(string uri, HttpMethod method, string contentType = "application/json", Dictionary<string, string> headers = null) where Tout : class, new()
        {
            var request = GetHttpRequestMessage<string>(null, uri, method, headers, contentType);
            var response = await _client.SendAsync(request);

#if NET45
            return response.FromJson<Tout>();
#else
            return await response.FromJsonAsync<Tout>();
#endif
        }

        public Tout Send<Tin, Tout>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null) where Tout : class, new()
        {
            var request = GetHttpRequestMessage(tin, uri, method, headers);
            var response = _client.SendAsync(request).GetAwaiter().GetResult();

#if NET45
            return response.FromJson<Tout>();
#else
            return response.FromJsonAsync<Tout>().GetAwaiter().GetResult();
#endif
        }

        public async Task<Tout> SendAsync<Tin, Tout>(string uri, HttpMethod method, Tin tin, Dictionary<string, string> headers = null) where Tout : class, new()
        {
            var request = GetHttpRequestMessage(tin, uri, method, headers);
            var response = await _client.SendAsync(request);

#if NET45
            return response.FromJson<Tout>();
#else
            return await response.FromJsonAsync<Tout>();
#endif
        }

        #endregion Send 方法
    }

#pragma warning restore CS1591
}