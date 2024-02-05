using DSED_Commun;
using System;

namespace DSED_Producteur2
{
    class Program
    {
        public static string NomEchange => "ex2-bus-entreprise";
        public static string NomApplication => "DSED_Producteur2";
        static void Main(string[] args)
        {
            (new ProducteurEntreesJournal(NomEchange, NomApplication)).Executer();
        }
    }
}
