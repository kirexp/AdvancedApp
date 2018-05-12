using System;
using System.Collections.Generic;
using DAL.Entities.Account;
using Enums;

namespace DAL.Entities
{
    public class Rent : IEntity {
        public virtual long Id { get; set; }
        public virtual string StartPoint { get; set; }
        public virtual string DestinationPoint { get; set; }
        public virtual DateTime RentDate{ get; set; }
        public virtual Vehicle Vehicle{ get; set; }
        public virtual int Payment{ get; set; }
        public virtual User Tenant { get; set; }
        public virtual RentStatus Status { get; set; }
        public virtual int  WayLength { get; set; }
        public virtual DateTime RentStartTime { get; set; }
        public virtual DateTime? RentEndTime{ get; set; }
    }
    public class Vehicle:IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Number { get; set; }
        public virtual string Class { get; set; }
        public virtual string Brand { get; set; }
        public virtual int CostPerMile { get; set; }
        public virtual VehicleState State { get; set; }

    }
    public class VehicleState:IEntity {
        public virtual long Id { get; set; }
        public virtual VehicleRentStatus Status { get; set; }
        public virtual Coordinates CurrentPosition { get; set; }
        public virtual Vehicle Car { get; set; }
    }
    public class Coordinates:IEntity {
        public virtual long Id { get; set; }
        public virtual decimal Longitude { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual string Address { get; set; }
        public virtual VehicleState State { get; set; }
    }
    public class VehicleHistory:IEntity {
        public virtual long Id { get; set; }
        public virtual DateTime RentDateTime { get; set; }
        public virtual User Tenant { get; set; }
    }
}
