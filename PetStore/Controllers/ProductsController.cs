using Microsoft.AspNetCore.Mvc;

namespace PetStore.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
