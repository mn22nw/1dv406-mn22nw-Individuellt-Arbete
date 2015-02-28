using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL
{
    public class Kategori
    {
        public int KaID { get; set; }

        [Required(ErrorMessage = "Ett namn måste anges")]
        [StringLength(25)]
        public string Namn { get; set; }
    }
}