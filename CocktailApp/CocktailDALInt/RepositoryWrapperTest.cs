using System.Collections.Generic;
using System.Linq;
using CocktailDAL;
using CocktailDAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailDALInt
{
    [TestClass]
    public class RepositoryWrapperTest
    {
        private const string CocktailName = "White Russian";
        private const string AlcoolName = "Vodka";
        private const string AlcoolQuantity = "1/3";
        private const string OtherAlcoolName = "Gin";
        private const string OtherSoftName = "Jus d'orange";
        private const string SoftName = "Lait";
        private const string SoftQuantity = "2/3";
        private RepositoryWrapper instance;

        [TestInitialize]
        public void Before()
        {
            instance = new RepositoryWrapper(new RepositoryContext(new DbContextOptionsBuilder<RepositoryContext>().UseMySql(Properties.Resources.mysqlconnection).Options));

            instance.InsertAlcool(AlcoolName);
            instance.InsertSoft(SoftName);
        }

        [TestCleanup]
        public void After()
        {
            instance.CleanUp();
        }

        [TestMethod]
        public void InsertAddIngredientAndFindCocktail()
        {
            var newCocktail = new Cocktail
            {
                Name = CocktailName,
                Alcools = new List<Ingredient> { new Ingredient { Name = AlcoolName, Quantity = AlcoolQuantity } },
                Softs = new List<Ingredient> { new Ingredient { Name = SoftName, Quantity = SoftQuantity } }
            };

            instance.InsertCocktail(newCocktail);

            var cocktail = instance.GetCocktails().Single();
            Assert.AreEqual(CocktailName, cocktail.Name);
            Assert.AreEqual(AlcoolName, cocktail.Alcools.Single().Name);
            Assert.AreEqual(AlcoolQuantity, cocktail.Alcools.Single().Quantity);
            Assert.AreEqual(SoftName, cocktail.Softs.Single().Name);
            Assert.AreEqual(SoftQuantity, cocktail.Softs.Single().Quantity);
            Assert.IsTrue(cocktail.IsActive);
            Assert.IsFalse(cocktail.InactiveIngredients.Any());

            instance.DeactivateLiquid(AlcoolName);

            cocktail = instance.GetCocktails().Single();
            Assert.AreEqual(CocktailName, cocktail.Name);
            Assert.AreEqual(AlcoolName, cocktail.Alcools.Single().Name);
            Assert.AreEqual(SoftName, cocktail.Softs.Single().Name);
            Assert.IsFalse(cocktail.IsActive);
            Assert.AreEqual(AlcoolName, cocktail.InactiveIngredients.Single());
        }

        [TestMethod]
        public void InsertFindDeactivateAndReactivateAlcool()
        {
            instance.InsertAlcool(OtherAlcoolName);
            var alcools = instance.GetAlcools();
            Assert.AreEqual(2, alcools.Count);
            Assert.AreEqual(OtherAlcoolName, alcools.First());
            Assert.AreEqual(AlcoolName, alcools.Skip(1).First());

            instance.DeactivateLiquid(OtherAlcoolName);
            alcools = instance.GetAlcools();
            Assert.AreEqual(AlcoolName, alcools.Single());

            instance.ActivateLiquid(OtherAlcoolName);
            alcools = instance.GetAlcools();
            Assert.AreEqual(2, alcools.Count);
            Assert.AreEqual(OtherAlcoolName, alcools.First());
            Assert.AreEqual(AlcoolName, alcools.Skip(1).First());
        }

        [TestMethod]
        public void InsertFindDeactivateAndReactivateSoft()
        {
            instance.InsertSoft(OtherSoftName);
            var softs = instance.GetSofts();
            Assert.AreEqual(2, softs.Count);
            Assert.AreEqual(OtherSoftName, softs.First());
            Assert.AreEqual(SoftName, softs.Skip(1).First());

            instance.DeactivateLiquid(OtherSoftName);
            softs = instance.GetSofts();
            Assert.AreEqual(SoftName, softs.Single());

            instance.ActivateLiquid(OtherSoftName);
            softs = instance.GetSofts();
            
            Assert.AreEqual(2, softs.Count);
            Assert.AreEqual(OtherSoftName, softs.First());
            Assert.AreEqual(SoftName, softs.Skip(1).First());
        }
    }
}
