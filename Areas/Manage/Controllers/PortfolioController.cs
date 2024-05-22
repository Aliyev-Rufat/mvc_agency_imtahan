using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication16.DAL;
using WebApplication16.Models;

namespace WebApplication16.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PortfolioController(AppDbContext context,IWebHostEnvironment environment)
        {
            this._context = context;
            this._environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Portfolio.ToList());
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]

        public IActionResult Create(Portfolio portfolio)
        {
            if(!ModelState.IsValid) return View();

            string path = _environment.WebRootPath + @"\Upload\";
            string filename = Guid.NewGuid() + portfolio.PhotoFile.FileName;

            using(FileStream stream = new FileStream(path+filename,FileMode.Create))
            {
                portfolio.PhotoFile.CopyTo(stream);
            }
            portfolio.ImgUrl = filename;
            _context.Portfolio.Add(portfolio);
            _context.SaveChanges();
           
            return RedirectToAction("Index");            
        }


        public IActionResult Update(int id)
        {
            var Portfolio = _context.Portfolio.FirstOrDefault(x  => x.Id == id);
            if(Portfolio == null)
            {
                return NotFound();
            }
            return View(Portfolio);
        }

        [HttpPost]

        public IActionResult Update(Portfolio portfolio)
        {
            if(!ModelState.IsValid && portfolio.PhotoFile! == null)
            {
                return View();
            }

            var oldPortfolio = _context.Portfolio.FirstOrDefault(x =>x.Id == portfolio.Id);
            if(portfolio.PhotoFile == null)
            {
                string path = _environment.WebRootPath + @"\Upload\" + oldPortfolio.PhotoFile;


                string filename = Guid.NewGuid() + portfolio.PhotoFile.FileName;

                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    portfolio.PhotoFile.CopyTo(stream);
                }

                oldPortfolio.ImgUrl = filename;
            }

            oldPortfolio.Name = portfolio.Name;
            oldPortfolio.Position = portfolio.Position;
            
            _context.SaveChanges();
            return RedirectToAction("Index");


        }


        public ActionResult Delete(int id)
        {
            var oldPortfolio = _context.Portfolio.FirstOrDefault(x => x.Id == id);

            _context.Portfolio.Remove(oldPortfolio);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
