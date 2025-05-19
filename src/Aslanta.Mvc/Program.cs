using System.Reflection;
using Aslanta.Mvc;
using Aslanta.Mvc.RequestStats;
using Aslanta.Snacks.Interfaces;
using Aslanta.Snacks.Services;

var builder = WebApplication.CreateBuilder(args);

// Load configurations
var sharedConfigPath = builder.Environment.IsDevelopment()
    ? "appsettings.shared.json"                      // Local path
    : "/app/config/appsettings.shared.json";         // K8s path
builder.Configuration
    .AddJsonFile(sharedConfigPath, optional: true, reloadOnChange: true);
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

// Dependency injection
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISnackService, SnackService>();
builder.Services.AddSingleton<IRequestStatService, RequestStatService>();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "This is a Demo API",
        Version = "v1",
        Description = "An example ASP.NET Core MVC API with Swagger"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
    options.RoutePrefix = "swagger";
});

app.UseRequestStatsMiddleware();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
