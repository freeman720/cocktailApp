namespace CocktailDAL.Model
{
    public class LiquidType
    {
        private LiquidType(string value) { Value = value; }

        public string Value { get; set; }

        public static LiquidType Alcool => new LiquidType("Alcool");
        public static LiquidType Soft => new LiquidType("Soft");
    }
}
