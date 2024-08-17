using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStore.Models;
using PetStore.Services;

namespace PetStore.Controllers
{
    public class ProductsController(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment) : Controller
    {
        private readonly ApplicationDbContext dbContext = applicationDbContext;
        //private readonly IWebHostEnvironment webHostEnvironment = environment;
        public IActionResult Index()
        {
            var products = dbContext.Products.OrderByDescending(p => p.Id).ToList();
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

            //Save the image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDTO.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/products/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDTO.ImageFile.CopyTo(stream);
            }

            //Save the new product in the datebase
            //_ = new
            //    Product()
            //{
            //    Name = productDTO.Name,
            //    Brand = productDTO.Brand,
            //    Category = productDTO.Category,
            //    Price = productDTO.Price,
            //    Description = productDTO.Description,
            //    ImageFileName = newFileName,
            //    CreatedAt = DateTime.Now
            //};

            Product product = new()
            {
                Name = productDTO.Name,
                Brand = productDTO.Brand,
                Category = productDTO.Category,
                Price = productDTO.Price,
                Description = productDTO.Description,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Products");
        }
    }
}
