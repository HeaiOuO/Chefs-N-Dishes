using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefDish.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ChefDish.Controllers
{
    public class HomeController : Controller
    {
        private ChefContext dbContext;
        public HomeController(ChefContext context)
        {
            dbContext = context;
        }

        //localhost:5000
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.chefs
            .Include(chef => chef.dishs)
            .ToList();
            ViewBag.allchefs = AllChefs;
            return View();
        }

        //localhost:5000/dishes
        [Route("Dishes")]
        [HttpGet]
        public IActionResult Dish()
        {
            List<Dish> AllDishes = dbContext.dishs
            .Include(dish =>dish.Creator)
            .ToList();
            ViewBag.alldishes = AllDishes;
            return View("Dishes");
        }

        //localhost:5000/AddChefView (add a chef view all chefs)
        [HttpGet]
        [Route("AddChefView")]

        public IActionResult AddChefView()
        {
            return View("AddChef");
        }

        //localhost:5000/Addchef 
        [HttpPost]
        [Route("AddChef")]
        public IActionResult AddChef(Chef chef)
        {
            if(ModelState.IsValid)
            {
                if(chef.Birthday >= DateTime.Today)
                {
                    ModelState.AddModelError("Birthday", "Birthday must be from the past!");
                    return View("AddChef");
                }
                Chef newChef = new Chef
                {
                    FirstName = chef.FirstName,
                    LastName = chef.LastName,
                    Birthday = chef.Birthday,
                };
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddChef");
            }
        }

        //localhost:5000/AddDishView (add a dish page view all dishes)
        [HttpGet]
        [Route("AddDishView")]
        public IActionResult AddDishView()
        {
            List<Chef> AllChefs = dbContext.chefs.ToList();
            ViewBag.allchefs = AllChefs;
            return View("AddDish");
        }

        //localhost:5000/AddDish
        [HttpPost]
        [Route("AddDish")]
        public IActionResult AddDish(Dish dish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("AddDishView");
            }
            else
            {
                List<Chef> AllChefs = dbContext.chefs.ToList();
                ViewBag.allchefs = AllChefs;
                return View("AddDish", dish);
            }
        }       
        

    }
}
