using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace App1.Models
{
    public class UserDto
    {
        public long Id { get; set; }
        [JsonProperty(PropertyName = "unique_name")]
        public string  UserName{ get; set; }
        public string[] role { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        //public DateTime exp { get; set; }
}
}
