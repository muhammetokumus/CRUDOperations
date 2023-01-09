using CRUDOperation.Application.Repositories;
using CRUDOPeration.Domain.Entities;
using CRUDOperations.UI.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CRUDOperations.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMemoryCache _memoryCache;
        public HomeController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMemoryCache memoryCache)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //Cache Mekanizması
            List<Product> products;
            var cacheKey = "time";
            if (_memoryCache.TryGetValue(cacheKey, out object list))
                return View(list);

            products = _productReadRepository.GetAll().ToList();
            var cacheEntryOptions = new MemoryCacheEntryOptions().
                SetSlidingExpiration(TimeSpan.FromSeconds(20)).
                SetPriority(CacheItemPriority.Normal);
            _memoryCache.Set(cacheKey, products, cacheEntryOptions);
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _memoryCache.Remove("time");
                await _productWriteRepository.AddAsync(product);
                await _productWriteRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _memoryCache.Remove("time");
                _productWriteRepository.Update(product);
                await _productWriteRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            if (ModelState.IsValid)
            {
                _memoryCache.Remove("time");
                _productWriteRepository.Remove(product);
                await _productWriteRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}