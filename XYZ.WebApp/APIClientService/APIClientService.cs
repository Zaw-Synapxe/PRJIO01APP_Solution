using RestSharp;

namespace XYZ.WebApp.APIClientService
{
    public class APIClientService<T> : IAPIClientService<T> where T : class
    {
        private readonly RestClient _RestClient;
        private readonly string _baseurl = "https://localhost:7150/api/";
        public APIClientService()
        {
            _RestClient = new RestClient(_baseurl);
        }
        public async Task<List<T>> GetAll(string subURL)
        {
            RestRequest _RestRequest = new(subURL, Method.Get)
            { RequestFormat = DataFormat.Json };

            var response = await _RestClient.ExecuteAsync<List<T>>(_RestRequest);
            
            if (response.Data == null)
                throw new Exception(response.ErrorMessage);
            return response.Data;
        }
        public async Task<T> GetById(Int64 Id, string subURL)
        {
            RestRequest _RestRequest = new(subURL + Id, Method.Get)
            { RequestFormat = DataFormat.Json };

            var response = await _RestClient.ExecuteAsync<T>(_RestRequest);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);
            return response.Data;
        }

        public async Task<RestResponse> Add(T model, string subURL)
        {
            RestRequest _RestRequest = new(subURL, Method.Post);
            _RestRequest.AddJsonBody(model);
            RestResponse _RestResponse = await _RestClient.ExecuteAsync(_RestRequest);
            return _RestResponse;
        }
        public async Task<RestResponse> Update(T model, string subURL)
        {
            RestRequest _RestRequest = new(subURL, Method.Put);
            _RestRequest.AddJsonBody(model);
            RestResponse _RestResponse = await _RestClient.ExecuteAsync(_RestRequest);
            return _RestResponse;
        }
        public async Task<RestResponse> Delete(Int64 Id, string subURL)
        {
            RestRequest _RestRequest = new(subURL + Id, Method.Delete);
            RestResponse _RestResponse = await _RestClient.ExecuteAsync(_RestRequest);
            return _RestResponse;
        }
    }
}
