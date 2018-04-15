using System.Collections.Generic;
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

            return new string[] { "value1", "value2" };
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
