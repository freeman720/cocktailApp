using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailDAL.DbModel
{
    [Table("alcool")]
    public class Alcool
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(45)]
        public string Name { get; set; }
    }
}
