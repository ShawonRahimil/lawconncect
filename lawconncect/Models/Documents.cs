using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace lawconncect.Models
{
    public class Documents
    {
        public int Id { get; set; }
        [ValidateNever]
        public string DocPath { get; set; } = "";
        [NotMapped]
        [ValidateNever]
        public IFormFile Document { get; set; }
    }
}
