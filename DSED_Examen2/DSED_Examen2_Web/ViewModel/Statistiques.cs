using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSED_Examen2_Web.ViewModel
{
    public class Statistiques
    {
        public DateTime Date { get; set; }
        public int NombreEntreesCritiques { get; set; }
        public int NombreEntreesInformations { get; set; }
        public int NombreEntreesAvertissements { get; set; }
    }
}
