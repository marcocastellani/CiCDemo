using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BlogDC") ?? throw new InvalidOperationException("Connection string 'BlogDC' not found.");
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddDbContext<BlogDC>(options => options.UseNpgsql(connectionString));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("https://*.azurestaticapps.net")
                .SetIsOriginAllowedToAllowWildcardSubdomains();

        });
});
 

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseHsts();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
 
app.MapControllers();

app.Run();