using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Entities.Account;
using DAL.Repositories;
using Enums;
using Microsoft.AspNet.Identity;
using Permission = DAL.Entities.Account.Permission;
namespace DAL {
    public class DbInitializer {
        public  void Seed() {
            using (var repository = new Repository<IEntity>()) {           
                this.InitUsers(repository);
                this.InitCars(repository);
                repository.Commit();
            }
        }

        private void InitUsers(Repository rep) {
            var hasher = new PasswordHasher();
            var permissionsRepository = new Repository<Entities.Account.Permission>(rep);
            var userRep = new Repository<User>(rep);
            if (!userRep.Any()) {
                var rolesRepository = new Repository<Entities.Account.Role>(rep);
                var administrationPanelView = new Permission {
                    Name = Constants.Permission.AdministrationPanelView,
                    NameRu = "Панели администрирования. Просмотр",
                };
                permissionsRepository.Insert(administrationPanelView);
                var editEmployeePermission = new Permission {
                    Name = Constants.Permission.AdministrationPanelEmployeeEdit,
                    NameRu = "Пользователи. Создание/редактирование",
                };
                permissionsRepository.Insert(editEmployeePermission);
                var roleViewPermission = new Permission {
                    Name = Constants.Permission.AdministrationRoleView,
                    NameRu = "Роли. Просмотр",
                };
                permissionsRepository.Insert(roleViewPermission);
                var roleEditPerm = new Permission {
                    Name = Constants.Permission.AdministrationRoleEdit,
                    NameRu = "Роли. Создание/редактирование",
                };
                var statisticsView = new Permission {
                    Name = Constants.Permission.StatisticsView,
                    NameRu = "Просмотр статистики",
                };
                permissionsRepository.Insert(statisticsView);
                var adminRole = new Role {
                    Description = "Admin-All-Permissions",
                    Name = "Admin",
                    Permissions = new HashSet<Permission>() {
                        administrationPanelView,
                        editEmployeePermission,
                        roleViewPermission,
                        roleEditPerm,
                        statisticsView,
                    }
                };
                rolesRepository.Insert(adminRole);
                var admin = new User {
                    Email = "testSS@gomeol.com",
                    Password = hasher.HashPassword("ASD123qwe"),
                    UserName = "111122223333",
                    UserType = UserTypeEnum.Admin,
                    Roles = new HashSet<Role>() {adminRole},
                    LastPasswordChangedDate = DateTime.Now,
                };
                userRep.Insert(admin);
            }

        }

        private void InitCars(Repository rep) {
            var carRepository = new Repository<Vehicle>(rep);
            if (!carRepository.Any()) {
                for (int i = 0; i < 10; i++) {
                    var car = new Vehicle {
                        Brand = "BMW m" + i,
                        Class = "Seedan",
                        CostPerMile = 2000 + i + 200,
                        Number = "z210" + i + "ka"
                    };
                    var state = new VehicleState {
                        //Car = car,
                        Status = VehicleRentStatus.Free
                    };
                    var coords = new Coordinates {
                        Address = "",
                        State = state,
                        Latitude = Convert.ToDecimal("50," + i),
                        Longitude = Convert.ToDecimal("30," + i)
                    };
                    state.CurrentPosition = coords;
                    car.State = state;
                    carRepository.Insert(car);
                }

                //carRepository.Insert(new Vehicle {
                //    Brand = "BMW m5",
                //    Class = "Seedan",
                //    CostPerMile = 2000,
                //    Number = "z2101ka"
                //});
                //var car1 = new Vehicle {
                //    Brand = "Mercedess v2",
                //    Class = "Seedan",
                //    CostPerMile = 2000,
                //    Number = "z2102ka",
                //};
                //var state1 = new VehicleState {
                //    Car = car1,
                //    CurrentPosition = new Coordinates {
                //        Address = "asdasd",
                //        Latitude = 75,
                //        Longitude = 70
                //    },
                //    Status = VehicleRentStatus.Free
                //};
                //car1.State = state1;
                //carRepository.Insert(car1);

                //carRepository.Insert(new Vehicle {
                //    Brand = "Toyota",
                //    Class = "Seedan",
                //    CostPerMile = 1000,
                //    Number = "z2103ka"
                //});
                //carRepository.Insert(new Vehicle {
                //    Brand = "Mazda rx9",
                //    Class = "Seedan",
                //    CostPerMile = 1300,
                //    Number = "z2104ka"
                //});
                //carRepository.Insert(new Vehicle {
                //    Brand = "Skoda l11",
                //    Class = "Seedan",
                //    CostPerMile = 2000,
                //    Number = "z2105ka"
                //});
                //carRepository.Insert(new Vehicle {
                //    Brand = "Derways f21",
                //    Class = "Seedan",
                //    CostPerMile = 2000,
                //    Number = "z2106ka"
                //});
                //carRepository.Insert(new Vehicle {
                //    Brand = "Chrysler m22",
                //    Class = "Seedan",
                //    CostPerMile = 3000,
                //    Number = "z2107ka"
                //});
                //carRepository.Insert(new Vehicle {
                //    Brand = "Chevrolet aura25",
                //    Class = "Seedan",
                //    CostPerMile = 2000,
                //    Number = "z2108ka"
                //});
            }
        }
    }
}