using DSED_Commun;

namespace DSED_Examen2_BusEntrepriseProxy.Models
{
    public class EntreeJournalModel
    {
        public Guid Id { get; }
        public DateTime DateEntree { get; }
        public string NomApplication { get; set; }
        public string Severite { get; set; }
        public string Message { get; set; }

        public EntreeJournalModel ()
        {
            ;
        }

        public EntreeJournalModel (string nomApplication, string severite, string message)
        {
            Id = Guid.NewGuid();
            DateEntree = DateTime.Now;
            NomApplication = nomApplication;
            Severite = severite;
            Message = message;
        }

        public EntreeJournal VersEntite()
        {
            return new EntreeJournal()
            {
                Id = Id,
                DateEntree = DateEntree,
                Severite = Severite,
                Message = Message
            };
        }
    }
}
