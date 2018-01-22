using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFCodeDemoWeb
{
    public class Program
    {
        // 修改之后，需要单独打开浏览器、输入配置的地址信息进行网站访问
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {   
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: false, reloadOnChange: true)
                .Build();

            return WebHost.CreateDefaultBuilder(args)   // 默认会加很多内容
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
