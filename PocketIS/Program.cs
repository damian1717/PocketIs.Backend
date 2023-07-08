using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using PocketIS.Application.Common.Authentication;
using PocketIS.Application.Common.Mvc;
using PocketIS.Application.Common.Swagger;
using PocketIS.Framwork;
using PocketIS.Infrastucture.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwt();
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

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

app.UseErrorHandler();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
