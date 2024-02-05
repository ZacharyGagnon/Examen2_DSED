using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System;
using System.Text;
using DSED_Commun;

namespace DSED_Examen2_CopierToutesLesEntreesCritiquesVersFichiers
{
    internal class Program
    {
        public static string NomFile => "ex2-file-copieCritique";
        public static string NomEchange => "ex2-bus-entreprise";
        static void Main(string[] args)
        {
            string[] requetesSujets = { "critique.*.*" };
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                    exchange: NomEchange,
                    type: "topic",
                    durable: true,
                    autoDelete: false
                    );
                    channel.QueueDeclare(
                    NomFile,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: NomFile,
                        exchange: NomEchange,
                        routingKey: requeteSujet);
                    }

                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                    consumateur.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        string sujet = ea.RoutingKey;

                        Console.WriteLine($"Message reçu \"{message}\" avec le sujet : {sujet}");
                        
                        EntreeJournal entreeJournal = JsonConvert.DeserializeObject<EntreeJournal>(message);
                        
                        System.IO.File.WriteAllText(@"..\..\..\..\JournalCritique\" + $"{entreeJournal.Id}.txt", message);
                        
                    };
                    channel.BasicConsume(queue: NomFile,
                    autoAck: true,
                    consumer: consumateur);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}