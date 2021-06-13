using LeanrDotNetCore.Config;
using Microsoft.AspNetCore.Mvc;

namespace LeanrDotNetCore.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private IConfiguratinService _config;

        
        public HomeController(IConfiguratinService config)
        {
            _config = config;
        }

        [Route("Index")]
        public string Index()
        {
            return _config.key;
        }
    }
}
