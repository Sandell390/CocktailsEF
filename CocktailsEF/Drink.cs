using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class Drink
    {
        [Key]
        [Required]
        public int ID { get; set; }


        public ICollection<AmountIngredient> AmountIngredient { get; set; }


        [Required]
        public DrinkType Type { get; set; }

        [Required]
        public string Name { get; set; }

        public Drink() { }

        public Drink(string name, DrinkType type, int id) 
        { 
            Type = type;
            Name = name;
            ID = id;
        }

        public enum DrinkType
        {
            Martini,
            HighballNCollins,
            Shot,
            Rocks,
            Margarita,
            Champage,
            WhiteWine,
            RedWine,
            Beer,
            Other
        }
    }
}
