public interface IHttpHelper
{
    Task<string> DeleteAsync(string url);
    Task<string> GetAsync(string url);
    Task<string> PostAsync(string url, string content);
    Task<string> PutAsync(string url, string content);
}