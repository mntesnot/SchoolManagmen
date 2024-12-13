using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagmen.Models;

namespace SchoolManagmen.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Students = new List<Student>
            {
                new Student
                    {
                        Id = 1,
                        Name="Test",
                        Age=15,
                        PhoneNo="588945416"

                    },new Student
                    {
                        Id = 2,
                        Name="Test 2",
                        Age=15,
                        PhoneNo="588945416"
                    },new Student
                    {
                        Id = 3,
                        Name="Test-3",
                        Age=15,
                        PhoneNo="588945416"
                    }
            };
          
            

            
            return View(Students);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();  
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
