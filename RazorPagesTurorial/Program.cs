using Microsoft.EntityFrameworkCore;
using RazorPagesTurorial.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("EmployeeDBConnection");
// Add services to the container.
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

builder.Services.Configure<RouteOptions>(
    options => 
    {   options.LowercaseQueryStrings = true;
        options.LowercaseUrls = true;
        options.AppendTrailingSlash = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
