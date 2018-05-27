using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using App1.Droid;
using App1.Extenssions;
using App1.ViewModels;
using Newtonsoft.Json;

namespace App1.Services {
    public class RentService {
        private Http _http;

        public RentService() {
            this._http = new Http();
        }
        public async Task<SimpleResponse<int>> CreateRentAsync(VehicleDto model) {

            var lp = new LocationProvider();
            var position = await lp.GetGps();
            
            //var addresses = await lp.GetAddressByPosition(position);
            //var address = addresses.FirstOrDefault();

            var data = new {
                CarId = model.Id,
                CurrentPosition = new {
                    Longitude = position?.Longitude ?? 0.0D,
                    Latitude = position?.Latitude ?? 0.0D,
                    Address = "ул. Жубанова 8"
                },
                Payment = model.Cost,
                DestinationPoint = new {
                    Longitude = model.X,
                    Latitude = model.Y,
                    Address = ""
                }
            };
            var res = await _http.PostAsJson<SimpleResponse<int>>("Reserve/CreateRent", data);
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
