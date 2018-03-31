using System;
using System.ServiceModel;

namespace EventsLib
{
    public class CalcService : ICalcService
    {
        /*static Action<int, double, double, double> m_Event1 = delegate { };
        static Action m_Event2 = delegate { };

        public void SubscribeCalculatedEvent()
        {
            ICalcServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ICalcServiceEvents>();
            m_Event1 += subscriber.Calculated;
        }

        public void SubscribeCalculationFinishedEvent()
        {
            ICalcServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ICalcServiceEvents>();
            m_Event2 += subscriber.CalculationFinished;
        }

        public void Calculate(int nOp, double dblX, double dblY)
        {
            double dblResult = 0;
            switch (nOp)
            {
                case 0: dblResult = dblX + dblY; break;
                case 1: dblResult = dblX - dblY; break;
                case 2: dblResult = dblX * dblY; break;
                case 3: dblResult = (dblY == 0) ? 0 : dblX / dblY; break;
            }

            m_Event1(nOp, dblX, dblY, dblResult);
            m_Event2();
        }*/

        static Action<int> eventIncr = delegate { };
        static Action<int> eventDecr = delegate { };
        static Action eventUpdate = delegate { };

        static int counter = 0;

        public void SubscribeIncrEvent()
        {
            ICalcServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ICalcServiceEvents>();
            eventIncr += subscriber.IncrOp;
        }

        public void SubscribeDecrEvent()
        {
            ICalcServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ICalcServiceEvents>();
            eventDecr += subscriber.DecrOp;
        }

        public void SubscribeUpdateFinishedEvent()
        {
            ICalcServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<ICalcServiceEvents>();
            eventUpdate += subscriber.UpdateFinished;
        }
        public void Decr()
        {
            counter--;

            eventDecr(counter);
            eventUpdate();
        }

        public void Incr()
        {
            counter++;

            eventIncr(counter);
            eventUpdate();
        }
    }
}
