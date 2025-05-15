using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Hangfire
//builder.Services.AddHangfire(x => x.UseSqlServerStorage(@"Data Source=MAXIMUS-XI\\SQLDEV2017;Initial Catalog=MyProjectApiHangFire;Integrated Security=True;Pooling=False"));
builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true, // Migration to Schema 7 is required
       }));
builder.Services.AddHangfireServer(options =>
{
    options.StopTimeout = TimeSpan.FromSeconds(15);
    options.ShutdownTimeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

// hangfire
var options = new DashboardOptions()
{
    Authorization = new[] { new MyAuthorizationFilter() }
};
app.UseHangfireDashboard("/dashboard", options);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;

    //public bool Authorize(DashboardContext context)
    //{
    //    var httpContext = context.GetHttpContext();

    //    // IMPORTANT: Allowing authenticated users to see the Dashboard is potentially dangerous!
    //    return httpContext.User.Identity.IsAuthenticated;
    //}

}