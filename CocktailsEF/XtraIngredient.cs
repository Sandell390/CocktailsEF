using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class XtraIngredient : Ingredient
    {
        public XtraIngredient() : base() { }
        public XtraIngredient(string name, string icon) : base(name, icon)
        {

        }
    }
}
