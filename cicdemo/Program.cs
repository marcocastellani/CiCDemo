using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BlogDC") ?? throw new InvalidOperationException("Connection string 'BlogDC' not found.");

builder.Services.AddDbContext<BlogDC>(options => options.UseNpgsql(connectionString));
builder.Services.AddCors(p=>p.AddDefaultPolicy(b=>b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(p=>p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();