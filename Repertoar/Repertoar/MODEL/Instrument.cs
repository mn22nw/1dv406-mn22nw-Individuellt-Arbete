using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repertoar.MODEL
{
    public class Instrument
    {
        public int InstrumentID { get; set; }

        [Required(ErrorMessage = "Ett namn måste anges")]
        [StringLength(60)]
        public string Namn { get; set; }

    }
}