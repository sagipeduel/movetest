namespace movetest.Helpers
{
    public class HttpHelper : IHttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to GET from '{url}'. Status code: {response.StatusCode}");
        }

        public async Task<string> PostAsync(string url, string content)
        {
            HttpContent httpContent = new StringContent(content);
            HttpResponseMessage response = await _httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to POST to '{url}'. Status code: {response.StatusCode}");
        }

        public async Task<string> PutAsync(string url, string content)
        {
            HttpContent httpContent = new StringContent(content);
            HttpResponseMessage response = await _httpClient.PutAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to Put to '{url}'. Status code: {response.StatusCode}");
        }

        public async Task<string> DeleteAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to DELETE from '{url}'. Status code: {response.StatusCode}");
        }
    }

}
