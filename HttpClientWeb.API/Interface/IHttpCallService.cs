namespace HttpClientWeb.API.Interface
{
    public interface IHttpCallService
    {
        Task<T> GetData<T>();
    }
}
