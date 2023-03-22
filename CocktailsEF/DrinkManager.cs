using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class DrinkManager
    {
        public List<Drink> GetDrinks()
        {
            using (var context = new DrinkContext())
            {
                List<Drink> drinks = context.Drinks.ToList();
                List<Drink> returnDrinks = new List<Drink>();
                foreach (var drink in drinks)
                {
                    returnDrinks.Add(GetDrink(drink.ID));
                }

                return returnDrinks;
            }
        }

        public Drink? GetDrink(int id)
        {
            using (var context = new DrinkContext())
            {
                return context.Drinks.Where(d => d.ID == id).Include(i => i.AmountIngredient).ThenInclude(i => i.Amount).Include(i => i.AmountIngredient).ThenInclude(i => i.Ingredient).SingleOrDefault();
            }
        }

        public void AddDrink(Drink drink)
        {
            using (var context = new DrinkContext())
            {
                //context.Add<Drink>(drink);

                Drink tempDrink = new Drink();
                tempDrink.Name = drink.Name;
                tempDrink.Type = drink.Type;
                tempDrink.AmountIngredient =  new List<AmountIngredient>();
                var addeddrink = context.Add(tempDrink);
                context.SaveChanges();

                var drink1 = context.Drinks.Include("AmountIngredient").Single(d => d.ID == addeddrink.Entity.ID);

                foreach (var item in drink.AmountIngredient)
                {
                    AmountIngredient amount = context.AmountIngredients.Where(i => i.IngredientID == item.Ingredient.ID && i.AmountID == item.Amount.ID).SingleOrDefault();
                    if (amount == null)
                    {
                        amount = context.AmountIngredients.Attach(item).Entity;
                    }

                    drink1.AmountIngredient.Add(amount);
                }
                context.SaveChanges();
            }
        }

        public void RemoveDrink(Drink drink)
        {
            if (drink == null)
                return;
            using (var context = new DrinkContext())
            {
                context.Remove<Drink>(drink);
                context.SaveChanges();

            }
        }

        public void UpdateDrink(Drink drink)
        {
            using (var context = new DrinkContext())
            {
                var drink1 = context.Drinks.Include("AmountIngredient").Single(d => d.ID == drink.ID);
                drink1.Name = drink.Name;
                drink1.Type = drink.Type;
                foreach (var item in drink1.AmountIngredient)
                {
                    if (!drink.AmountIngredient.Any(x => x.IngredientID == item.IngredientID && x.AmountID == item.AmountID))
                    {
                        drink1.AmountIngredient.Remove(item);
                    }
                }

                foreach (var item in drink.AmountIngredient)
                {
                    if (!drink1.AmountIngredient.Any(x => x.IngredientID == item.IngredientID && x.AmountID == item.AmountID))
                    {
                        AmountIngredient amount = context.AmountIngredients.Where(i => i.IngredientID == item.Ingredient.ID && i.AmountID == item.Amount.ID).SingleOrDefault();
                        if (amount == null)
                        {
                            amount = context.AmountIngredients.Attach(item).Entity;
                        }

                        drink1.AmountIngredient.Add(item);
                    }
                }

                context.SaveChanges();


            }
        }
    }
}
