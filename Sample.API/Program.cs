using Sample.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



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

//app.UseSession(); // UseSession must be called here.

// Add HTTP security headers in ASP.NET Core using custom middleware
app.UseMiddleware<SecurityHeadersMiddleware>();

app.UseAuthorization();

app.MapControllers();

// Common Exception handling using middleware
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
