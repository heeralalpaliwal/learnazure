using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeanrDotNetCore.Config
{
    public class ConfiguratinService : IConfiguratinService
    {
        private readonly IConfiguration _config;

        public ConfiguratinService(IConfiguration config)
        {
            _config = config;
        }

        public string key { get => _config["key"]; }
    }
}
