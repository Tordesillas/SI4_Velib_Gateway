using System;

namespace ConsoleClient
{
    class CalcServiceCallbackSink : CalcServiceReference.ICalcServiceCallback
    {
        /*public void Calculated(int nOp, double dblNum1, double dblNum2, double dblResult)
        {
            switch (nOp)
            {
                case 0: Console.WriteLine("\nOperation : Addition"); break;
                case 1: Console.WriteLine("\nOperation : Subtraction"); break;
                case 2: Console.WriteLine("\nOperation : Multiplication"); break;
                case 3: Console.WriteLine("\nOperation : Division"); break;
            }
            Console.WriteLine("Operand 1 ...: {0}", dblNum1);
            Console.WriteLine("Operand 2 ...: {0}", dblNum2);
            Console.WriteLine("Result ......: {0}", dblResult);
        }

        public void CalculationFinished()
        {
            Console.WriteLine("Calculation completed");
        }*/
        public void DecrOp(int res)
        {
            Console.WriteLine("Operation : Decr");
            Console.WriteLine("Result : {0}", res);
        }

        public void IncrOp(int res)
        {
            Console.WriteLine("Operation : Incr");
            Console.WriteLine("Result : {0}", res);
        }

        public void UpdateFinished()
        {
            Console.WriteLine("Counter updated");
        }
    }
}
