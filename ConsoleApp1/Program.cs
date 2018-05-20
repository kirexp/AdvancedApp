using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args) {
            var z = new Thread(() => {
                
                Console.WriteLine(DateTime.Now);
            });
            z.
            z.Start();
            Console.ReadLine();
        }
    }
}
