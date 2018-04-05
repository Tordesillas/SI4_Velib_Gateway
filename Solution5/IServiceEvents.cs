using System.ServiceModel;

namespace Solution5
{
    interface IServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void GetStation(string station, int bikes);
    }
}
