﻿using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace App1.Extenssions
{
    public class SettingsManager {
        private static SettingsManager _instance;
        private SettingsManager() {

        }
        static SettingsManager() {
            _instance=new SettingsManager();
        }
        public static SettingsManager Instance => _instance;
        private static ISettings AppSettings => CrossSettings.Current;
        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;
        private const string IsAuthKey = "is_auth";
        private const string JWTKey = "jwt_key";
        private const string HasActiveRentKey = "has_rent";
        private const string RentIdKey = "rent_Id";
        public string AuthToken
        {
            get { return AppSettings.GetValueOrDefault(JWTKey, ""); }
            set { AppSettings.AddOrUpdateValue(JWTKey, value); }
        }
        public bool IsAuthentithicated {
            get { return AppSettings.GetValueOrDefault(IsAuthKey, false); }
            set { AppSettings.AddOrUpdateValue(IsAuthKey, value); }
        }
        public string UserName {
            get { return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue(UserNameKey, value); }
        }
        public bool HasRent
        {
            get { return AppSettings.GetValueOrDefault(HasActiveRentKey, false); }
            set { AppSettings.AddOrUpdateValue(HasActiveRentKey, value); }
        }
        public int RentId
        {
            get { return AppSettings.GetValueOrDefault(RentIdKey, 0); }
            set { AppSettings.AddOrUpdateValue(RentIdKey, value); }
        }
        public DateTime Expires { get; set; }
    }
}
