using Microsoft.AspNetCore.Builder;
using PortalIpalEscalas.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

app.UseCustomMiddlewares();

app.Run();
