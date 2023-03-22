namespace CocktailsEF
{
    internal class Program
    {
        static IngredientManager ingredientManager = new IngredientManager();
        static DrinkManager drinkManager = new DrinkManager();
        static AmountManager amountManager = new AmountManager();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (var context = new DrinkContext())
            {
                context.Database.EnsureCreated();
            }

            while (true)
            {
                Console.Clear();

                Console.WriteLine("1. Ingredients");
                Console.WriteLine("2. Drinks");
                switch (Console.ReadLine()) 
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("1. View All");
                        Console.WriteLine("2. Edit Ingredient");
                        Console.WriteLine("3. Add ingredient");
                        Console.WriteLine("4. Delete ingredient");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                ViewAllIngredients();
                                break;
                            case "2":
                                ViewAllIngredients();
                                Console.WriteLine();
                                Console.WriteLine("Choice ingredient: ");
                                string choice = Console.ReadLine();
                                UpdateIngredient(int.Parse(choice));
                                break;
                            case "3":
                                AddIngredient();
                                break;
                            case "4":
                                DeleteIngredient();
                                break;
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("1. View All");
                        Console.WriteLine("2. Edit Drink");
                        Console.WriteLine("3. Add Drink");
                        Console.WriteLine("4. Delete Drink");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                ViewAllDrink();
                                Console.Read();
                                break;
                            case "2":
                                UpdateDrink();
                                break;
                            case "3":
                                AddDrink();
                                break;
                            case "4":
                                RemoveDrink();
                                break;
                        }
                        break;
                }
            }
        }

        static void ViewAllIngredients()
        {
            List<Ingredient> ingredients = ingredientManager.GetIngredients();

            Console.WriteLine();

            for (int i = 0; i < ingredients.Count; i++)
            {
                Ingredient ingredient = ingredients[i];
                Console.Write($"{ingredient.ID}. Name: {ingredient.Name} | Icon: {ingredient.Icon} ");
                if(ingredient is Liquid)
                    Console.Write($"| Color: {((Liquid)ingredient).Color} | Type: {((Liquid)ingredient).Type.ToString()}");

                Console.WriteLine();
            }

        }

        static void AddIngredient()
        {
            Ingredient ing;
            Console.Clear();
            Console.WriteLine("New Ingredient: ");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Icon: ");
            string icon = Console.ReadLine();

            Console.WriteLine("Is this a liquid? 1 = yes | 2 = no");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Color: ");
                string color = Console.ReadLine();
                Console.WriteLine();
                for (int i = 0; i < Enum.GetNames(typeof(Liquid.LiquidType)).Length; i++)
                {
                    Console.WriteLine($"{i}. {Enum.GetNames(typeof(Liquid.LiquidType))[i]}");
                }

                Console.WriteLine("Choice a Liquid type: ");
                int index = int.Parse(Console.ReadLine());
                Liquid.LiquidType type = (Liquid.LiquidType)Enum.Parse(typeof(Liquid.LiquidType), index.ToString());
                ing = new Liquid(name, icon, color, type);
            }
            else
            {
                ing = new XtraIngredient(name, icon);
            }
            ingredientManager.AddIngredient(ing);
            Console.WriteLine("Created new ingredient");
            Console.ReadKey();
        }

        static void ViewIngredient(int id)
        {
            Ingredient ing = ingredientManager.GetIngredient(id);

            Console.Write($"Name: {ing.Name} | Icon: {ing.Icon} ");
            if (ing is Liquid)
                Console.Write($"| Color: {((Liquid)ing).Color} | Type: {((Liquid)ing).Type.ToString()}");
            Console.WriteLine();
        }

        static void UpdateIngredient(int id)
        {
            Console.Clear();
            Console.WriteLine("Update Ingredient: ");
            Ingredient ing = ingredientManager.GetIngredient(id);

            ViewIngredient(id);

            Console.WriteLine("1. name");
            Console.WriteLine("2. icon");
            if (ing is Liquid)
            {
                Console.WriteLine("3. Color");
                Console.WriteLine("4. type");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ing.Name = Console.ReadLine();
                        break;
                    case "2":
                        ing.Icon = Console.ReadLine();
                        break;
                    case "3":
                        ((Liquid)ing).Color = Console.ReadLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        for (int i = 0; i < Enum.GetNames(typeof(Liquid.LiquidType)).Length; i++)
                        {
                            Console.WriteLine($"{i}. {Enum.GetNames(typeof(Liquid.LiquidType))[i]}");
                        }

                        Console.WriteLine("Choice a Liquid type: ");
                        int index = int.Parse(Console.ReadLine());
                        Liquid.LiquidType type = (Liquid.LiquidType)Enum.Parse(typeof(Liquid.LiquidType), index.ToString());
                        break;
                }
            }
            else
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ing.Name = Console.ReadLine();
                        break;
                    case "2":
                        ing.Icon = Console.ReadLine();
                        break;
                }
            }
            ingredientManager.UpdateIngredient(ing);
        }

        static void DeleteIngredient()
        {
            ViewAllIngredients();
            Console.WriteLine("Choice one to delete: ");
            string choice = Console.ReadLine();
            ingredientManager.RemoveIngredient(ingredientManager.GetIngredient(int.Parse(choice)));
        }

        static void ViewAllDrink()
        {
            List<Drink> drinks = drinkManager.GetDrinks().OrderBy(i => i.ID).ToList();

            Console.Clear();
            Console.WriteLine("All Drinks: ");
            Console.WriteLine();


            foreach (var drink in drinks)
            {
                Console.WriteLine($"{drink.ID}. Name: {drink.Name}");
                Console.WriteLine("Ingredients: ");
                if (drink.AmountIngredient == null)
                    continue;
                foreach (var item in drink.AmountIngredient)
                {
                    Console.WriteLine($"{item.Ingredient.Name} {item.Amount.Number} {item.Amount.Unite}");
                }

                Console.WriteLine();
            }
        }
        static void AddDrink()
        {
            Drink drink = new Drink();
            Console.Clear();
            Console.WriteLine("Add drink: ");
            Console.WriteLine("Name: ");
            drink.Name = Console.ReadLine();
            Console.WriteLine();
            for (int i = 0; i < Enum.GetNames(typeof(Drink.DrinkType)).Length; i++)
            {
                Console.WriteLine($"{i}. {Enum.GetNames(typeof(Drink.DrinkType))[i]}");
            }

            Console.WriteLine("Choice a Glass type: ");
            drink.Type = (Drink.DrinkType)Enum.Parse(typeof(Drink.DrinkType), Console.ReadLine());

            drink.AmountIngredient = new List<AmountIngredient>();
            Console.WriteLine();
            bool ingredientDone = false;
            while (!ingredientDone)
            {
                Console.WriteLine();
                ViewAllIngredients();
                Console.WriteLine("Choice a ingredient: ");
                string ingIndex = Console.ReadLine();

                Console.WriteLine("Which unite?");
                string unite = Console.ReadLine();

                Console.WriteLine("How much? ");
                string number = Console.ReadLine();

                drink.AmountIngredient.Add(new AmountIngredient() { Amount = amountManager.GetAmount(float.Parse(number), unite), Ingredient = ingredientManager.GetIngredient(int.Parse(ingIndex))});
                
                Console.WriteLine();
                Console.WriteLine("More ingredients? 1 = yes | 2 = no");
                if (Console.ReadLine().Contains("2"))
                {
                    ingredientDone = true;
                }
            }

            drinkManager.AddDrink(drink);
        }

        static void UpdateDrink(int id = -1)
        {
            
            string choiceDrink = id.ToString();
            if (id == -1) 
            {
                ViewAllDrink();
                Console.WriteLine("Choice drink: ");
                choiceDrink = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine($"Choiced drink: {choiceDrink}");
            Drink drink = drinkManager.GetDrink(int.Parse(choiceDrink));
            ViewDrink(drink.ID);
            Console.WriteLine();
            Console.WriteLine("Action: ");
            Console.WriteLine("1. Add ingrediet");
            Console.WriteLine("2. Remove ingrediet");
            Console.WriteLine("3. Rename");
            Console.WriteLine("4. Edit Glass Type");
            Console.WriteLine("5. Back");
            string choice = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine();
                    ViewAllIngredients();
                    Console.WriteLine("Choice a ingredient: ");
                    string ingIndex = Console.ReadLine();

                    Console.WriteLine("Which unite?");
                    string unite = Console.ReadLine();

                    Console.WriteLine("How much? ");
                    string number = Console.ReadLine();

                    drink.AmountIngredient.Add(new AmountIngredient() { Amount = amountManager.GetAmount(float.Parse(number), unite), Ingredient = ingredientManager.GetIngredient(int.Parse(ingIndex)) });

                    Console.WriteLine();
                    Console.WriteLine("More ingredients? 1 = yes | 2 = no");
                    break;
                case "2":

                    foreach (var item in drink.AmountIngredient)
                    {
                        Console.WriteLine($"{item.IngredientID}. {item.Ingredient.Name} {item.Amount.Number} {item.Amount.Unite}");
                    }
                    string removeing = Console.ReadLine();

                    drink.AmountIngredient = drink.AmountIngredient.Where(a => a.IngredientID != int.Parse(removeing)).ToList();

                    break;
                case "3":
                    drink.Name = Console.ReadLine();
                    break;
                case "4":
                    for (int i = 0; i < Enum.GetNames(typeof(Drink.DrinkType)).Length; i++)
                    {
                        Console.WriteLine($"{i}. {Enum.GetNames(typeof(Drink.DrinkType))[i]}");
                    }

                    Console.WriteLine("Choice a Glass type: ");
                    drink.Type = (Drink.DrinkType)Enum.Parse(typeof(Drink.DrinkType), Console.ReadLine());
                    break;
                case "5":
                    return;
                    break;
            }

            drinkManager.UpdateDrink(drink);
            UpdateDrink(drink.ID);
        }

        static void ViewDrink(int id)
        {
            Drink drink = drinkManager.GetDrink(id);

            Console.Write($"{drink.ID}. Name: {drink.Name}");
            Console.WriteLine("Ingredients: ");
            foreach (var item in drink.AmountIngredient)
            {
                Console.WriteLine($"{item.Ingredient.Name} {item.Amount.Number} {item.Amount.Unite}");
            }

            Console.WriteLine();

        }

        static void RemoveDrink()
        {
            ViewAllDrink();
            Console.WriteLine("Choice one to delete: ");
            string choice = Console.ReadLine();
            drinkManager.RemoveDrink(drinkManager.GetDrink(int.Parse(choice)));
        }
    }
}