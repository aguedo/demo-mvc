using System.Reflection;
using Aslanta.Mvc;
using Aslanta.Mvc.Applications.DemoFilters;
using Aslanta.Mvc.RequestStats;
using Aslanta.Snacks.Interfaces;
using Aslanta.Snacks.Services;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Load configurations
var sharedConfigPath = builder.Environment.IsDevelopment()
    ? "appsettings.shared.json"                      // Local path
    : "/app/config/appsettings.shared.json";         // K8s path
builder.Configuration
    .AddJsonFile(sharedConfigPath, optional: true, reloadOnChange: true);
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<FeatureFlags>(
    builder.Configuration.GetSection("FeatureFlags"));

// Auth0
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.Scope = "openid profile email";
});

// Accept the standard headers
// Needed because we are behind a reverse proxy (nginx) that terminates TLS
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
    options.ForwardLimit = null;
});

// Sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Aslanta.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Demo filters
builder.Services.AddScoped<LoggerFilter>();

// Demo Output Caching
builder.Services.AddOutputCache(policy =>
{
    policy.AddPolicy("20Secs", builder =>
    {
        builder.Expire(TimeSpan.FromSeconds(20));
    });
});


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

app.UseForwardedHeaders();

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

app.UseRequestStatsMiddleware(); // Custom middleware to log request stats

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseOutputCache();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
