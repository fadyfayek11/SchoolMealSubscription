using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMealSubscription.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        public double Price { get; set; }

        [ValidateNever]
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public Category? Category { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; } = "";

        [ValidateNever]
        public int? ClubId { get; set; }
        [ForeignKey(nameof(ClubId))]
        [ValidateNever]

        public Club? Club { get; set; }

        [ValidateNever]
        public ICollection<BookComments>? BookComments { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
