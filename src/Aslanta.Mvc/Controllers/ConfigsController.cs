using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aslanta.Mvc.Controllers
{
    [Route("configs")]
    public class ConfigsController : Controller
    {
        // Does not automatically update when the configuration changes.
        private readonly AppSettings _settings;

        // Automatically updates when the configuration changes.
        private readonly IConfiguration _config;

        // Automatically updates when the configuration changes (Recommended).
        private readonly IOptionsMonitor<AppSettings> _settingsMonitor;
        private readonly IOptionsMonitor<FeatureFlags> _featureFlags;

        public ConfigsController(IOptionsMonitor<AppSettings> settingsMonitor,
         IConfiguration config, IOptions<AppSettings> options,
         IOptionsMonitor<FeatureFlags> featureFlags)
        {
            _settings = options.Value;
            _config = config;
            _settingsMonitor = settingsMonitor;
            _featureFlags = featureFlags;
        }

        [HttpGet("")]
        public ActionResult Index()
        {
            // IOptions
            ViewBag.Options = new
            {
                Color = _settings.Color,
                Apps = _settings.Apps
            };

            // IConfiguration
            ViewBag.Config = new
            {
                Color = _config["AppSettings:Color"],
                Apps = _config.GetSection("AppSettings:Apps").Get<AppInstance[]>()
            };

            // IOptionsMonitor
            ViewBag.OptionsMonitor = new
            {
                Color = _settingsMonitor.CurrentValue.Color,
                Apps = _settingsMonitor.CurrentValue.Apps
            };

            // IOptionsMonitor with FeatureFlags
            ViewBag.FeatureFlags = new
            {
                FeatureX = _featureFlags.CurrentValue.EnableFeatureX,
                FeatureY = _featureFlags.CurrentValue.EnableFeatureY,
                FeatureZ = _featureFlags.CurrentValue.EnableFeatureZ
            };

            return View();
        }
    }
}
