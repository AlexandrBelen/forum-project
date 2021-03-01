using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Paper
    {
        public int ID { get; set; }

        [StringLength(120, MinimumLength = 3)]
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(600, MinimumLength = 3)]
        public string Intro { get; set; }

        [Required]
        [Display(Name = "Body")]
        public string Body { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Author")]
        public IdentityUser Author { get; set; }

        [Required]
        [Display(Name = "Theme")]
        public string Themе { get; set; }

        public List<Comment> Comments { get; set; }
        public Paper()
        {
            DateTime = DateTime.Now;
        }
    }
}
