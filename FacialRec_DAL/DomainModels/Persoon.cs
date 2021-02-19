using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRec_DAL.DomainModels
{
    [Table("Persoon")]
    public class Persoon
    {
        public int ID { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public byte [] Foto { get; set; }

        public override string ToString()
        {
            return Achternaam;
        }
    }
}
