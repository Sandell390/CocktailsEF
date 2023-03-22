using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsEF
{
    public class AmountManager
    {

        public Amount? GetAmount(float number, string unite)
        {
            Amount amount;
            using (var context = new DrinkContext())
            {
                amount = context.Amounts.Where(amount => amount.Number == number && amount.Unite == unite)!.ToList().FirstOrDefault()!;
            }

            if (amount == null)
            {
                AddAmount(new Amount(number, unite));
                amount = GetAmount(number, unite);
            }

            return amount;
        }

        public Amount AddAmount(Amount amount)
        {
            using (var context = new DrinkContext())
            {
                context.Add<Amount>(amount);
                context.SaveChanges();
            }

            return amount;
        }
    }
}
