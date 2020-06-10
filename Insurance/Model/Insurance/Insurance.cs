using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceComp
{
    public class Insurance
    {
        [Key]
        public int Num { get; set; }

        public string Type { get; set; }

        public string Username { get; set; }

        [ForeignKey("Type")]
        public virtual InsType Ins { get; set; }

        [ForeignKey("Username")]
        public virtual User user { get; set; }

    }
}
