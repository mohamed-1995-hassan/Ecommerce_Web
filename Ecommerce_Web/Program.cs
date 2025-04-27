using Core.Interfaces;
using Ecommerce_Web.Data;
using Ecommerce_Web.Errors;
using Ecommerce_Web.Extentions;
using Ecommerce_Web.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddApplicationService(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseAuthorization();


app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
await StoreContextSeed.SeedAsync(context);

try
{
    await context.Database.MigrateAsync();
}
catch(Exception ex)
{
    logger.LogError(ex, "error happen");
}

app.Run();
