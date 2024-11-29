
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DisplayName("Soort Kaft")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Name { get; set; }
    }
}
