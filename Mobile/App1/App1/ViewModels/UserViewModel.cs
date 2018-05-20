using System;
using System.Collections.Generic;
using System.Text;
using App1.Extenssions;

namespace App1.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public UserViewModel() {
            if(SettingsManager.Instance.IsAuthentithicated)
            this.UserName = SettingsManager.Instance.UserName;
        }
    }
}
