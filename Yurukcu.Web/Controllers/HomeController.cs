using Microsoft.AspNetCore.Mvc;
using Yurukcu.Web.Data;

public class HomeController : Controller
{
    private readonly ProductContext _context;

    public HomeController(ProductContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products); 
    }
    public IActionResult AboutUs()
    {
        return View();
    }
}
