using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class AmountIngredient
    {
        [Key]
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }


        [Key]
        public int AmountID { get; set; }
        public Amount Amount { get; set; }

        public List<Drink> Drinks { get; set; }

    }
}
