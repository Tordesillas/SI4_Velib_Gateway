using System;
using System.ServiceModel;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcServiceCallbackSink objsink = new CalcServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);

            CalcServiceReference.CalcServiceClient objClient = new CalcServiceReference.CalcServiceClient(iCntxt);
            /*objClient.SubscribeCalculatedEvent();
            objClient.SubscribeCalculationFinishedEvent();

            double dblNum1 = 1000, dblNum2 = 2000;
            objClient.Calculate(0, dblNum1, dblNum2);

            dblNum1 = 2000; dblNum2 = 4000;
            objClient.Calculate(1, dblNum1, dblNum2);

            dblNum1 = 2000; dblNum2 = 4000;
            objClient.Calculate(2, dblNum1, dblNum2);

            dblNum1 = 2000; dblNum2 = 400;
            objClient.Calculate(3, dblNum1, dblNum2);*/

            objClient.SubscribeIncrEvent();
            objClient.SubscribeDecrEvent();
            objClient.SubscribeUpdateFinishedEvent();

            objClient.Incr();
            objClient.Incr();
            objClient.Decr();

            Console.WriteLine("Press any key to close ...");
            Console.ReadKey();
        }
    }
}
