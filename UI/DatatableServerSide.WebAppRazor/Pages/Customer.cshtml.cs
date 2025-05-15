using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DatatableServerSide.WebAppRazor.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ILogger<CustomerModel> _logger;

        public CustomerModel(ILogger<CustomerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
