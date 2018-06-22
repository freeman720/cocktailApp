using System.Collections.Generic;
using System.Linq;
using CocktailDAL.DbModel;
using CocktailDAL.Model;
using Cocktail = CocktailDAL.DbModel.Cocktail;

namespace CocktailDAL
{
    public class RepositoryWrapper
    {
        private const int IsActive = 1;
        private readonly RepositoryContext repositoryContext;
        private RepositoryBase<Cocktail> cocktail;
        private RepositoryBase<CocktailExtended> cocktailExtended;
        private RepositoryBase<Liquid> liquid;
        private RepositoryBase<Alcool> alcool;
        private RepositoryBase<Soft> soft;
        private RepositoryBase<CocktailLiquid> cocktailLiquid;

        private RepositoryBase<Cocktail> Cocktail
        {
            get
            {
                if (cocktail == null)
                {
                    cocktail = new RepositoryBase<Cocktail>(repositoryContext);
                }

                return cocktail;
            }
        }

        private RepositoryBase<CocktailExtended> CocktailExtended
        {
            get
            {
                if (cocktailExtended == null)
                {
                    cocktailExtended = new RepositoryBase<CocktailExtended>(repositoryContext);
                }

                return cocktailExtended;
            }
        }

        private RepositoryBase<Liquid> Liquid
        {
            get
            {
                if (liquid == null)
                {
                    liquid = new RepositoryBase<Liquid>(repositoryContext);
                }

                return liquid;
            }
        }

        private RepositoryBase<Alcool> Alcool
        {
            get
            {
                if (alcool == null)
                {
                    alcool = new RepositoryBase<Alcool>(repositoryContext);
                }

                return alcool;
            }
        }

        private RepositoryBase<Soft> Soft
        {
            get
            {
                if (soft == null)
                {
                    soft = new RepositoryBase<Soft>(repositoryContext);
                }

                return soft;
            }
        }

        private RepositoryBase<CocktailLiquid> CocktailLiquid
        {
            get
            {
                if (cocktailLiquid == null)
                {
                    cocktailLiquid = new RepositoryBase<CocktailLiquid>(repositoryContext);
                }

                return cocktailLiquid;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public void CleanUp()
        {
            CocktailLiquid.Delete();
            CocktailLiquid.Save();
            Cocktail.Delete();
            Cocktail.Save();
            Liquid.Delete();
            Liquid.Save();
        }

        public void InsertCocktail(Model.Cocktail newCocktail)
        {
            Cocktail.Create(new Cocktail { Name = newCocktail.Name });
            Cocktail.Save();

            foreach (var ingredient in newCocktail.Ingredients)
            {
                repositoryContext.AddIngredient(newCocktail.Name, ingredient.Name, ingredient.Quantity);
            }
        }

        public List<Model.Cocktail> GetCocktails()
        {
            return CocktailExtended.Find().ToList().GroupBy(c => c.CocktailName).Select(
                c => new Model.Cocktail
                {
                    Name = c.Key,
                    Alcools = GetIngredient(c, LiquidType.Alcool),
                    Softs = GetIngredient(c, LiquidType.Soft)
                }
                ).ToList();
        }

        public void InsertAlcool(string name)
        {
            repositoryContext.InsertAlcool(name);
        }

        public void InsertSoft(string name)
        {
            repositoryContext.InsertSoft(name);
        }

        public void DeactivateLiquid(string name)
        {
            repositoryContext.DeactivateLiquid(name);
        }

        public void ActivateLiquid(string name)
        {
            repositoryContext.ActivateLiquid(name);
        }

        public List<string> GetAlcools()
        {
            return Alcool.Find().Select(a => a.Name).ToList();
        }

        public List<string> GetSofts()
        {
            return Soft.Find().Select(s => s.Name).ToList();
        }

        private static List<Ingredient> GetIngredient(IGrouping<string, CocktailExtended> groupedCocktail, LiquidType type)
        {
            return groupedCocktail
                .Where(i => i.Type == type.Value)
                .Select(i => new Ingredient
                {
                    Name = i.LiquidName,
                    IsActive = i.IsActive == IsActive,
                    Quantity = i.Quantity
                })
                .ToList();
        }
    }
}