using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using Newtonsoft.Json;

namespace App1.Services
{
    public class RentService {
        private Http _http;

        public RentService() {
            this._http=new Http();
        }
        public async Task<SimpleResponse<VehicleDto>> CreateRentAsync(long carId) {
            var res = await _http.GetAsync<SimpleResponse<VehicleDto>>("Reserve/GetVehicle");
            return res;
            //var ms = new MemoryStream();
            //await res.Content.CopyToAsync(ms);
            //    var jbo = JsonConvert.DeserializeObject<RentResponse>(Encoding.UTF8.GetString(ms.GetBuffer()));
            //    return jbo;

        }
        public async Task<VehicleDto[]> GetUnReservedVehicles() {
            var res = await _http.GetAsync<SimpleResponse<VehicleDto[]>>("Reserve/GetUnReservedVehicles");
            return res.Data;
        }
        public async Task<VehicleDto> GetVehicle(long id) {
            var res = await _http.GetAsync<SimpleResponse<VehicleDto>>("Reserve/GetVehicle?Id="+id);
            return res.Data;
        }
    }

    public class RentRequest
    {
        public long CarId { get; set; }
        public Coordinates CurrentPosition { get; set; }
        public int Payment { get; set; }

    }
    public class Coordinates 
    {
        public virtual decimal Longitude { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual string Address { get; set; }
    }
}
