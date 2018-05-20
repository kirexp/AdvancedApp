using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using Newtonsoft.Json;

namespace App1.Services
{
    public class CabinetService {
        public async Task<RentViewModel> GetRent() {
            var http= new Http();
            return await http.GetAsync<RentViewModel>("/Cabinet/GetLastRent");
        }

        public async Task<SummaryViewModel> GetSummary() {
            var http = new Http();
            return await http.GetAsync<SummaryViewModel>("/Cabinet/GetSummary");
        }
    }
    public class RentViewModel
    {
        public string StartingPoint { get; set; }
        public string DestinationPoint { get; set; }
        public int WayLength { get; set; }
        public int Payment { get; set; }
    }

    public class RentResponse : SimpleResponse<RentViewModel> {

    }
    public class SummaryViewModel
    {
        public int LongestRentWay { get; set; }
        public TimeSpan LongestRentTime { get; set; }
        public int SummaryLength { get; set; }
        public TimeSpan SummaryTime { get; set; }
        public int Freezed { get; set; }
        public int Payment { get; set; }
    }
}
