using M10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace M10.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly SHA1 _hashing;
        public AuthorizationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _hashing = new SHA1CryptoServiceProvider();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("login")]
        public IActionResult LoginUser(User data)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<User> users = _dbContext.User;
                byte[] encodedPassword = Encoding.UTF8.GetBytes(data.Password);
                var hashedPassword = _hashing.ComputeHash(encodedPassword);
                if (users.Any(i => i.Login == data.Login && i.Password == Encoding.UTF8.GetString(hashedPassword)))
                {
                    HttpContext.Session.SetString("Login", data.Login);
                    return RedirectToAction("Profile");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        [ActionName("register")]
        public async Task<IActionResult> RegisterNewUser(User data)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<User> users = _dbContext.User;
                if (!users.Any(i => i.Login == data.Login))
                {
                    try
                    {
                        byte[] hashedPassword = _hashing.ComputeHash(Encoding.UTF8.GetBytes(data.Password));
                        string passwordToStore = Encoding.UTF8.GetString(hashedPassword);
                        var userToAdd = new User { Login = data.Login, Password = passwordToStore };
                        await _dbContext.User.AddAsync(userToAdd);
                        await _dbContext.StudentScores.AddAsync
                        (
                            new StudentScores
                            {
                                StudentName = data.Login
                            }
                        );
                        await _dbContext.SaveChangesAsync();
                        HttpContext.Session.SetString("Login", data.Login);
                        return RedirectToAction("Profile");
                    }
                    catch (Exception ex)
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public IActionResult Profile()
        {
            string login = HttpContext.Session.GetString("Login");
            if (string.IsNullOrWhiteSpace(login))
            {
                return RedirectToAction("Login");
            }
            var allStudentScores = _dbContext.StudentScores;
            List<Lecture> lectures = _dbContext.Lecture.ToList();
            if (login == "Lecturer")
            {
                var lecturerProfileModel = new LecturerProfileViewModel
                {
                    Lectures = new List<string>(),
                    AllStudentScores = new Dictionary<string, List<int>>()
                };
                foreach (var lec in lectures) lecturerProfileModel.Lectures.Add(lec.Name);
                foreach (var stud in allStudentScores)
                {
                    lecturerProfileModel.AllStudentScores.Add(stud.StudentName, stud.GetScoresAsCollection().ToList());
                }
                return View("LecturerProfile", lecturerProfileModel);
            }
            else
            {
                StudentScores studentScores = allStudentScores.FirstOrDefault(i => i.StudentName == login);
                var profileViewModel = new ProfileViewModel
                {
                    UserName = studentScores.StudentName,
                    Scores = new List<int>(),
                    Lectures = new List<string>()
                };
                foreach (var lec in lectures) profileViewModel.Lectures.Add(lec.Name);
                var studentScoresResult = studentScores.GetScoresAsCollection();
                for (int i = 0; i < lectures.Count(); ++i)
                {
                    profileViewModel.Scores.Add(studentScoresResult.ElementAt(i));
                }
                return View("Profile", profileViewModel);
            }
        }
    }
}
