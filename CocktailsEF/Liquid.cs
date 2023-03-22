using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class Liquid : Ingredient
    {
        [Required]
        public LiquidType Type { get;  set; }

        [Required]
        public string Color { get;  set; }

        public enum LiquidType
        {
            Juice,
            Soda,
            Sprite,
            Other
        }

        public Liquid() :base(){ }

        public Liquid(string name,string icon, string color, LiquidType type) : base(name, icon)
        {
            Type = type;
            Color = color;
        }
    }
}
