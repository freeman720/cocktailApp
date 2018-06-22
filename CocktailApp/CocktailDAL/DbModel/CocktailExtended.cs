using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailDAL.DbModel
{
    [Table("cocktailextended")]
    public class CocktailExtended
    {
        [Key, StringLength(36)]
        public string Id { get; set; }

        [Required, StringLength(45)]
        public string CocktailName { get; set; }

        [Required, StringLength(45)]
        public string LiquidName { get; set; }

        [Required, StringLength(45)]
        public string Type { get; set; }

        [StringLength(45)]
        public string Quantity { get; set; }

        [Required]
        public int IsActive { get; set; }
    }
}
