using System.Linq;
using Common;
using DAL.Entities;
using DAL.Repositories;
using Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CabinetController : Controller
    {
        public IActionResult GetCurrentRent() {
            using (var repository = new Repository<Rent>()) {
                var currentRent = repository
                    .Get(x => x.Tenant.UserName == this.User.Identity.Name && x.Status == RentStatus.Active)
                    .SingleOrDefault();
                if (currentRent != null) {
                    return Json(SimpleResponse.Success(currentRent));
                }
                else {
                    return Json(SimpleResponse.Error("Нет активной аренды"));
                }
            }
        }
        public IActionResult GetMyRentHistory() {
            EventLogger.Info($"Запрос на GetMyRentHistory - {this.User.Identity.Name}");
            using (var repository = new Repository<Rent>()) {
                var myRents = repository.Get(x => x.Tenant.UserName == this.User.Identity.Name);
                return Json(myRents);
            }
        }
        [HttpGet]
        public IActionResult GetLastRent() {
            EventLogger.Info($"Запрос на GetLastRent - {this.User.Identity.Name}");
            using (var repository = new Repository<Rent>()) {
                var lastRent = repository.Get(x => x.Tenant.UserName == this.User.Identity.Name)
                    .OrderByDescending(x => x.Id).Select(x=>new RentViewModel {
                        DestinationPoint = x.DestinationPoint,
                        Payment = x.Payment,
                        StartingPoint = x.StartPoint,
                        WayLength = x.WayLength
                    }).FirstOrDefault();
                return Json(SimpleResponse.Success(lastRent));
            }
        }

        public IActionResult GetSummary() {
            var summary = new SummaryViewModel();
            summary.GetSummary(this.User.Identity.Name);
            return Json(SimpleResponse.Success(summary));
        }
    }
}