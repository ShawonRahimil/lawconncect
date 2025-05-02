using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace lawconncect.Models
{
    public class Lawyer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
     
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    
        public string Description { get; set; }
    }
}
