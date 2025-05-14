using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace lawconncect.Models
{
    public class Master
    {
        public int Id { get; set; }
    public string CaseNumber { get; set; }
       
      
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        [ForeignKey("Adalot")]
        public int AdalotId { get; set; }
        [ValidateNever]
        public Section Section { get; set; }
        [ValidateNever]
        public Adalot Adalot { get; set; }
        [ValidateNever]
        public ICollection<Details> Details { get; set; }
    }
}
