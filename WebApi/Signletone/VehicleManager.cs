using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
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
    //public class VehicleManager {
    //    public VehicleDto Find(long id)
    //    {
    //        using (var repository = new Repository<Vehicle>())
    //        {
    //            var vehicle = repository.Get(id);
    //            var vehicleDto = new VehicleDto();
    //            vehicleDto.Id = id;
    //            vehicleDto.Brand = vehicle.Brand;
    //            vehicleDto.Class = vehicle.Class;
    //            vehicleDto.Cost = vehicle.CostPerMile;
    //            vehicleDto.X = vehicle.State.CurrentPosition.Latitude;
    //            vehicleDto.Y = vehicle.State.CurrentPosition.Longitude;
    //            return vehicleDto;
    //        }
    //    }
    //    public bool CreateRent(RentRequest requestData,out long rentId) {
    //        rentId = 0;
    //        using (var repository = new Repository<Rent>())
    //        {
    //            var userId = Thread.CurrentPrincipal.GetId();
    //            var vehicle = repository.Load<Vehicle>(requestData.CarId);
    //            if (vehicle == null) return false;
    //            var rent = new Rent
    //            {
    //                Tenant = repository.Load<User>(userId),
    //                EndPoint = requestData.DestinationPoint,
    //                Payment = requestData.Payment,
    //                RentDate = DateTime.Now,
    //                RentStartTime = DateTime.Now,
    //                StartPoint = requestData.CurrentPosition,
    //                Vehicle = vehicle,
    //                Status = RentStatus.Active,
    //            };
    //            repository.Insert(rent);
    //            repository.Commit();
    //            rentId = rent.Id;
    //            return true;
    //        }
    //    }
    //    public IList<VehicleDto> GetVehicles()
    //    {
    //        using (var repository = new Repository<Vehicle>())
    //        {
    //            var freeCars = repository.Get(x => x.State.Status == VehicleRentStatus.Free).Select(x =>
    //                new VehicleDto
    //                {
    //                    Id = x.Id,
    //                    Number = x.Number,
    //                    Brand = x.Brand,
    //                    Class = x.Class,
    //                    Cost = x.CostPerMile,
    //                    X = x.State.CurrentPosition.Latitude,
    //                    Y = x.State.CurrentPosition.Longitude
    //                }).ToList();
    //            return freeCars;
    //        }
    //    }
    //    public bool UndoRent(RentUndoRequest requestData, out VehicleDto vehicle) {
    //        vehicle = null;
    //        using (var repository = new Repository<Rent>())
    //        {
    //            var userId = Thread.CurrentPrincipal.GetId();
    //            var rent= repository.Get(requestData.RendId);
    //            if (rent == null) return false;
    //            rent.Status = RentStatus.InActive;
    //            rent.RentEndTime=DateTime.Now;
    //            rent.Vehicle.State.CurrentPosition.Latitude = requestData.CurrentPosition.Latitude;
    //            rent.Vehicle.State.CurrentPosition.Longitude = requestData.CurrentPosition.Longitude;
    //            repository.Update(rent);
    //            repository.Commit();
    //            vehicle = new VehicleDto {
    //                Id = rent.Vehicle.Id,
    //                Class = rent.Vehicle.Class,
    //                Brand = rent.Vehicle.Brand,
    //                Cost = rent.Vehicle.CostPerMile,
    //                Number = rent.Vehicle.Number,
    //                X = rent.Vehicle.State.CurrentPosition.Latitude,
    //                Y = rent.Vehicle.State.CurrentPosition.Longitude
    //            };
    //            return true;
    //        }
    //    }
    //}

    public  class VehicleManager {
        private static VehicleManager _instance;
        public  ObservableList<VehicleDto> ActiveVehicles;
        private VehicleManager() {
            this.ActiveVehicles= new ObservableList<VehicleDto>(this.GetVehicles());
        }
        public static VehicleManager GetInstance() {
            return _instance ?? (_instance = new VehicleManager());
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

        public VehicleDto CurrentRent(long id) {
            using (var repository = new Repository<Rent>()) {
                var rent = repository.Get(id);
                var vehicleDto = new VehicleDto();
                vehicleDto.Id = id;
                vehicleDto.Brand = rent.Vehicle.Brand;
                vehicleDto.Class = rent.Vehicle.Class;
                vehicleDto.Cost = rent.Vehicle.CostPerMile;
                vehicleDto.X = rent.Vehicle.State.CurrentPosition.Latitude;
                vehicleDto.Y = rent.Vehicle.State.CurrentPosition.Longitude;
                return vehicleDto;
            }
        }
        public  bool CreateRent(RentRequest requestData, long userId, out long rentId) {
            try {
                rentId = 0;
                using (var repository = new Repository<Rent>()) {
                    var vehicleRep = new Repository<Vehicle>(repository);
                    var vehicle = repository.Load<Vehicle>(requestData.CarId);
                    if (vehicle == null) return false;
                    vehicle.State.Status = VehicleRentStatus.Reserved;
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
                    vehicleRep.Update(vehicle);
                    repository.Insert(rent);
                    repository.Commit();
                    rentId = rent.Id;
                    return true;
                }
            }
            catch (Exception ex) {
                GenLogger.Fatal($"CreateRent - {Thread.CurrentPrincipal.GetId()}",ex);
                rentId = 0;
                return false;
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
        public  bool UndoRent(RentUndoRequest requestData, long userId) {
            try {
                using (var repository = new Repository<Rent>()) {
                    var rent = repository.Get(requestData.RendId);
                    if (rent == null) return false;
                    rent.Status = RentStatus.InActive;
                    rent.RentEndTime = DateTime.Now;
                    rent.Vehicle.State.CurrentPosition.Latitude = requestData.CurrentPosition.Latitude;
                    rent.Vehicle.State.CurrentPosition.Longitude = requestData.CurrentPosition.Longitude;
                    rent.Vehicle.State.Status = VehicleRentStatus.Free;
                    repository.Update(rent);
                    repository.Commit();
                    this.ActiveVehicles.Add(new VehicleDto {
                        Id = rent.Vehicle.Id,
                        Brand = rent.Vehicle.Brand,
                        Class = rent.Vehicle.Brand,
                        Number = rent.Vehicle.Number,
                        Cost = rent.Vehicle.CostPerMile,
                        X = rent.Vehicle.State.CurrentPosition.Latitude,
                        Y = rent.Vehicle.State.CurrentPosition.Longitude
                    });
                    return true;
                }
            }
            catch (Exception ex) {
                GenLogger.Fatal($"UndoRent - { Thread.CurrentPrincipal.Identity.GetUserId()}",ex);
                return false;
            }

        }
        public bool FinishRent(FinishRentRequest requestData, long userId) {
            try {
                using (var repository = new Repository<Rent>()) {
                    var currentRent = repository.Get(requestData.RentId);
                    currentRent.RentEndTime=DateTime.Now;
                    currentRent.Status = RentStatus.Finished;
                    currentRent.EndPoint = requestData.CurrentPosition;
                    currentRent.Vehicle.State.CurrentPosition.Latitude = requestData.CurrentPosition.Latitude;
                    currentRent.Vehicle.State.CurrentPosition.Longitude = requestData.CurrentPosition.Longitude;
                    currentRent.Vehicle.State.Status =VehicleRentStatus.Free;
                    repository.Update(currentRent);
                    repository.Commit();
                    this.ActiveVehicles.Add(new VehicleDto
                    {
                        Id = currentRent.Vehicle.Id,
                        Brand = currentRent.Vehicle.Brand,
                        Class = currentRent.Vehicle.Brand,
                        Number = currentRent.Vehicle.Number,
                        Cost = currentRent.Vehicle.CostPerMile,
                        X = currentRent.Vehicle.State.CurrentPosition.Latitude,
                        Y = currentRent.Vehicle.State.CurrentPosition.Longitude
                    });
                    return true;
                }
            }
            catch (Exception ex) {
                EventLogger.Error($"Запрос на GetLastRent FinishRent завершился неудачей - ",ex);
                return false;
            }
        }
    }
}
