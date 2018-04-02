using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace WebApi.Models
{
    public class VehicleModel
    {
        public long Id { get; set; }

        public Vehicle Get(long id) {
            using (var repository = new Repository<Vehicle>()) {
                return repository.Get(id);
            }
        }
    }
}
