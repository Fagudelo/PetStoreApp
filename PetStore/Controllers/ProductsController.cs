using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStore.Models;
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

        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            if (productDTO.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is requiered");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productDTO);
                }
            }
            return RedirectToAction("Index", "Products");
        }
    }
}
