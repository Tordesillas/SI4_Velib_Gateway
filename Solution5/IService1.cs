using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Solution5
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string[] GetCitiesName();

        [OperationContract]
        string[] GetStationsFromCity(string city);

        [OperationContract]
        string GetStationOfCity(int station, string city);
    }
}
