using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication16.DAL;

namespace WebApplication16.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Portfolio.ToList());
        }


    }
}