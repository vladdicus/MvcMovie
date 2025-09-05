using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> mylogger;

        public ProductController(ILogger<ProductController> logger)
        {
            mylogger = logger;
        }

        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Title = "TENET", Genre = "Sci-Fi", CopiesAvailable = 4, RentalPrice = 3.99m },
            new Product { Id = 2, Title = "The Godfather 2", Genre = "Crime", CopiesAvailable = 2, RentalPrice = 2.99m },
            new Product { Id = 3, Title = "The Incredibles", Genre = "Animation", CopiesAvailable = 6, RentalPrice = 1.99m }
        };

        public IActionResult Index()
        {
            mylogger.LogInformation("Index called");
            return View(_products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            mylogger.LogInformation("Create GET called");
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            mylogger.LogInformation("Create POST called");
            if (!ModelState.IsValid)
            {
                mylogger.LogInformation("Model state is invalid");
                return View(product);
            }

            mylogger.LogInformation("Model state is valid, adding product");
            product.Id = _products.Count + 1;
            _products.Add(product);

            return RedirectToAction("Index");
        }

        // added an edit function because it wasn't there before in my previous build
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // see comment above for hhtpget edit
        [HttpPost]
        public IActionResult Edit(int id, Product updated)
        {
            if (id != updated.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(updated);
            }

            var index = _products.FindIndex(p => p.Id == id);
            if (index == -1)
            {
                return NotFound();
            }

            _products[index].Title = updated.Title;
            _products[index].Genre = updated.Genre;
            _products[index].CopiesAvailable = updated.CopiesAvailable;
            _products[index].RentalPrice = updated.RentalPrice;

            return RedirectToAction("Index");
        }
    }
}