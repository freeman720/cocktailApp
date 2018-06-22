using System.Data;
using CocktailDAL.DbModel;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace CocktailDAL
{
    public class RepositoryContext : DbContext
    {
        private const string InsertAlcoolProcedure = "insertAlcool";
        private const string InsertSoftProcedure = "insertSoft";
        private const string DeactivateLiquidProcedure = "deActivateLiquid";
        private const string ActivateLiquidProcedure = "activateLiquid";
        private const string AddIngredientProcedure = "addIngredient";
        private const string AlcoolNameParameter = "@AlcoolName";
        private const string SoftNameParameter = "@SoftName";
        private const string LiquidNameParameter = "@LiquidName";
        private const string CocktailNameParameter = "@CocktailName";
        private const string LiquidQuantityParameter = "@LiquidQuantity";

        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Alcool> Alcools { get; set; }
        public DbSet<CocktailExtended> CocktailExtendeds { get; set; }
        public DbSet<CocktailLiquid> CocktailLiquids { get; set; }
        public DbSet<Liquid> Liquids { get; set; }
        public DbSet<Soft> Softs { get; set; }

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CocktailLiquid>()
                .HasKey(c => new { c.CocktailId, c.LiquidId });
        }

        public void InsertAlcool(string name)
        {
            CallStoredProcedure(InsertAlcoolProcedure, AlcoolNameParameter, name);
        }

        public void InsertSoft(string name)
        {
            CallStoredProcedure(InsertSoftProcedure, SoftNameParameter, name);
        }

        public void DeactivateLiquid(string name)
        {
            CallStoredProcedure(DeactivateLiquidProcedure, LiquidNameParameter, name);
        }

        public void ActivateLiquid(string name)
        {
            CallStoredProcedure(ActivateLiquidProcedure, LiquidNameParameter, name);
        }

        public void AddIngredient(string cocktailName, string liquidName, string quantity)
        {
            CallStoredProcedure(AddIngredientProcedure, new[]
            {
                new MySqlParameter(CocktailNameParameter, cocktailName),
                new MySqlParameter(LiquidNameParameter, liquidName),
                new MySqlParameter(LiquidQuantityParameter, quantity)
            });
        }

        private void CallStoredProcedure(string procedureName, string paramName, string paramValue)
        {
            CallStoredProcedure(procedureName, new[] { new MySqlParameter(paramName, paramValue) });
        }

        private void CallStoredProcedure(string procedureName, MySqlParameter[] parameters)
        {
            var cmd = new MySqlCommand(procedureName)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = (MySqlConnection)Database.GetDbConnection()
            };

            cmd.Parameters.AddRange(parameters);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}