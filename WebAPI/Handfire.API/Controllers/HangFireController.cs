using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handfire.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        // https://codewithmukesh.com/blog/hangfire-in-aspnet-core-3-1/

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from HangFire web API.");
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Welcome()
        {
            //https://localhost:5001/api/hangfire/welcome
            var jobId = BackgroundJob.Enqueue(() => SendWelcomeEmail("zaw", "Welcome to our app."));
            return Ok($"Job ID: {jobId}. Welcome email sent to the user.");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Discount()
        {
            // https://localhost:5001/api/hangfire/discount
            int timeInSeconds = 30;
            var jobId = BackgroundJob.Schedule(() => SendWelcomeEmail("zaw", "Welcome to our app"), TimeSpan.FromSeconds(timeInSeconds));
            //var jobId = BackgroundJob.Schedule(() => SendWelcomeEmail("zaw", "Welcome to our app"), TimeSpan.FromMinutes(2));

            return Ok($"Job ID: {jobId}. Discount email will be sent in {timeInSeconds} seconds !");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult DatabaseUpdate()
        {
            // https://localhost:5001/api/hangfire/databaseupdate
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Database updated"), Cron.Minutely);
            return Ok("Database check job initiated!");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Invoice(string userName)
        {
            // https://localhost:5001/api/hangfire/invoice
            // https://docs.oracle.com/cd/E12058_01/doc/doc.1014/e12030/cron_expressions.htm
            RecurringJob.AddOrUpdate(() => SendInvoiceMail(userName), Cron.Monthly);
            return Ok($"Recurring Job Scheduled. Invoice will be mailed Monthly for {userName}!");
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Confirm()
        {
            // https://localhost:5001/api/hangfire/confirm
            int timeInSeconds = 30;
            var parentJobId = BackgroundJob.Schedule(() => Console.WriteLine("You asked to be unsubscribed!"), TimeSpan.FromSeconds(timeInSeconds));

            BackgroundJob.ContinueJobWith(parentJobId, () => Console.WriteLine("You were unsubscribed!"));

            return Ok("Confirmation job created!");
        }

        //
        [HttpPost("register")]
        public IActionResult Register()
        {
            // register user

            // create fire-and-forget job to send welcome mail.
            BackgroundJob.Enqueue(() => SendWelcomeMail());

            return Ok();
        }

        [HttpPost("addtocart")]
        public IActionResult AddToCart()
        {
            // add item to user's cart

            // create scheduled job to send reminder mail if user has not completed order.
            BackgroundJob.Schedule(() => SendReminder(), TimeSpan.FromMinutes(5));

            return Ok();
        }

        [HttpGet("addrecurringjob")]
        public IActionResult AddRecurringJob()
        {
            RecurringJob.AddOrUpdate("Pending Deliveries Job", () => CheckPendingDeliveries(), Cron.Hourly);

            return Ok();
        }

        // Methods
        public static void SendWelcomeMail()
        {
            Console.WriteLine("Welcome mail has been sent to user");
        }

        public void SendWelcomeEmail(string usr, string txt)
        {
            Console.WriteLine("User : " + usr + ", Message :" + txt);
        }

        public void SendInvoiceMail(string userName)
        {
            //Logic to Mail the user
            Console.WriteLine($"Here is your invoice, {userName}");
        }

        public static void SendReminder()
        {
            bool completed = new Random().Next() % 2 == 0;

            if (completed)
            {
                Console.WriteLine("The user has already completed the order for cart item xxxx");
            }
            else
            {
                Console.WriteLine("A reminder has been sent to user for cart item xxxx");
            }
        }

        public static void CheckPendingDeliveries()
        {
            int number = new Random().Next(1, 10);

            Console.WriteLine($"There are {number} pending deliveries");
        }
        //
    }
}
