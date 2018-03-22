using AppAdmin.ServiceReference1;
using System;

namespace AppAdmin
{
    class Program
    {
        /// <summary>
        /// Launches a console for the admin of Célib.
        /// Proposes to the admin influencing the server cache.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans la session administrateur de Célib, l'outil efficace pour trouver des vélos célibataires dans votre ville !");
            Console.WriteLine("Tapez une commande ou /help pour connaître les commande disponibles.");
            Service1Client client = new Service1Client();

            while (true)
            {
                string[] userResponse = Console.ReadLine().Trim().Split(' ');

                switch (userResponse[0])
                {
                    case "/help":
                        Console.WriteLine("AIDE : /empty_cache - vide le cache du serveur intermédiaire.\n" +
                                          "       /set_delay <delay> - donne les informations d'une station.\n" +
                                          "       /exit - termine le programme.\n");
                        break;

                    case "/empty_cache":
                        client.EmptyCache();
                        Console.WriteLine("Le cache a été vidé.");
                        break;

                    case "/set_delay":
                        if (userResponse.Length != 2)
                        {
                            Console.WriteLine("Les paramètres sont incorrects.");
                        }
                        else
                        {
                            Int32.TryParse(userResponse[1], out int n);
                            client.SetCacheDelay(n);
                            Console.WriteLine("Le délai du cache a été modifié.");
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
