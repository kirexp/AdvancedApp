using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using App1.Droid;
using App1.ViewModels;
using Newtonsoft.Json;

namespace App1.Services {
    public class RentService {
        private Http _http;

        public RentService() {
            this._http = new Http();
        }
        public async Task<SimpleResponse<long>> CreateRentAsync(RentCreationViewModel model) {

            var location = MainActivity.LastKnownLocation;

            var data = new {
                CarId = model.VehicleDto.Id,
                CurrentPosition = new {
                    Longitude = location?.Longitude ?? 0.0D,
                    Latitude = location?.Latitude ?? 0.0D,
                    Address = ""
                },
                Payment = model.VehicleDto.Cost,
                DestinationPoint = new {
                    Longitude = model.VehicleDto.X,
                    Latitude = model.VehicleDto.Y,
                    Address = ""
                }
            };
            var res = await _http.PostAsJson<SimpleResponse<long>>("Reserve/GetVehicle", data);
            return res;

        }
        public async Task<VehicleDto[]> GetUnReservedVehicles() {
            var res = await _http.GetAsync<SimpleResponse<VehicleDto[]>>("Reserve/GetUnReservedVehicles");
            return res.Data;
        }
        public async Task<VehicleDto> GetVehicle(long id) {
            var res = await _http.GetAsync<SimpleResponse<VehicleDto>>("Reserve/GetVehicle?Id=" + id);
            return res.Data;
        }
    }

    public class RentRequest {
        public long CarId { get; set; }
        public Coordinates CurrentPosition { get; set; }
        public int Payment { get; set; }

    }
    public class Coordinates {
        public virtual decimal Longitude { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual string Address { get; set; }
    }
}
