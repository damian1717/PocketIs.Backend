using Microsoft.AspNetCore.Mvc.Razor;
using PocketIS.Framwork;
using PocketIS.Infrastucture.Persistence;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .WithMethods("GET", "POST", "PUT", "DELETE")
                          .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();

builder.Services.Configure<RazorViewEngineOptions>(c =>
{
    c.ViewLocationFormats.Clear();
    c.ViewLocationFormats.Add("/Views/Raports/{0}" + RazorViewEngine.ViewExtension);
    c.ViewLocationFormats.Add("/Views/Raports/Base/{0}" + RazorViewEngine.ViewExtension);
    c.ViewLocationFormats.Add("/Views/Raports/Components/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddRepositories();
builder.Services.AddPersistence(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
