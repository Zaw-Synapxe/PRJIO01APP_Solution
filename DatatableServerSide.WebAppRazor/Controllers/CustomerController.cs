using DatatableServerSide.WebAppRazor.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DatatableServerSide.WebAppRazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CustomerController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Database with some Sample Data
        // https://www.mockaroo.com/

        [HttpPost]
        public IActionResult GetCustomers()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var customerData = (from tempcustomer in context.tbl_Customers select tempcustomer);

                ////sort data
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                //}

                // search data when search value found
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.FirstName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.Contact.Contains(searchValue)
                                                || m.Email.Contains(searchValue));
                }

                //get total count of data in table
                recordsTotal = customerData.Count();

                var data = customerData.Skip(skip).Take(pageSize).ToList();

                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //
    }
}
