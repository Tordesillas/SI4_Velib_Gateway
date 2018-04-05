using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Solution5
{
    public class Service1 : IService1
    {
        private string[] citiesCache;
        private Dictionary<string, Station[]> stationsCache;
        private long delay;

        /// <summary>
        /// Constructs a Service1 and launches the timer to empty the cache.
        /// </summary>
        public Service1()
        {
            delay = 604800000;
            stationsCache = new Dictionary<string, Station[]>();
            actions = new Dictionary<Tuple<string, int>, Action<string, int>>();
            bikes = new Dictionary<Tuple<string, int>, int>();
            DelayCache();
        }

        /// <summary>
        /// Gives the available city names of JCDecaux.
        /// Uses the cache if the request has been already done.
        /// </summary>
        public async Task<string[]> GetCitiesName()
        {
            if (citiesCache == null)
            {
                WebRequest request = await Task.Run(() => request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156"));
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                Station[] answers = JsonConvert.DeserializeObject<Station[]>(responseFromServer);

                List<string> names = new List<string>();
                foreach (Station ro in answers)
                {
                    names.Add(ro.Name);
                }
                reader.Close();
                response.Close();

                citiesCache = names.ToArray();
            }

            return citiesCache;
        }

        /// <summary>
        /// Gives the station names of a city thanks to its name given in parameter.
        /// Uses the cache if the request has been already done.
        /// </summary>
        public async Task<string[]> GetStationsFromCity(string city)
        {
            try
            {
                Station[] stations;
                if (stationsCache.ContainsKey(city.ToLower()))
                {
                    stationsCache.TryGetValue(city, out stations);
                }
                else
                {
                    WebRequest request = await Task.Run(() => WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156"));
                    WebResponse response = request.GetResponse();

                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    stations = JsonConvert.DeserializeObject<Station[]>(responseFromServer);

                    stationsCache.Add(city.ToLower(), stations);
                    reader.Close();
                    response.Close();
                }

                List<string> names = new List<string>();
                foreach (Station ro in stations)
                {
                    if (char.IsDigit(ro.Name[0]))
                    {
                        names.Add(ro.Name);
                    } else
                    {
                        names.Add(ro.Number + " - " + ro.Name);
                    }
                }

                return names.ToArray();
            } catch
            {
                return new string[] { "Aucun résultat n'a été trouvé."};
            }
        }

        /// <summary>
        /// Gives the data known about a station of a city. Here, only the number of available bikes is given.
        /// Uses the cache if the request has been already done.
        /// </summary>
        /// <param name="station">The station id</param>
        /// <param name="city">The city name which contains the station</param>
        public async Task<string> GetStationOfCity(int station, string city)
        {
            try
            {
                Station stationFound;
                WebRequest request = await Task.Run(() => WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/" + station + "?contract=" + city + "&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156"));
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                stationFound = JsonConvert.DeserializeObject<Station>(responseFromServer);

                Tuple<string, int> tuple = new Tuple<string, int>(city, station);
                if (actions.ContainsKey(tuple) && !bikes[tuple].Equals(stationFound.Available_bikes))
                {
                    actions[tuple](stationFound.Name, stationFound.Available_bikes);
                    bikes[tuple] = stationFound.Available_bikes;
                }

                return stationFound.ToStrings();
            }
            catch
            {
                return "Aucun résultat n'a été trouvé.";
            }
        }

        /// <summary>
        /// Launches a timer which will erase the cache in one week.
        /// </summary>
        private void DelayCache()
        {
            Task.Delay(new TimeSpan(delay)).ContinueWith(t => EmptyCache());
            Task.Delay(new TimeSpan(delay / 100)).ContinueWith(t => CallSubscriberAsync());
        }

        /// <summary>
        /// Erases the cache and reloads the timer.
        /// </summary>
        void EmptyCache()
        {
            stationsCache = new Dictionary<string, Station[]>();
            citiesCache = null;
            DelayCache();
        }

        /// <summary>
        /// Erases cache.
        /// </summary>
        void IService1.EmptyCache()
        {
            EmptyCache();
        }

        /// <summary>
        /// Change the delay of the cache removing.
        /// </summary>
        public void SetCacheDelay(long newDelay)
        {
            delay = newDelay;
            EmptyCache();
        }

        private Dictionary<Tuple<string, int>, Action<string, int>> actions;
        private Dictionary<Tuple<string, int>, int> bikes;

        public void SuscribeStationEvent(int station, string city)
        {
            IServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IServiceEvents>();
            Tuple<string, int> tuple = new Tuple<string, int>(city, station);
            if (actions.ContainsKey(tuple))
            {
                actions[tuple] += subscriber.GetStation;
            } else
            {
                Action<string, int> newEvent = delegate { };
                newEvent += subscriber.GetStation;
                actions.Add(tuple, newEvent);
                bikes.Add(tuple, -1);
            }
        }

        private async Task CallSubscriberAsync()
        {
            foreach (Tuple<string, int> tuple in actions.Keys)
            {
                await GetStationOfCity(tuple.Item2, tuple.Item1);
            }
        }
    }
}
