using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSED_Commun
{
    public class ProducteurEntreesJournal
    {
        public string NomEchange { get; } 
        public string NomApplication { get; }

        public ProducteurEntreesJournal(string p_nomEchange, string p_nomApplication)
        {
            this.NomEchange = p_nomEchange;
            this.NomApplication = p_nomApplication;
        }

        public void Executer()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int nombreMessages = 10;
            string[] severitesMessages = { "information", "avertissement", "critique" };

            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    // Ajoutez la déclaration de l'échange
                    channel.ExchangeDeclare(
                    exchange: NomEchange,
                    type: "topic",
                    durable: true,
                    autoDelete: false
                    );

                    while (true)
                    {
                        Console.WriteLine("Press [enter] to send messages.");
                        Console.ReadLine();
                        for (int i = 0; i < nombreMessages; i++)
                        {
                            string severiteMessage = severitesMessages[rnd.Next(severitesMessages.Length)];

                            // Construisez votre sujet ici en suivant votre nomenclature :
                            string sujet = $"{severiteMessage}.{NomApplication}.journal";

                            // Construisez votre entreeIci...
                            EntreeJournal entreeJournal = new EntreeJournal() {
                                // ... en remplissant les propriétés de l'objet :
                                Id = Guid.NewGuid(), DateEntree = DateTime.Now, Severite = severiteMessage, Message = $"Message {i} de sévérité {severiteMessage} de l'application {this.NomApplication}"
                            };

                            string message = JsonConvert.SerializeObject(entreeJournal);

                            var body = Encoding.UTF8.GetBytes(message);

                            // Envoyez votre message dans l'échange :
                            channel.BasicPublish(exchange: NomEchange,
                            routingKey: sujet,
                            basicProperties: null,
                            body: body);

                            Console.Out.WriteLine($"Message \"{message}\" dans le sujet {sujet}");
                        }
                    }
                }
            }
        }
    }
}
