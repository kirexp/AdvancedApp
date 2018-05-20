using System;
using System.Linq;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Hub;
using WebApi.Models;
using WebApi.Signletone;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReserveController : Controller {
        private VehicleManager _manager;
        public ReserveController() {
            this._manager = new VehicleManager();
        }
        public SimpleResponse GetUnReservedVehicles() {
            try {
                var freeCars= _manager.GetVehicles();
                    return SimpleResponse.Success(freeCars);
            }
            catch (Exception ex) {
                var z = 0;
                return SimpleResponse.Error("2");
            }
        }
        [HttpGet]
        public SimpleResponse GetVehicle(long id) {
             var vehicle = _manager.Find(id);
            if (vehicle != null)
                return SimpleResponse.Success(vehicle);
                return SimpleResponse.Error("Не удалось найти данный автомобиль");
        }
        [HttpPost]
        public SimpleResponse CreateRent([FromBody]RentRequest model) {
            long rentId;
            var result = this._manager.CreateRent(model,out rentId);
            if (result) {
                var vehicleToRent = VehicleHub.ActiveVehicles.Single(x => x.Id == model.CarId);
                VehicleHub.ActiveVehicles.Remove(vehicleToRent);
                return SimpleResponse.Success(rentId);
            }
                return SimpleResponse.Error("Не удалось арендовать");
        }
        [HttpPost]
        public SimpleResponse UnReserve([FromBody] RentUndoRequest model) {
            VehicleDto vehicle;
            var result = this._manager.UndoRent(model, out vehicle);
            if (result) {
                VehicleHub.ActiveVehicles.Add(vehicle);
                return SimpleResponse.Success();
            }
            return SimpleResponse.Error("Не удалось отменить аренду");
        }
    }

    public class RentRequest {
        public long CarId { get; set; }
        public Coordinates DestinationPoint { get; set; }
        public Coordinates CurrentPosition { get; set; }
        public int Payment { get; set; }

    }
    public class RentUndoRequest
    {
        public long RendId { get; set; }
        public Coordinates CurrentPosition { get; set; }

    }
    public class CoordinatesDTO
    {
        public virtual decimal Longitude { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual string Address { get; set; }
    }

}