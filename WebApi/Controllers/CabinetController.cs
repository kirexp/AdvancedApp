using System.Linq;
using System.Security.Claims;
using System.Threading;
using Common;
using DAL.Entities;
using DAL.Repositories;
using DevExtreme.AspNet.Data;
using Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.ApiFolder;
using WebApi.Auth;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CabinetController : Controller
    {
        public CabinetController() {
      
        }
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
        public IActionResult GetMyRentHistory(AspNetDevextremeDataSourceLoader options) {
            EventLogger.Info($"Запрос на GetMyRentHistory - {this.User.Identity.Name}");
            using (var repository = new Repository<Rent>()) {
                var query = repository.Get(x => x.Tenant.UserName == this.User.Identity.Name).Select(x=>new RentViewModel {
                    Id=x.Id,
                    Payment = x.Payment,
                    StartCoordinates = x.StartPoint,
                    EndCoordinates = x.EndPoint,
                    WayLength = x.WayLength
                });
                return Json(DataSourceLoader.Load(query, options));
            }
        }
        [HttpGet]
        public IActionResult GetLastRent() {
            EventLogger.Info($"Запрос на GetLastRent - {this.User.Identity.Name}");
            using (var repository = new Repository<Rent>()) {
                var lastRent = repository.Get(x => x.Tenant.UserName == this.User.Identity.Name)
                    .OrderByDescending(x => x.Id).Select(x=>new RentViewModel {
                        Payment = x.Payment,
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