using System.ServiceModel;

namespace EventsLib
{
    [ServiceContract(CallbackContract = typeof(ICalcServiceEvents))]
    public interface ICalcService
    {
        /*[OperationContract]
        void Calculate(int nOp, double dblNum1, double dblNum2);

        [OperationContract]
        void SubscribeCalculatedEvent();

        [OperationContract]
        void SubscribeCalculationFinishedEvent();*/

        [OperationContract]
        void Incr();

        [OperationContract]
        void Decr();

        [OperationContract]
        void SubscribeIncrEvent();

        [OperationContract]
        void SubscribeDecrEvent();

        [OperationContract]
        void SubscribeUpdateFinishedEvent();
    }
}