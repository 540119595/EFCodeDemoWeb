using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.IServices.Sys;
using Domain.Models.Sys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EFCodeDemoWeb.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;
        private IUserService _userService;

        public HomeController(IMemoryCache memoryCache, IUserService userService)
        {
            _cache = memoryCache;
            _userService = userService;
        }

        public IActionResult Index()
        {
            // Look for cache key.
            if (!_cache.TryGetValue("CacheKeys.Entry", out DateTime cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = DateTime.Now;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    // 设置相对过期时间3秒
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3))
                    .RegisterPostEvictionCallback((key, value, reason, substate) =>
                    {
                        Console.WriteLine($"键{key}值{value}改变，因为{reason}");
                    });

                // Save data in cache.
                _cache.Set("CacheKeys.Entry", cacheEntry, cacheEntryOptions);
            }

            return View("Index", cacheEntry);//Content("Hello test");
        }
    }
}