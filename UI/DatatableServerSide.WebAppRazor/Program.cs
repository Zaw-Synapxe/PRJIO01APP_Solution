using DatatableServerSide.WebAppRazor.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using DatatableServerSide.WebAppRazor.Services.CsvService;
using DatatableServerSide.WebAppRazor.Services.ExportService;
using DatatableServerSide.WebAppRazor.Services.ExcelService;
using DatatableServerSide.WebAppRazor.Services.HtmlService;
using DatatableServerSide.WebAppRazor.Services.JsonService;
using DatatableServerSide.WebAppRazor.Services.XmlService;
using DatatableServerSide.WebAppRazor.Services.YamlService;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
});

builder.Services.AddControllers();
//builder.Services.AddControllers()
//    .AddNewtonsoftJson(options =>
//    {
//        options.SerializerSettings.Converters.Add(new StringEnumConverter());
//    });

//
builder.Services.AddRazorPages();

builder.Services.AddScoped<IExportService, ExportService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IHtmlService, HtmlService>();
builder.Services.AddScoped<IJsonService, JsonService>();
builder.Services.AddScoped<IXmlService, XmlService>();
builder.Services.AddScoped<IYamlService, YamlService>();

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DatatableServerSide.WebAppRazor", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.FirstOrDefault());

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//// global cors policy
//app.UseCors(x => x
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .SetIsOriginAllowed(origin => true) // allow any origin 
//    .AllowCredentials());

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "docs";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DatatableServerSide.WebAppRazor Docs v1");

});

app.UseRouting();

app.UseAuthorization();

//
app.MapControllers();
app.MapRazorPages();
//

app.Run();
