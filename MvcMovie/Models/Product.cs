using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovies.Models
{
    // Product class (I chose rental movies as the product) that stores movie title, genre, copies available, and rental price for our movie rental CRUD product catalog
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int CopiesAvailable { get; set; }
        public decimal RentalPrice { get; set; }
    }
}