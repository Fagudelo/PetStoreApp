using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStore.Services;

namespace PetStore.Controllers
{
    public class ProductsController(ApplicationDbContext applicationDbContext) : Controller
    {
        private readonly ApplicationDbContext dbContext = applicationDbContext;
        public IActionResult Index()
        {
            var products = dbContext.Products.OrderByDescending(p => p.Id) .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
