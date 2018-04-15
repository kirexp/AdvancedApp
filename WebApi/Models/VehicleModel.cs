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
using WebApi.Controllers;

namespace WebApi.Models
{


    public class VehicleDto {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Brand { get; set; }
        public string Class { get; set; }
        public int Cost { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}
