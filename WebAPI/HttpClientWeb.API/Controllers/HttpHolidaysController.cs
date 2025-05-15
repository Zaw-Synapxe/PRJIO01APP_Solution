using HttpClientWeb.API.Interface;
using HttpClientWeb.API.Models;
using HttpClientWeb.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpHolidaysController : ControllerBase
    {

        private readonly IHolidaysApiService _holidaysApiService;

        public HttpHolidaysController(IHolidaysApiService holidaysApiService)
        {
            _holidaysApiService = holidaysApiService;
        }

        [HttpGet]
        [Route("GetHolidaysData")]
        public async Task<IActionResult> GetHolidays(string countryCode, int year)
        {
            try
            {
                var response = await _holidaysApiService.GetHolidays(countryCode, year);
                return (response is null) ? NotFound(response) : Ok(response);
            }
            catch (Exception)
            {
                throw;
            }

        }

        //
    }
}
