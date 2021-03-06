﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace WebApi.Models
{
    public class RentViewModel
    {
        public long Id { get; set; }
        public Coordinates StartCoordinates { get; set; }
        public Coordinates EndCoordinates { get; set; }
        public string StartingPoint {
            get {
                if (this.StartCoordinates != null)
                    return this.StartCoordinates.Address;
                return "EMPTY";
            }
        }
        public string DestinationPoint {
            get {
                if (this.EndCoordinates != null)
                    return this.EndCoordinates.Address;
                return "EMPTY";
            }
        }
        public int WayLength { get; set; }
        public int Payment { get; set; }

    }
    public class SummaryViewModel
    {
        public int LongestRentWay { get; set; }
        public TimeSpan LongestRentTime { get; set; }
        public int SummaryLength { get; set; }
        public TimeSpan SummaryTime { get; set; }
        public int Freezed { get; set; }
        public int Payment { get; set; }
        public void GetSummary(string userName) {
            using (var repository = new Repository<Rent>()) {
                var summary = repository.Get(x => x.Tenant.UserName == userName);
                var z = summary.Select(x => new My {Start = x.RentStartTime, End = x.RentEndTime}).ToList();
                this.LongestRentTime = this.Longest(z);
                if (z.Count > 0) {
                    this.SummaryLength = summary.Sum(x => x.WayLength);
                    this.LongestRentWay = summary.Max(x => x.WayLength);
                }

                this.SummaryLength = 0;
                this.LongestRentWay = 0;
                this.SummaryTime = this.Sum(z);
            }
        }

        private TimeSpan Longest(List<My> li) {
            TimeSpan longest = TimeSpan.Zero;
            foreach (var my in li) {
                if (my.End.HasValue) {
                    var substruction = my.End.Value.Subtract(my.Start);
                    if (substruction > longest) longest = substruction;
                }
            }
            return longest;
        }
        private TimeSpan Sum(List<My> li)
        {
            TimeSpan longest = TimeSpan.Zero;
            foreach (var my in li) {
                if (my.End.HasValue) {
                    var substruction = my.End.Value.Subtract(my.Start);
                    longest+= substruction;
                }
            }
            return longest;
        }
        public class My {
            public DateTime Start { get; set; }
            public DateTime? End { get; set; }
        }
    }
}
