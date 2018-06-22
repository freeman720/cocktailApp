using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailDAL.DbModel
{
    [Table("liquid")]
    public class Liquid
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(45)]
        public string Name { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required, DefaultValue(1)]
        public int IsActive { get; set; }
    }
}