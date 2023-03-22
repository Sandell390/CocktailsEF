using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class IngredientManager
    {

        public List<Ingredient> GetIngredients() 
        {
            using (var context = new DrinkContext())
            {
                List<Liquid> liquids = context.Liquid.ToList();
                List<XtraIngredient> xtraIngredient = context.XtraIngredients.ToList();
                List<Ingredient> big = new List<Ingredient>();
                big.AddRange(liquids);
                big.AddRange(xtraIngredient);
                return big;
            }
        }

        public Ingredient? GetIngredient(int id) 
        {
            using (var context = new DrinkContext())
            {
                return context.Find<Ingredient>(id);
            }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            using (var context = new DrinkContext())
            {
                context.Add<Ingredient>(ingredient);
                context.SaveChanges();

            }
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            if (ingredient == null)
                return;

            using (var context = new DrinkContext())
            {
                context.Remove<Ingredient>(ingredient);
                context.SaveChanges();


            }
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            using (var context = new DrinkContext())
            {
                context.Update<Ingredient>(ingredient);
                context.SaveChanges();


            }
        }
    }
}
