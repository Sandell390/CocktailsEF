using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class Amount
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public float Number { get; set; }

        [Required]
        public string Unite { get; set; }

        public Amount() { }

        public Amount(float number, string unite)
        {
            Number = number;
            Unite = unite;
        }

    }
}
