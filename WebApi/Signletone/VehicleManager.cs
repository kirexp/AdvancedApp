using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Entities.Account;
using DAL.Repositories;
using Enums;
using Microsoft.AspNet.Identity;
using WebApi.Auth;
using WebApi.Controllers;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Signletone
{
    public class VehicleManager {
        public VehicleDto Find(long id)
        {
            using (var repository = new Repository<Vehicle>())
            {
                var vehicle = repository.Get(id);
                var vehicleDto = new VehicleDto();
                vehicleDto.Id = id;
                vehicleDto.Brand = vehicle.Brand;
                vehicleDto.Class = vehicle.Class;
                vehicleDto.Cost = vehicle.CostPerMile;
                vehicleDto.X = vehicle.State.CurrentPosition.Latitude;
                vehicleDto.Y = vehicle.State.CurrentPosition.Longitude;
                return vehicleDto;
            }
        }
        public bool CreateRent(RentRequest requestData,out long rentId) {
            rentId = 0;
            using (var repository = new Repository<Rent>())
            {
                var userId = Thread.CurrentPrincipal.GetId();
                var vehicle = repository.Load<Vehicle>(requestData.CarId);
                if (vehicle == null) return false;
                var rent = new Rent
                {
                    Tenant = repository.Load<User>(userId),
                    EndPoint = requestData.DestinationPoint,
                    Payment = requestData.Payment,
                    RentDate = DateTime.Now,
                    RentStartTime = DateTime.Now,
                    StartPoint = requestData.CurrentPosition,
                    Vehicle = vehicle,
                    Status = RentStatus.Active,
                };
                repository.Insert(rent);
                repository.Commit();
                rentId = rent.Id;
                return true;
            }
        }
        public IList<VehicleDto> GetVehicles()
        {
            using (var repository = new Repository<Vehicle>())
            {
                var freeCars = repository.Get(x => x.State.Status == VehicleRentStatus.Free).Select(x =>
                    new VehicleDto
                    {
                        Id = x.Id,
                        Number = x.Number,
                        Brand = x.Brand,
                        Class = x.Class,
                        Cost = x.CostPerMile,
                        X = x.State.CurrentPosition.Latitude,
                        Y = x.State.CurrentPosition.Longitude
                    }).ToList();
                return freeCars;
            }
        }
        public bool UndoRent(RentUndoRequest requestData, out VehicleDto vehicle) {
            vehicle = null;
            using (var repository = new Repository<Rent>())
            {
                var userId = Thread.CurrentPrincipal.GetId();
                var rent= repository.Get(requestData.RendId);
                if (rent == null) return false;
                rent.Status = RentStatus.InActive;
                rent.RentEndTime=DateTime.Now;
                rent.Vehicle.State.CurrentPosition.Latitude = requestData.CurrentPosition.Latitude;
                rent.Vehicle.State.CurrentPosition.Longitude = requestData.CurrentPosition.Longitude;
                repository.Update(rent);
                repository.Commit();
                vehicle = new VehicleDto {
                    Id = rent.Vehicle.Id,
                    Class = rent.Vehicle.Class,
                    Brand = rent.Vehicle.Brand,
                    Cost = rent.Vehicle.CostPerMile,
                    Number = rent.Vehicle.Number,
                    X = rent.Vehicle.State.CurrentPosition.Latitude,
                    Y = rent.Vehicle.State.CurrentPosition.Longitude
                };
                return true;
            }
        }
    }

    public  class VehicleManager2 {
        private static VehicleManager2 _instance;
        public  ObservableList<VehicleDto> ActiveVehicles;
        private VehicleManager2() {
            this.ActiveVehicles= new ObservableList<VehicleDto>(this.GetVehicles());
        }

        public static VehicleManager2 GetInstance() {
            return _instance ?? (_instance = new VehicleManager2());
        }

        public  VehicleDto Find(long id) {
            using (var repository = new Repository<Vehicle>()) {
                var vehicle = repository.Get(id);
                var vehicleDto = new VehicleDto();
                vehicleDto.Id = id;
                vehicleDto.Brand = vehicle.Brand;
                vehicleDto.Class = vehicle.Class;
                vehicleDto.Cost = vehicle.CostPerMile;
                vehicleDto.X = vehicle.State.CurrentPosition.Latitude;
                vehicleDto.Y = vehicle.State.CurrentPosition.Longitude;
                return vehicleDto;
            }
        }
        public  bool CreateRent(RentRequest requestData, out long rentId) {
            rentId = 0;
            using (var repository = new Repository<Rent>()) {
                var userId = Thread.CurrentPrincipal.GetId();
                var vehicle = repository.Load<Vehicle>(requestData.CarId);
                if (vehicle == null) return false;
                var rent = new Rent {
                    Tenant = repository.Load<User>(userId),
                    EndPoint = requestData.DestinationPoint,
                    Payment = requestData.Payment,
                    RentDate = DateTime.Now,
                    RentStartTime = DateTime.Now,
                    StartPoint = requestData.CurrentPosition,
                    Vehicle = vehicle,
                    Status = RentStatus.Active
                };
                repository.Insert(rent);
                repository.Commit();
                rentId = rent.Id;
                return true;
            }
        }
        public  IList<VehicleDto> GetVehicles() {
            using (var repository = new Repository<Vehicle>()) {
                var freeCars = repository.Get(x => x.State.Status == VehicleRentStatus.Free).Select(x =>
                    new VehicleDto {
                        Id = x.Id,
                        Number = x.Number,
                        Brand = x.Brand,
                        Class = x.Class,
                        Cost = x.CostPerMile,
                        X = x.State.CurrentPosition.Latitude,
                        Y = x.State.CurrentPosition.Longitude
                    }).ToList();
                return freeCars;
            }
        }
        public  bool UndoRent(RentUndoRequest requestData, out VehicleDto vehicle) {
            vehicle = null;
            using (var repository = new Repository<Rent>()) {
                var userId = Thread.CurrentPrincipal.Identity;
                var rent = repository.Get(requestData.RendId);
                if (rent == null) return false;
                rent.Status = RentStatus.InActive;
                rent.RentEndTime = DateTime.Now;
                rent.Vehicle.State.CurrentPosition.Latitude = requestData.CurrentPosition.Latitude;
                rent.Vehicle.State.CurrentPosition.Longitude = requestData.CurrentPosition.Longitude;
                repository.Update(rent);
                repository.Commit();
                vehicle = new VehicleDto {
                    Id = rent.Vehicle.Id,
                    Class = rent.Vehicle.Class,
                    Brand = rent.Vehicle.Brand,
                    Cost = rent.Vehicle.CostPerMile,
                    Number = rent.Vehicle.Number,
                    X = rent.Vehicle.State.CurrentPosition.Latitude,
                    Y = rent.Vehicle.State.CurrentPosition.Longitude
                };
                return true;
            }
        }
    }
}
