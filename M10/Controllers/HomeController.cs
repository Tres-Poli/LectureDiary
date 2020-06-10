using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using M10.Models;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace M10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Lecture> lectures = _dbContext.Lecture;
            var lecturesScheduleView = new LectureScheduleViewModel(lectures);

            return View(lecturesScheduleView);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
