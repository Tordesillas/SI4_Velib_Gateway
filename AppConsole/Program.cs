using System;

namespace AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans Célib, l'outil efficace pour trouver des vélos célibataires dans votre ville !");
            Console.WriteLine("Tapez une commande ou /help pour connaître les commande disponibles.");
            Service1Client client = new Service1Client();

            while (true)
            {
                string[] userResponse = Console.ReadLine().Trim().Split(' ');

                switch (userResponse[0])
                {
                    case "/help":
                        Console.WriteLine("AIDE : /cities - liste toutes les villes.\n" +
                                          "       /stations <ville> - liste les stations d'une ville.\n" +
                                          "       /station <ville> <n° de station> - donne les informations d'une station.\n" +
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
