using System;
using System.Linq;
using System.Threading;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auth;
using WebApi.Hub;
using WebApi.Models;
using WebApi.Signletone;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReserveController : Controller {
        public ReserveController() {
            Thread.CurrentPrincipal = this.User;
        }
        public SimpleResponse GetUnReservedVehicles() {
            try {
                var freeCars = VehicleManager.GetInstance().GetVehicles();
                    return SimpleResponse.Success(freeCars);
            }
            catch (Exception ex) {
                var z = 0;
                return SimpleResponse.Error("2");
            }
        }
        [HttpGet]
        public SimpleResponse GetVehicle(long id) {
             var vehicle = VehicleManager.GetInstance().Find(id);
            if (vehicle != null)
                return SimpleResponse.Success(vehicle);
                return SimpleResponse.Error("Не удалось найти данный автомобиль");
        }
        [HttpPost]
        public SimpleResponse CreateRent([FromBody]RentRequest model) {
            long rentId;
            var manager = VehicleManager.GetInstance();
            var result = manager.CreateRent(model,this.User.GetId(),out rentId);
            if (result) {
                var vehicleToRent = manager.ActiveVehicles.Single(x => x.Id == model.CarId);
                manager.ActiveVehicles.Remove(vehicleToRent);
                return SimpleResponse.Success(rentId);
            }
                return SimpleResponse.Error("Не удалось арендовать");
        }
        [HttpPost]
        public SimpleResponse CancelRent([FromBody] RentUndoRequest model) {
            var manager = VehicleManager.GetInstance();
            var result = manager.UndoRent(model, this.User.GetId());
            if (result) {
                return SimpleResponse.Success();
            }
            return SimpleResponse.Error("Не удалось отменить аренду");
        }

        [HttpPost]
        public SimpleResponse FinishRent([FromBody] FinishRentRequest model) {
            try {
                VehicleManager.GetInstance().FinishRent(model, this.User.GetId());
                return SimpleResponse.Success();
            }
            catch (Exception ex) {
                return SimpleResponse.Error("Не удалось завершить аренду");
            }

        }
        [HttpGet]
        public SimpleResponse<VehicleDto> GetRent(long id) {
           return SimpleResponse.Success(VehicleManager.GetInstance().CurrentRent(id));
        }
    }

    public class BaseRent {
        public long CarId { get; set; }
        public Coordinates CurrentPosition { get; set; }
    }
    public class RentRequest: BaseRent {
        public Coordinates DestinationPoint { get; set; }
        public int Payment { get; set; }

    }
    public class RentUndoRequest {
        public long RendId { get; set; }
        public Coordinates CurrentPosition { get; set; }

    }
    public class FinishRentRequest : BaseRent {
        public long RentId { get; set; }

    }
    public class CoordinatesDTO
    {
        public virtual decimal Longitude { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual string Address { get; set; }
    }

}