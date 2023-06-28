using HttpClientWeb.API.Interface;
using HttpClientWeb.API.Models;
using System.Text.Json;

namespace HttpClientWeb.API.Service
{
    public class HolidaysApiService : IHolidaysApiService
    {
        //private static readonly HttpClient client;
        private readonly HttpClient client;

        //static HolidaysApiService()
        //{
        //    client = new HttpClient()
        //    {
        //        BaseAddress = new Uri("https://date.nager.at")
        //    };
        //}

        public HolidaysApiService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("PublicHolidaysApi");
        }

        public async Task<List<HolidayModel>> GetHolidays(string countryCode, int year)
        {
            var url = string.Format("/api/v2/PublicHolidays/{0}/{1}", year, countryCode);
            var result = new List<HolidayModel>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<HolidayModel>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }


        //
    }
}
