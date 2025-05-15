using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;
using Persistence.Extensions;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.

//////
////var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
////if (connectionString == null)
////{
////    throw new ApplicationException("DefaultConnection is not set");
////}


////builder.Services.AddDbContext<ApplicationDbContext>(x =>
////    x.UseSqlServer(connectionString,
////                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

////// ------------------------------
////#region Repositories
////builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
////builder.Services.AddTransient<IDeveloperRepository, DeveloperRepository>();
////builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
////#endregion
////builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//////

//////////builder.Services.AddPersistenceLayer(builder.Configuration);

//

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
