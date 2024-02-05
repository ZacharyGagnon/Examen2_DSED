using DSED_Commun;
using DSED_Examen2_BusEntrepriseProxy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RabbitMQ.Client;
using System.Numerics;
using System.Text;

namespace DSED_Examen2_BusEntrepriseProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntreeJournalController : ControllerBase
    {
        public static string NomFile => "ex2-file-API";
        public static string NomEchange => "ex2-bus-entreprise";
        // POST: HomeController1/Create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] EntreeJournalModel p_entreeJournal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (p_entreeJournal.Severite != "critique" || p_entreeJournal.Severite != "avertissement" || p_entreeJournal.Severite != "information")
            {
                return BadRequest();
            }
            EntreeJournal entreeJournal = p_entreeJournal.VersEntite();
            string sujet = $"{p_entreeJournal.Severite}.{p_entreeJournal.NomApplication}.journal";
            string jsonClient = Newtonsoft.Json.JsonConvert.SerializeObject(entreeJournal);
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connexion = factory.CreateConnection())
            {
                using (RabbitMQ.Client.IModel channel = connexion.CreateModel())
                {
                    channel.QueueDeclare(queue: NomFile,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                    byte[] body = Encoding.UTF8.GetBytes(jsonClient);

                    channel.BasicPublish(exchange: NomEchange, routingKey: sujet, body: body);
                }
            }
            return CreatedAtAction("Post", entreeJournal);
        }
    }
}
