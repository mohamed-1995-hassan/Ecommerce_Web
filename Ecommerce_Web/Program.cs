using Core.Entities.identity;
using Core.Interfaces;
using Ecommerce_Web.Data;
using Ecommerce_Web.Errors;
using Ecommerce_Web.Extentions;
using Ecommerce_Web.Middleware;
using Infrastructure.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDocumentation();
  


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
await StoreContextSeed.SeedAsync(context);
await AppIdentitySeed.SeedUserAsync(userManager);

try
{
    await context.Database.MigrateAsync();
}
catch(Exception ex)
{
    logger.LogError(ex, "error happen");
}

app.Run();
