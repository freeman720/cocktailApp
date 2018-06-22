using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailDAL.DbModel
{
    [Table("soft")]
    public class Soft
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(45)]
        public string Name { get; set; }
    }
}
