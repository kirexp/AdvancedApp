using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Signletone;

namespace WebApi.Hub
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VehicleHub :  Microsoft.AspNetCore.SignalR.Hub {
        private IHubContext<VehicleHub> context;
        public VehicleHub(IHubContext<VehicleHub> context) {
            this.context = context;
            VehicleManager.GetInstance().ActiveVehicles.OnAdd += ActiveVehiclesOnOnAdd;
            VehicleManager.GetInstance().ActiveVehicles.OnRemove += ActiveVehiclesOnOnRemove;
        }
        private void ActiveVehiclesOnOnRemove(object sender, VehicleDto e) {
            this.ReserveCar(e);
        }
        private void ActiveVehiclesOnOnAdd(object sender, VehicleDto e) {
            this.UnlockCar(e);
        }
        public Task UnlockCar(VehicleDto vehicle){
            return this.Clients.All.SendAsync("onUnlocked", vehicle);
        }
        public Task ReserveCar(VehicleDto vehicle) {


            return this.context.Clients.All.SendAsync("onReserved", vehicle.Id);
        }
        public override Task OnConnectedAsync() {
            EventLogger.Info("UserConnected"+DateTime.Now.ToString());
            Clients.Caller.SendAsync("onInitialize", VehicleManager.GetInstance().ActiveVehicles);
            return base.OnConnectedAsync();
        }
    }
}
