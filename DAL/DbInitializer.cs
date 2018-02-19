using System;
using System.Collections.Generic;
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
                repository.Commit();
            }
        }

        private void InitUsers(Repository rep) {
            var profileRepository = new Repository<Profile>(rep);
            var hasher = new PasswordHasher();
            var permissionsRepository = new Repository<Entities.Account.Permission>(rep);
            var userRep = new Repository<User>(rep);
            if (!userRep.Any()) {


                var rolesRepository = new Repository<Entities.Account.Role>(rep);
                var administrationPanelView = new Permission {
                    Name = Constants.Permission.AdministrationPanelView,
                    NameRu = "Панели администрирования. Просмотр",
                    NameKz = "Панели администрирования. Просмотр",
                };
                permissionsRepository.Insert(administrationPanelView);
                var editEmployeePermission = new Permission {
                    Name = Constants.Permission.AdministrationPanelEmployeeEdit,
                    NameRu = "Пользователи. Создание/редактирование",
                    NameKz = "Пользователи. Создание/редактирование",
                };
                permissionsRepository.Insert(editEmployeePermission);
                var roleViewPermission = new Permission {
                    Name = Constants.Permission.AdministrationRoleView,
                    NameRu = "Роли. Просмотр",
                    NameKz = "Роли. Просмотр",
                };
                permissionsRepository.Insert(roleViewPermission);
                var roleEditPerm = new Permission {
                    Name = Constants.Permission.AdministrationRoleEdit,
                    NameRu = "Роли. Создание/редактирование",
                    NameKz = "Роли. Создание/редактирование",
                };
                permissionsRepository.Insert(roleEditPerm);
                var privilegedView = new Permission {
                    Name = Constants.Permission.PrivilegedView,
                    NameRu = "База данных льготников. Просмотр",
                    NameKz = "База данных льготников. Просмотр",
                };
                permissionsRepository.Insert(privilegedView);
                var dayCareQueueReportView = new Permission
                {
                    Name = Constants.Permission.DayCareQueueReportView,
                    NameRu = "База свободных мест(просмотр всех мест)",
                    NameKz = "База свободных мест(просмотр всех мест)",
                };
                permissionsRepository.Insert(dayCareQueueReportView);
                var privilegedEdit = new Permission {
                    Name = Constants.Permission.PrivilegedEdit,
                    NameRu = "База данных льготников. Создание/редактирование",
                    NameKz = "База данных льготников. Создание/редактирование",
                };
                permissionsRepository.Insert(privilegedEdit);
                var dayCareQueueView = new Permission {
                    Name = Constants.Permission.DayCareQueueView,
                    NameRu = "Справочники ДДО. Просмотр",
                    NameKz = "Справочники ДДО. Просмотр",
                };
                permissionsRepository.Insert(dayCareQueueView);
                var dayCareQueueEdit = new Permission {
                    Name = Constants.Permission.DayCareQueueEdit,
                    NameRu = "Справочники ДДО. Создание/редактирование",
                    NameKz = "Справочники ДДО. Создание/редактирование"
                };
                permissionsRepository.Insert(dayCareQueueEdit);
                var dayCareQueueFreePlacesView = new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesView,
                    NameRu = "База свободных мест в ДДО. Просмотр",
                    NameKz = "База свободных мест в ДДО. Просмотр"
                };
                permissionsRepository.Insert(dayCareQueueFreePlacesView);
                var dayCareQueueFreePlacesEdit = new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesEdit,
                    NameRu = "База свободных мест в ДДО. Добавление свободных мест",
                    NameKz = "База свободных мест в ДДО. Добавление свободных мест"
                };
                permissionsRepository.Insert(dayCareQueueFreePlacesEdit);
                var dayCareQueueFreePlacesListView = new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesListView,
                    NameRu = "Журнал очередей ДДО. Просмотр",
                    NameKz = "Журнал очередей ДДО. Просмотр"
                };
                permissionsRepository.Insert(dayCareQueueFreePlacesListView);
                var dayCareQueueFreePlacesSetInQueue = new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesSetInQueue,
                    NameRu = "Журнал очередей ДДО. Постановка в очередь",
                    NameKz = "Журнал очередей ДДО. Постановка в очередь"
                };
                permissionsRepository.Insert(dayCareQueueFreePlacesSetInQueue);
                var requestListView = new Permission {
                    Name = Constants.Permission.RequestListView,
                    NameRu = "Журнал заявлений. Просмотр",
                    NameKz = "Журнал заявлений. Просмотр"
                };
                permissionsRepository.Insert(requestListView);
                var requestListEdit = new Permission {
                    Name = Constants.Permission.RequestListEdit,
                    NameRu = "Журнал заявлений. Обновление данных",
                    NameKz = "Журнал заявлений. Обновление данных"
                };
                permissionsRepository.Insert(requestListEdit);
                var requestListRemovalFromQueue = new Permission {
                    Name = Constants.Permission.RequestListRemovalFromQueue,
                    NameRu = "Журнал заявлений. Снятие с очереди",
                    NameKz = "Журнал заявлений. Снятие с очереди"
                };
                permissionsRepository.Insert(requestListRemovalFromQueue);
                var requestListCheckDayCareQueue = new Permission {
                    Name = Constants.Permission.RequestListCheckDayCareQueue,
                    NameRu = "Журнал заявлений. Выбор ДДО",
                    NameKz = "Журнал заявлений. Выбор ДДО"
                };
                permissionsRepository.Insert(requestListCheckDayCareQueue);
                var archiveView = new Permission {
                    Name = Constants.Permission.ArchiveView,
                    NameRu = "Архив. Просмотр",
                    NameKz = "Архив. Просмотр",
                };
                permissionsRepository.Insert(archiveView);
                var statisticsView = new Permission {
                    Name = Constants.Permission.StatisticsView,
                    NameRu = "Просмотр статистики",
                    NameKz = "Просмотр статистики",
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
                        privilegedView,
                        privilegedEdit,
                        dayCareQueueView,
                        dayCareQueueEdit,
                        dayCareQueueFreePlacesView,
                        dayCareQueueFreePlacesEdit,
                        dayCareQueueFreePlacesListView,
                        requestListView,
                        requestListEdit,
                        requestListRemovalFromQueue,
                        requestListCheckDayCareQueue,
                        archiveView,
                        statisticsView,dayCareQueueReportView
                    }
                };
                rolesRepository.Insert(adminRole);
                //adminRole.Permissions.Add(administrationPanelView);
                //adminRole.Permissions.Add(editEmployeePermission);
                //adminRole.Permissions.Add(roleViewPermission)
                //adminRole.Permissions.Add(roleEditPerm)
                //adminRole.Permissions.Add(privilegedView)
                //adminRole.Permissions.Add(privilegedEdit)
                //adminRole.Permissions.Add(dayCareQueueView)
                //adminRole.Permissions.Add(dayCareQueueEdit)
                //adminRole.Permissions.Add(dayCareQueueFreePlacesView)
                //adminRole.Permissions.Add(dayCareQueueFreePlacesEdit)
                //adminRole.Permissions.Add(dayCareQueueFreePlacesListView)
                //adminRole.Permissions.Add(requestListView)
                //adminRole.Permissions.Add(requestListEdit)
                //adminRole.Permissions.Add(requestListRemovalFromQueue)
                //adminRole.Permissions.Add(requestListCheckDayCareQueue)
                //adminRole.Permissions.Add(archiveView)
                //adminRole.Permissions.Add(statisticsView)

                var profile = new Profile {
                    Email = "kirzero2@gmail.com",
                    BirthDate = DateTime.Now,
                    Building = "1",
                    City = "1",
                    Department = "1",
                    Organization = "1",
                    Position = "1",
                    LastName = "Admin",
                    MiddleName = "Admin",
                    FirstName = "Admin",
                    District = "1",
                    Flat = "1",
                    IIN = "111122223333",
                    Phone = "0000000000",
                    Region = "1",
                    Street = "1",
                    
                };
                profileRepository.Insert(profile);
                var admin = new User {
                    Email = "kirzero2@gmail.com",
                    Password = hasher.HashPassword("ASD123qwe"),
                    Profile = profile,
                    UserName = "111122223333",
                    UserType = UserTypeEnum.Admin,
                    Roles = new HashSet<Role>() {adminRole},
                    LastPasswordChangedDate = DateTime.Now,
                    
                };
                userRep.Insert(admin);
            }

        }
    }
}