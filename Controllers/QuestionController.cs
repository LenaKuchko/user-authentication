using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StackOverFlow.Models;
using StackOverFlow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace StackOverFlow.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuestionController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }
        //See all questions
        //public IActionResult Index()
        //{
        //    return View(_db.Questions.ToList());
        //}
        //Post A questions
        public IActionResult PostQuestion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostQuestion(Question question)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            question.ApplicationUser = currentUser;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var thisQuestion = _db.Questions
                .Include(question => question.Answers)
                .FirstOrDefault(question => question.Id == id);

            return View(thisQuestion);
        }
    }
}
