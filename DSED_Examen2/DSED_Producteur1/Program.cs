using DSED_Commun;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DSED_Producteur1
{
    class Program
    {
        public static string NomEchange => "ex2-bus-entreprise";
        public static string NomApplication => "DSED_Producteur1";
        static void Main(string[] args)
        {
            (new ProducteurEntreesJournal(NomEchange, NomApplication)).Executer();
        }
    }
}
