using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceComp
{
    public class Payout
    {
        [Key]
        public int IdPayout { get; set; }

        public int IdIncident { get; set; }

        public int Sum { get; set; }

        [ForeignKey("IdIncident")]
        public virtual Incident incident { get; set; }
    }
}
