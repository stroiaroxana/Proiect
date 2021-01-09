using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Producator
    {
        public int ID { get; set; }
        [Display(Name = "Nume producător")]
        public string NumeProducator { get; set; }
        
        public ICollection<Produs> Produse { get; set; }
    }
}
