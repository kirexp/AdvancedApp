using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services {
    public interface IPhone {
        Task Call(string phoneNumber);
    }
}
