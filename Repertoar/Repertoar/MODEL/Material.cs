﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Repertoar.MODEL
{
    public class Material
    {
        public int MID { get; set; }

        [Required(ErrorMessage = "En kategori måste anges")]
        public int KaID { get; set; }
       
        [Required(ErrorMessage = "En kompositör måste väljas i listan")]
        public int KompID { get; set; } 

        [Required(ErrorMessage = "Ett namn måste anges")]
        [StringLength(100)]
        public string Namn { get; set; }

        [Required(ErrorMessage = "En svårighetsgrad måste anges")]
        public int Level { get; set; }
       
        [Required(ErrorMessage = "En genre måste anges")]
        [StringLength(20)]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Status måste anges")]
        [StringLength(15)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Ett instrument måste väljas i listan")]
        public int InstrumentID { get; set; } 

        public DateTime Datum { get; set; }

        [StringLength(1500)]
        public string Anteckning { get; set; } //NULLABLE?

        
        //When user adds their own composer
        
        [StringLength(60)]
        public string Composer { get; set; }

    }
}