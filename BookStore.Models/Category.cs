﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Range(minimum:1,maximum:100, ErrorMessage = "{0} moet tussen {1} en {2} liggen.")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [Display(Name = "Volgnummer")]
        public int DisplayOrder { get; set; }

        [Display(Name="Aanmaakdatum")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public List<Product>? Products { get; set; }
    }
}
