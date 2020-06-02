using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceComp
{
    public class Incident
    {
        [Key]
        public int IdIncident { get; set; }

        public int Num { get; set; }

        public string Explain { get; set; }

        public string File { get; set; }

        public string Status { get; set; }

        [ForeignKey("Num")]
        public virtual Insurance Ins { get; set; }
    }
}
