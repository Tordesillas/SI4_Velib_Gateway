# Velib Gateway

## Projets de la solution
- AppConsole : Console pour l'utilisateur du système.
- AppForm : Interface graphique pour l'utilisateur du système.
- AppAdmin : Console pour l'administrateur du système.
- Solution5 : Web Service intermédiaire.

## Travail réalisé

### MVP
Le Web Service propose une API WS-SOAP.
Une application console permet au client de connaître le nombre de vélos disponibles à travers l'API.
Le client peut obtenir de l'aide à partir de la commande /help.
    
### Extensions
-   Interface graphique pour le client.
-   Accès asynchrones dans l'interface graphique.
-   Ajout du cache.


### Choix technique : la mise en place du cache
Créer un cache est une solution efficace pour réduire les requêtes entre un client et un Web Service. Ici, le service intermédiaire en prend la responsabilité afin de limiter les accès au Web Service de Vélib. 3 informations transitent par l'IWS : la liste des villes, celle des stations et les vélos disponibles par station. Seules les deux premières données sont mises en cache. En effet, la dernière est une information devant se mettre à jour régulièrement, ce qui est contraire au système de cache.
Pour renouveler les informations disponibles dans le cache, les données sont supprimées toutes les semaines par défaut. L'administrateur peut utiliser une console pour réaliser la suppression.

## Utilisation des applications
### Console pour le client
L'utilisateur peut réaliser 3 commandes : lister les villes enregistrées dans le système Vélib, lister les stations associées à une ville et obtenir le nombre de vélos disponibles dans une station.
Pour cela, entrez une commande de la forme suivante : `/commande argument1 argument2`.
Tapez /help pour connaître la liste des commandes disponibles.
Par exemple, pour connaître les vélos disponibles de la première station de Toulouse, entrez: `/station Toulouse 1`.
### Interface graphique
Choisissez une ville dans le premier champ, une station dans la seconde pour afficher le nombre de vélos célibataires dans cette station.
### Console pour l'administrateur
L'administrateur peut, depuis la console, vider les caches et définir le temps de rafraîchissement du cache de l'IWS.
Pour cela, entrez une commande de la forme suivante : `/commande argument1 argument2`.
Tapez /help pour connaître la liste des commandes disponibles.
Par exemple, pour vider le cache du serveur intermédiaire, entrez: `/empty_cache`.
