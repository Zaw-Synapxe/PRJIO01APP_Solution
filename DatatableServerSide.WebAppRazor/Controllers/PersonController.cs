using DatatableServerSide.WebAppRazor.Data;
using DatatableServerSide.WebAppRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatatableServerSide.WebAppRazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult GetPersons()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault(); // get total page size
                var start = Request.Form["start"].FirstOrDefault(); // get starte length size from request.
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault(); // check if there is any search characters passed
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var personData = (from tempPerson in context.tbl_Persons select tempPerson); // get data from database
                //check for sorting column number and direction
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //get sorting column
                    Func<Person, string> orderingFunction = (c => sortColumn == "First Name" ? c.FirstName : sortColumn == "Last Name" ? c.LastName : c.FirstName);

                    //check sort order 
                    if (sortColumnDirection == "desc")
                    {
                        personData = personData.OrderByDescending(orderingFunction).AsQueryable();
                    }
                    else
                    {
                        personData = personData.OrderBy(orderingFunction).AsQueryable();
                    }

                }

                // if there is any search value, filter results
                if (!string.IsNullOrEmpty(searchValue))
                {
                    personData = personData.Where(m => m.FirstName.ToLower().Contains(searchValue.ToLower())
                                                || m.LastName.ToLower().Contains(searchValue.ToLower()));
                }
                // get total records acount
                recordsTotal = personData.Count();
                //get page data
                var data = personData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //[HttpPost]
        //public JsonResult GetEmployeeList()
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
        //    int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            
        //    var data = _context.Set<Employees>().AsQueryable();
            
        //    //get total count of data in table
        //    totalRecord = data.Count();

        //    // search data when search value found
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        data = data.Where(x => x.EmployeeFirstName.ToLower().Contains(searchValue.ToLower()) || x.EmployeeLastName.ToLower().Contains(searchValue.ToLower()) || x.Designation.ToLower().Contains(searchValue.ToLower()) || x.Salary.ToString().ToLower().Contains(searchValue.ToLower()));
        //    }

        //    // get total count of records after search
        //    filterRecord = data.Count();

        //    //sort data
        //    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(sortColumn + " " + sortColumnDirection);
            
        //    //pagination
        //    var empList = data.Skip(skip).Take(pageSize).ToList();

        //    var returnObj = new
        //    {
        //        draw = draw,
        //        recordsTotal = totalRecord,
        //        recordsFiltered = filterRecord,
        //        data = empList
        //    };
        //}
    }


    //

}
