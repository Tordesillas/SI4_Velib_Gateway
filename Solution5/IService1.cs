using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Solution5
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Task<string[]> GetCitiesName();

        [OperationContract]
        Task<string[]> GetStationsFromCity(string city);

        [OperationContract]
        Task<string> GetStationOfCity(int station, string city);
    }
}
