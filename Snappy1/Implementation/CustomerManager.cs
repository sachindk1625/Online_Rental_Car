using Snappy1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Snappy1.Implementation
{
    public class CustomerManager
    {
        private static readonly string bookingPrefix = "BKUHCL-";
        public List<SelectListItem> GetLocation()
        {
            var listLocation = new List<SelectListItem>();
            using (var entity = new online_resEntities())
            {
                foreach (var location in entity.Location)
                {
                    listLocation.Add(new SelectListItem { Text = location.LocationName, Selected = false, Value = location.LocationName });
                }
            }
            return listLocation;
        }


        public IEnumerable<BookedCars> GetMyBooking(string username)
        {
            List<BookedCars> bookedCars = new List<BookedCars>();
            IEnumerable<CarRental> myBooking = new List<CarRental>();
            using (var entity = new online_resEntities())
            {
                myBooking = entity.CarRental.Where(c => c.Username == username).ToList();
                foreach (var c in myBooking)
                {
                    var car = entity.Car.Where(a => a.CarId == c.CarId).FirstOrDefault();
                    bookedCars.Add(new BookedCars()
                    {
                        BookingId = c.RentalId,
                        BookingEndtDate = c.BookingEnddate,
                        BookingStartDate = c.BookingStartdate,
                        CarBrand = car.Brand,
                        CarColor = car.Color,
                        CarModel = car.Model,
                        CategoryType = car.CategoryType,
                        ProductionYear = car.ProductionYear
                    });
                }
            }
            return bookedCars;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<CarAvailabiltyModel> GetCarsAvailable(BookingModel model)
        {
            var list = new List<CarAvailabiltyModel>();

            using (var entity = new online_resEntities())
            {
                var carsBookedForOverlap = from r in entity.CarRental
                                           where !(r.BookingStartdate < model.EndDate && model.StartDate < r.BookingStartdate)
                                           select r.CarId;
                var carsAvailable = (from c in entity.Car
                                     where !(carsBookedForOverlap.Contains(c.CarId))
                                     select c.CategoryType).Distinct();
                foreach (var category in entity.Category)
                {
                    if (carsAvailable.Any(i => i.ToLower().Equals(category.CategoryType.ToLower())))
                    {
                        var amountPerHour = (double)category.Amount / 24;
                        var totalTimeNeeded = (model.EndDate.Subtract(model.StartDate)).TotalHours;
                        var totalAmout = totalTimeNeeded * amountPerHour;
                        list.Add(new CarAvailabiltyModel { BookingModel = model, Name = category.CategoryType, AmountPerDay = Convert.ToDouble(category.Amount.ToString("0.00")), AmountTotal = Convert.ToDouble(totalAmout.ToString("0.00")), ImagePath = category.CategoryType + ".png" });
                    }

                }
            }

            return list;
        }
        public ConfirmationModel Checkout(UserInfoModel model)
        {
            int carId = 0;
            string rentalId = "";
            ConfirmationModel confirmationModel = null;
            // bool IsBookingConfirmed = false;
            using (var entity = new online_resEntities())
            {
                var carsBookedForOverlap = from r in entity.CarRental
                                           where !(r.BookingStartdate <= model.BookingModel.EndDate && model.BookingModel.StartDate <= r.BookingStartdate)
                                           select r.CarId;
                //carId = entity.Car.FirstOrDefault(i => i.CategoryType.ToLower() == model.CarAvailabiltyModel.Name.ToLower()).CarId;
                carId = entity.Car.Where(o => !carsBookedForOverlap.Contains(o.CarId)).FirstOrDefault(i => i.CategoryType.ToLower() == model.CarAvailabiltyModel.Name.ToLower()).CarId;


                //carId = (from car in entity.Car
                //         where car.CategoryType.ToLower() == model.CarAvailabiltyModel.Name.ToLower() && !carsBookedForOverlap.Contains(car.CarId)
                //         select car.CarId).FirstOrDefault();

                rentalId = GetNewRentalId(carId);
                //check if user exist or else create it

                var user = entity.Customer.FirstOrDefault(o => o.Username.ToLower() == model.EmailId);
                if (user == null)
                {
                    var newCustomer = new Customer
                    {
                        Username = model.EmailId,
                        Dob = DateTime.Now.AddYears(-23),
                        Firstname = model.FirstName,
                        Lastname = model.LastName,
                        License = model.LicenseNumber
                    };
                    entity.Customer.Add(newCustomer);
                    entity.SaveChanges();
                }


                var newRental = new CarRental
                {
                    //RentalId = Convert.ToInt32(rentalId),
                    CarId = carId,
                    BookingEnddate = model.BookingModel.EndDate,
                    BookingStartdate = model.BookingModel.StartDate,
                    LocationName = model.BookingModel.Location,
                    Username = model.EmailId,
                    Dropoff = "empty",
                    Pickup = "empty",

                };
                entity.CarRental.Add(newRental);
                entity.SaveChanges();
                confirmationModel = new ConfirmationModel { UserModel = model, BookingNumber = bookingPrefix + rentalId, IsBookingConfirmed = true };
            }

            return confirmationModel;
        }

        private string GetNewRentalId(int carId)
        {
            return carId.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
        }
    }
}