namespace DreamyShop.Common.Caches
{
    public interface IRedisCacheService
    {
        T GetCachedData<T>(string key);
        void SetCachedData<T>(string key, T data, TimeSpan cacheDuration);
    }
}
