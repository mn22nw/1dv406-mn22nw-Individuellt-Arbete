using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL
{
    public class Kompositör
    {
        public int KompID { get; set; }

        [Required(ErrorMessage = "Ett namn måste anges")]
        [StringLength(60)]
        public string Namn { get; set; }

        [StringLength(1500)]
        public string Anteckning { get; set; }
    }
}