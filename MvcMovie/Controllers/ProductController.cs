using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> mylogger;

        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Title = "TENET", Genre = "Sci-Fi", CopiesAvailable = 4, RentalPrice = 3.99m },
            new Product { Id = 2, Title = "The Godfather 2", Genre = "Crime", CopiesAvailable = 2, RentalPrice = 2.99m },
            new Product { Id = 3, Title = "The Incredibles", Genre = "Animation", CopiesAvailable = 6, RentalPrice = 1.99m }
        };

        public ProductController(ILogger<ProductController> logger)
        {
            mylogger = logger;
        }

        public IActionResult Index()
        {
            mylogger.LogInformation("Index called");
            return View(_products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            mylogger.LogInformation("Create GET");
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            mylogger.LogInformation("Create POST");

            if (!ModelState.IsValid)
            {
                mylogger.LogWarning("Invalid model");
                return View(product);
            }

            product.Id = _products.Count + 1;
            _products.Add(product);

            mylogger.LogInformation("Movie added");

            return RedirectToAction("Index");
        }
    }
}
