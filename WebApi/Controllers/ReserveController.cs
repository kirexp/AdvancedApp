using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;
using Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ReserveController : Controller
    {
        public IActionResult GetUnReservedVehicles() {
            try {
                using (var repository = new Repository<Vehicle>()) {
                    var freeCars = repository.Get(x => x.State.Status == VehicleRentStatus.Free).Select(x => new {
                        Number = x.Number,
                        Brand = x.Brand,
                        Class = x.Class,
                        Cost = x.CostPerMile,
                        X = x.State.CurrentPosition.Latitude,
                        Y = x.State.CurrentPosition.Longitude
                    }).ToList();
                    return Json(SimpleResponse.Success(freeCars));
                }
            }
            catch (Exception ex) {
                var z = 0;
                return Json(SimpleResponse.Error("2"));
            }
        }
        [HttpGet]
        public IActionResult GetVehicle(long id) {
            var model = new VehicleModel();
            var vehicle = model.Get(id);
            if (vehicle != null)
                return Json(SimpleResponse.Success(vehicle));
                return Json(SimpleResponse.Error("Не удалось найти данный автомобиль"));
        }
    }
}