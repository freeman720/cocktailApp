using System.Collections.Generic;
using System.Linq;

namespace CocktailDAL.Model
{
    public class Cocktail
    {
        public string Name { get; set; }
        public List<Ingredient> Alcools { get; set; }
        public List<Ingredient> Softs { get; set; }
        public List<Ingredient> Ingredients => Alcools.Concat(Softs).ToList();
        public List<string> InactiveIngredients => Ingredients.Where(i => !i.IsActive).Select(i => i.Name).ToList();
        public bool IsActive => !InactiveIngredients.Any();
    }
}
