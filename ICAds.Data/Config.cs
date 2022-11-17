using System;
using Microsoft.Extensions.Configuration;
namespace ICAds.Data
{
    public static class Config
    {
        public static IConfiguration Configuration;

        public static void SetConfig(IConfiguration config)
        {
            Configuration = config;
        }

    }
}

