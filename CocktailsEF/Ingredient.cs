using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public abstract class Ingredient
    {
        [Key, Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public Ingredient() { }
        public Ingredient(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }
    }
}
