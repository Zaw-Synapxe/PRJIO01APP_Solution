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

app.UseAuthorization();

app.MapControllers();

app.Run();


/*
 
options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        //Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        Description = "Standard Authorization header using the Bearer scheme", // <<<<<<
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey // <<<<<<
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

 */