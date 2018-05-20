using System;
using System.Collections.Generic;
using System.Text;

namespace App1.ApiDTO {
    public class VehicleDto
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Brand { get; set; }
        public string Class { get; set; }
        public int Cost { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}
