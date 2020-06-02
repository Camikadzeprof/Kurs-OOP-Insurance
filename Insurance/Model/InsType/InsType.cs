using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceComp
{
    public class InsType
    {
        [Key]
        public string Type { get; set; }

        public int Fee { get; set; }

        public int MaxPayout { get; set; }
    }
}
