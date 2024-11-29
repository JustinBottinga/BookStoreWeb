using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [MaxLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Titel")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Omschrijving")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:\D*\d{3})?$)[\d-]+$")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [MaxLength(100), Display(Name = "Schrijver")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [Display(Name = "Catalogusprijs")]
        public double ListPrice { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [Display(Name = "Prijs")]
        public double Price { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [Display(Name = "Prijs bij 50+ afname")]
        public double Price50 { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [Display(Name = "Prijs bij 100+ afname")]
        public double Price100 { get; set; }

        public string? ImageUrl { get; set; }


        // Navigation Properties
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [Display(Name = "Soort Kaft")]
        public int CoverTypeId { get; set; }
        public CoverType? CoverType { get; set; }
    }
}
