﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
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
        public static ObservableList<VehicleDto> ActiveVehicles;
        private IHubContext<VehicleHub> context;
        private VehicleManager _vehicleManager;
        public VehicleHub(IHubContext<VehicleHub> context) {
            this.context = context;
            this._vehicleManager=new VehicleManager();
            if (ActiveVehicles == null) ActiveVehicles = new ObservableList<VehicleDto>(_vehicleManager.GetVehicles());
           ActiveVehicles.OnAdd+=ActiveVehiclesOnOnAdd;
            ActiveVehicles.OnRemove+=ActiveVehiclesOnOnRemove;
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
            Clients.Caller.SendAsync("onInitialize", ActiveVehicles);
            return base.OnConnectedAsync();
        }
    }
}
