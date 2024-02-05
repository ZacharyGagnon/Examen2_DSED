using DSED_Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSED_Examen2_Web.Models
{
    public class DepotJournaux
    {
        private List<EntreeJournal> m_entreeJournals = new List<EntreeJournal>();

        public void AjouterEntree(EntreeJournal p_entreeJournal)
        {
            this.m_entreeJournals.Add(p_entreeJournal);
        }

        public List<EntreeJournal> ObtenirEntreeCritiqueDu(DateTime p_date)
        {
            // Complétez le code pour qu'il renvoie la liste des entrées critiques (severite = "critique") de la journée passée en paramètres
            List<EntreeJournal> entreesCritiques = new List<EntreeJournal>();
            foreach (EntreeJournal entree in m_entreeJournals)
            {
                if (entree.DateEntree.Date == p_date.Date && entree.Severite == "critique")
                {
                   entreesCritiques.Add(entree);
                }
            }
            return entreesCritiques;
        }

        public List<EntreeJournal> ObtenirEntreeInformationDu(DateTime p_date)
        {
            // Complétez le code pour qu'il renvoie la liste des entrées critiques (severite = "information") de la journée passée en paramètres
            List<EntreeJournal> entreeInformation = new List<EntreeJournal>();
            foreach (EntreeJournal entree in m_entreeJournals)
            {
                if (entree.DateEntree.Date == p_date.Date && entree.Severite == "information")
                {
                    entreeInformation.Add(entree);
                }
            }
            return entreeInformation;
        }

        public List<EntreeJournal> ObtenirEntreeAvertissementDu(DateTime p_date)
        {
            // Complétez le code pour qu'il renvoie la liste des entrées critiques (severite = "avertissement") de la journée passée en paramètres
            List<EntreeJournal> entreeAvertissement = new List<EntreeJournal>();
            foreach (EntreeJournal entree in m_entreeJournals)
            {
                if (entree.DateEntree.Date == p_date.Date && entree.Severite == "avertissement")
                {
                    entreeAvertissement.Add(entree);
                }
            }
            return entreeAvertissement;
        }
    }
}
