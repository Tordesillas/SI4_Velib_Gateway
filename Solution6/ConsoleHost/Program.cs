using System;
using System.ServiceModel;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(EventsLib.CalcService));
                host.Open();
                Console.WriteLine("Service is Hosted as http://localhost:9011/CalcService");
                Console.WriteLine("\nPress  key to stop the service.");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was en error while Hosting Service [" + e.Message + "]");
                Console.WriteLine("\nPress  key to close.");
                Console.ReadLine();
            }
        }
    }
}