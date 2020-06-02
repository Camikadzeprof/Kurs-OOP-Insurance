using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceComp
{
    public class User
    {
        [Key]
        [MaxLength(15)]
        [Required]
        public string Username { get; set; }

        [MaxLength(15)]
        [Required]
        public string Name { get; set; }

        [MaxLength(20)]
        [Required]
        public string Surname { get; set; }

        [MaxLength(100)]
        [Required]
        public string Password { get; set; }

        [MaxLength(25)]
        [Required]
        public string SecretWord { get; set; }

        public int Role { get; set; }
    }
}
