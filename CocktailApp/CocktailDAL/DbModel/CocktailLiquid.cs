using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailDAL.DbModel
{
    [Table("cocktailliquid")]
    public class CocktailLiquid
    {
        public int CocktailId { get; set; }

        public int LiquidId { get; set; }

        [StringLength(45)]
        public string Quantity { get; set; }

    }
}
