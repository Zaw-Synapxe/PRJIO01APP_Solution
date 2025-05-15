using HttpClientWeb.API.Models;

namespace HttpClientWeb.API.Interface
{
    public interface IHolidaysApiService
    {
        Task<List<HolidayModel>> GetHolidays(string countryCode, int year);
    }
}
