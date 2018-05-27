using System;
using System.Threading.Tasks;
using App1.ApiDTO;

namespace App1.Services {
    public class CabinetService {
        public async Task<RentViewModel> GetRent() {
            var http = new Http();
            var response = await http.GetAsync<SimpleResponse<RentViewModel>>("/Cabinet/GetLastRent");
            return response.Data;
        }

        public async Task<SummaryViewModel> GetSummary() {
            var http = new Http();
            var response = await http.GetAsync<SimpleResponse<SummaryViewModel>>("/Cabinet/GetSummary");
            return response.Data;
        }
    }
    public class RentViewModel {
        public string StartingPoint { get; set; }
        public string DestinationPoint { get; set; }
        public int WayLength { get; set; }
        public int Payment { get; set; }
    }

    public class RentResponse : SimpleResponse<RentViewModel> {

    }
    public class SummaryViewModel {
        public int LongestRentWay { get; set; }
        public TimeSpan LongestRentTime { get; set; }
        public int SummaryLength { get; set; }
        public TimeSpan SummaryTime { get; set; }
        public int Freezed { get; set; }
        public int Payment { get; set; }
    }
}
