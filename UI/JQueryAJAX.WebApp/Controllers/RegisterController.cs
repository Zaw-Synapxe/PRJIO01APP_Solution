using JQueryAJAX.WebApp.Data;
using JQueryAJAX.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace JQueryAJAX.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        SqlConnection connection = null;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public Response RegisterNewUser(Users users)
        {
            Response response = new Response();

            DAL dal = new DAL();
            response = dal.Registration(users, connection);
            return response;

        }

        [HttpPost]
        [Route("Login")]
        public Response Login(Users users)
        {
            Response response = new Response();

            DAL dal = new DAL();
            response = dal.Login(users, connection);
            return response;

        }

        //
    }
}
