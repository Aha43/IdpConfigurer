using IdpConfigurer.Application.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapIdpProviderApis();

app.Run();
