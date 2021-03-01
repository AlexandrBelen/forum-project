using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PaperID { get; set; }
        public IdentityUser Author { get; set; }
        public string Body { get; set; }
        public IdentityUser ForUser { get; set; }
        public List<Comment> SubComments { get; set; }

    }
}
