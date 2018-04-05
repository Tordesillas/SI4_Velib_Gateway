using System;
using System.ServiceModel;

namespace AppConsole
{
    class Program
    {
        /// <summary>
        /// Launches the console application.
        /// Initializes the client.
        /// Parses the user's request.
        /// Relizes a request linked with the first keyword given by the user.
        /// Displays the answer.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans Célib, l'outil efficace pour trouver des vélos célibataires dans votre ville !");
            Console.WriteLine("Tapez une commande ou /help pour connaître les commande disponibles.");

            ServiceCallbackSink objsink = new ServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);

            ServiceReference.Service1Client client = new ServiceReference.Service1Client(iCntxt);

            while (true)
            {
                string[] userResponse = Console.ReadLine().Trim().Split(' ');

                switch (userResponse[0])
                {
                    case "/help":
                        Console.WriteLine("AIDE : /cities - liste toutes les villes.\n" +
                                          "       /stations <ville> - liste les stations d'une ville.\n" +
                                          "       /station <ville> <n° de station> - donne les informations d'une station.\n" +
                                          "       /sub <ville> <n° de station> - souscrit à une station" +
                                          "       /exit - termine le programme.\n");
                        break;

                    case "/cities":
                        Console.WriteLine(string.Join(", ", client.GetCitiesName()));
                        break;

                    case "/stations":
                        if (userResponse.Length != 2)
                        {
                            Console.WriteLine("Les paramètres sont incorrects.");
                        } else
                        {
                            Console.WriteLine(string.Join(", ", client.GetStationsFromCity(userResponse[1])));
                        }
                        break;

                    case "/station":
                        if (userResponse.Length != 3)
                        {
                            Console.WriteLine("Les paramètres sont incorrects.");
                        }
                        else
                        {
                            Int32.TryParse(userResponse[2], out int n);
                            Console.WriteLine(client.GetStationOfCity(n, userResponse[1]));
                        }
                        break;

                    case "/sub":
                        if (userResponse.Length != 3)
                        {
                            Console.WriteLine("Les paramètres sont incorrects.");
                        }
                        else
                        {
                            Int32.TryParse(userResponse[2], out int n);
                            client.SuscribeStationEvent(n, userResponse[1]);
                            Console.WriteLine("Souscription réalisée.");
                        }
                        break;

                    case "/exit":
                        client.Close();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("La commande n'a pas été reconnue.");
                        break;
                }
            }
        }
    }
}
