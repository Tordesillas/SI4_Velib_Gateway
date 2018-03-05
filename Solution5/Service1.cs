using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Solution5
{
    public class Service1 : IService1
    {
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
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156");
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                Station[] answers = JsonConvert.DeserializeObject<Station[]>(responseFromServer);

                List<string> names = new List<string>();
                foreach (Station ro in answers)
                {
                    if (char.IsDigit(ro.Name[0]))
                    {
                        names.Add(ro.Name);
                    } else
                    {
                        names.Add(ro.Number + " - " + ro.Name);
                    }
                }
                reader.Close();
                response.Close();

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
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/" + station + "?contract=" + city + "&apiKey=a8d04614d18cc2033006de4ac10446eb0af81156");
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                Station answer = JsonConvert.DeserializeObject<Station>(responseFromServer);

                return answer.ToStrings();
            } catch
            {
                return "Aucun résultat n'a été trouvé.";
            }
        }
    }
}
