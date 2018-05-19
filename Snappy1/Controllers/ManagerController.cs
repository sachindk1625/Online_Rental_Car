using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snappy1.Models;
using Microsoft.AspNetCore.Authorization;
using Snappy1.Controllers;
using Microsoft.AspNetCore.Identity;

namespace Snappy1.Controllers
{
    [Authorize(Roles ="Manager")]
    public class ManagerController : Controller
    {
        
        private online_resEntities _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public ManagerController(online_resEntities context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var username = _userManager.GetUserName(User);

            var cars = from c in _context.Car
                       where c.ManagerUsername==username
                       select c;
            return View(cars);
        }

        public IActionResult RentCars()
        {
            var cars = from c in _context.CarRental select c;
            return View(cars);
        }


        [HttpGet]
        public IActionResult AddCar()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddCar(Car cars)
        {
            int uniqueCarId = (from a in _context.Car
                               orderby a.CarId descending
                               select a.CarId).FirstOrDefault();
            cars.CarId = uniqueCarId+1;
            var Managerusername = _userManager.GetUserName(User);
            cars.ManagerUsername = Managerusername;
            cars.LocationName = _context.Location.Where(l => l.ManagerUsername == Managerusername).Select(a => a.LocationName).FirstOrDefault();        
            _context.Add(cars);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int carId)
        {
            var item = (from Car c in _context.Car
                        where c.CarId == carId
                        select c).FirstOrDefault();

            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int carId)
        {
            var item = (from Car c in _context.Car
                        where c.CarId == carId
                        select c).FirstOrDefault();

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Car cars)
        {
            //_context.Add(cars);
            _context.Update(cars);
            _context.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int carId)
        {
            var item = (from Car c in _context.Car
                        where c.CarId == carId
                        select c).FirstOrDefault();

            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(Car cars)
        {
            //_context.Add(cars);
            _context.Remove(cars);
            _context.SaveChanges();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}