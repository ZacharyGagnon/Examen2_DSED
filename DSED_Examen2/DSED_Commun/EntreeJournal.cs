using System;

namespace DSED_Commun
{
    public class EntreeJournal
    {
        // Ajoutez les propriétés d'une entree de journal
        public Guid Id { get; set; }
        public DateTime DateEntree { get; set; }
        public string Severite { get; set; }
        public string Message { get; set; }
    }
}
