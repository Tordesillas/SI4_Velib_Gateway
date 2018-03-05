using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Solution5
{
    public class Service1 : IService1
    {
        private Dictionary<string, Station[]> cityCache = new Dictionary<string, Station[]>();
        private Dictionary<string, Station> stationCache = new Dictionary<string, Station>();
        public string[] GetCitiesName()
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156");
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

            return names.ToArray();
        }

        public string[] GetStationsFromCity(string city)
        {
            try
            {
                Station[] stations;
                if (cityCache.ContainsKey(city.ToLower()))
                {
                    cityCache.TryGetValue(city, out stations);
                }
                else
                {
                    WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156");
                    WebResponse response = request.GetResponse();

                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    stations = JsonConvert.DeserializeObject<Station[]>(responseFromServer);

                    cityCache.Add(city.ToLower(), stations);
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

        public string GetStationOfCity(int station, string city)
        {
            try
            {
                Station stationFound;
                if (stationCache.ContainsKey(city.ToLower() + station))
                {
                    stationCache.TryGetValue(city, out stationFound);
                }
                else
                {
                    WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/" + station + "?contract=" + city + "&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156");
                    WebResponse response = request.GetResponse();

                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    stationFound = JsonConvert.DeserializeObject<Station>(responseFromServer);
                    stationCache.Add(city.ToLower() + stationFound.Number, stationFound);
                }
                return stationFound.ToStrings();
            } catch
            {
                return "Aucun résultat n'a été trouvé.";
            }
        }
    }
}
