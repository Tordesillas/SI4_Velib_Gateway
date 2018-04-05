using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsole
{
    class ServiceCallbackSink : ServiceReference.IService1Callback
    {
        public void GetStation(string station, int bikes)
        {
            Console.WriteLine(station + " - " + bikes);
        }
    }
}
