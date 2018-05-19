﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snappy1.Models;
using System.Web;
using Snappy1.Implementation;
using Microsoft.AspNetCore.Authorization;

namespace Snappy1.Controllers
{ 
    [Authorize(Roles="Customer")]
    public class CustomerController : Controller
    {
        private readonly CustomerManager _customerManager;
        public CustomerController()
        {
            _customerManager =  new CustomerManager();
        }
        public ActionResult Index()
        {
            return View(new BookingModel() { AllLocations = _customerManager.GetLocation() });
        }

        [HttpPost]
        public ActionResult CheckAvailability(BookingModel bookingModel)
        {
            
            if (ModelState.IsValid)
            {
                //HttpContext.Session.SetS("BookingModel","kjbsakjbkac");
                //this.HttpContext.Items["BookingModel"] = bookingModel;
                var carsAvailable = _customerManager.GetCarsAvailable(bookingModel);
                return View(carsAvailable);

            }
            bookingModel.AllLocations = _customerManager.GetLocation();
            return View("Index", bookingModel);
        }
        [HttpGet]
        public ActionResult ConfirmBooking(string carModelName, double totalAmount, string imagePath,DateTime startDate,DateTime endDate,string location)
        {

            var bookingModel = new BookingModel {  StartDate=startDate,EndDate=endDate,Location=location};
            var userModel = new UserInfoModel
            {
                BookingModel = bookingModel,
                CarAvailabiltyModel = new CarAvailabiltyModel { Name = carModelName, AmountTotal = totalAmount, ImagePath = imagePath },

            };
            return View(userModel);
        }

        [HttpPost]
        public ActionResult ConfirmBooking(UserInfoModel userModel)
        {
            if (ModelState.IsValid || (!string.IsNullOrEmpty(userModel.LicenseNumber) && !string.IsNullOrEmpty(userModel.EmailId) && !string.IsNullOrEmpty(userModel.FirstName)))
            {
               // userModel.BookingModel= (BookingModel)HttpContext.Items["BookingModel"];
                var confirmationModel = _customerManager.Checkout(userModel);
                
                return View("~/Views/Customer/Confirmation.cshtml", confirmationModel);
            }
            return View(userModel);

        }

    }
}