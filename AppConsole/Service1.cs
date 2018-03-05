namespace AppConsole
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IService1")]
    public interface IService1
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService1/GetCitiesName", ReplyAction = "http://tempuri.org/IService1/GetCitiesNameResponse")]
        string[] GetCitiesName();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService1/GetCitiesName", ReplyAction = "http://tempuri.org/IService1/GetCitiesNameResponse")]
        System.Threading.Tasks.Task<string[]> GetCitiesNameAsync();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService1/GetStationsFromCity", ReplyAction = "http://tempuri.org/IService1/GetStationsFromCityResponse")]
        string[] GetStationsFromCity(string city);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService1/GetStationsFromCity", ReplyAction = "http://tempuri.org/IService1/GetStationsFromCityResponse")]
        System.Threading.Tasks.Task<string[]> GetStationsFromCityAsync(string city);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService1/GetStationOfCity", ReplyAction = "http://tempuri.org/IService1/GetStationOfCityResponse")]
        string GetStationOfCity(int station, string city);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService1/GetStationOfCity", ReplyAction = "http://tempuri.org/IService1/GetStationOfCityResponse")]
        System.Threading.Tasks.Task<string> GetStationOfCityAsync(int station, string city);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : IService1, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<IService1>, IService1
    {

        public Service1Client()
        {
        }

        public Service1Client(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public Service1Client(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public string[] GetCitiesName()
        {
            return base.Channel.GetCitiesName();
        }

        public System.Threading.Tasks.Task<string[]> GetCitiesNameAsync()
        {
            return base.Channel.GetCitiesNameAsync();
        }

        public string[] GetStationsFromCity(string city)
        {
            return base.Channel.GetStationsFromCity(city);
        }

        public System.Threading.Tasks.Task<string[]> GetStationsFromCityAsync(string city)
        {
            return base.Channel.GetStationsFromCityAsync(city);
        }

        public string GetStationOfCity(int station, string city)
        {
            return base.Channel.GetStationOfCity(station, city);
        }

        public System.Threading.Tasks.Task<string> GetStationOfCityAsync(int station, string city)
        {
            return base.Channel.GetStationOfCityAsync(station, city);
        }
    }
}