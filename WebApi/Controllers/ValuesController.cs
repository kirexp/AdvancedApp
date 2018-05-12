using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Hub;
using WebApi.Signletone;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController :Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get() {
            var vehManager = new VehicleManager();
            long rentId = 0;
            //vehManager.CreateRent(new RentRequest {
            //    CarId = 3,
            //    CurrentPosition = new Coordinates {
            //        Longitude = 51.128433m,
            //        Latitude = 71.430546m,
            //        Address = "ТУТ"
            //    },
            //    DestinationPoint = "Алматы",
            //    Payment = 2000
            //}, out rentId);
            return new string[] { "value1", rentId.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            var vehicleManager = new VehicleManager();
            var toDelete = VehicleHub.ActiveVehicles[id];
            VehicleHub.ActiveVehicles.Remove(toDelete);
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
