using Sample.API.Middleware;
using Sample.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ProductsRepository>();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


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

app.UseAuthorization();

// Add HTTP security headers in ASP.NET Core using custom middleware
app.UseMiddleware<SecurityHeadersMiddleware>();

app.MapControllers();

// Common Exception handling using middleware
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
