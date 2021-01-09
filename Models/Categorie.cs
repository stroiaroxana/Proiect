﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Categorie
    {
        public int ID { get; set; }
        [Display(Name = "Nume categorie")]
        public string NumeCategorie { get; set; }
        
        public ICollection<CategorieProdus> CategoriiProdus { get; set; }
    }
}
