using DSED_Commun;
using DSED_Examen2_Web.Hubs;
using DSED_Examen2_Web.Models;
using DSED_Examen2_Web.ViewModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DSED_Examen2_Web.Consommateur
{
    // Code outrageusement simplifié pour ne pas rentrer dans de la technique
    // Ne pas faire cela en production
    public class ConsommateurFileJournalisation //: BackgroundService
    {
        public static string NomFile => "ex2-file-producteur";
        public static string NomEchange => "ex2-bus-entreprise";

        private static IConnection _connection;
        private static IModel _channel;
        private static IHubContext<TableauDeBordHub> _tableauDeBordHub;
        private static DepotJournaux _depotJournaux = new DepotJournaux();

        public static void MettreConsommateurJournauxEnPlace(IHubContext<TableauDeBordHub> p_tableauDeBordHub)
        {
            _tableauDeBordHub = p_tableauDeBordHub;

            string[] requetesSujets = { "*.*.*" };
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Ajoutez la création de l'échange
            _channel.ExchangeDeclare(
                    exchange: NomEchange,
                    type: "topic",
                    durable: true,
                    autoDelete: false
                    );
            // Ajoutez la création de la ou des files de messages
            _channel.QueueDeclare(
                    NomFile,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
            // Ajoutez l'association de l'échange, files et sujets
            foreach (var requeteSujet in requetesSujets)
            {
                _channel.QueueBind(queue: NomFile,
                exchange: NomEchange,
                routingKey: requeteSujet);
            }

            EventingBasicConsumer consumateur = new EventingBasicConsumer(_channel);
            consumateur.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                string sujet = ea.RoutingKey;

                EntreeJournal ej = JsonConvert.DeserializeObject<EntreeJournal>(message);

                // Ajoutez l'entrée au dépot de jounaux
                _depotJournaux.AjouterEntree(ej);

                // Ajoutez la mise à jour du nombre d'entrées par sévérité pour la journée courante
                // Utilisez l'objet contenu dans _tableauDeBordHub et en créant un objet de type Statistiques


                Console.WriteLine($"Message reçu \"{message}\" avec le sujet : {sujet}");
            };
            _channel.BasicConsume(queue: NomFile,
                                  autoAck: true,
                                  consumer: consumateur);
        }
    }
}
