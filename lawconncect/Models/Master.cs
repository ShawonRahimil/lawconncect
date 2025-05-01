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
      
        public Section Section { get; set; }
        public Adalot Adalot { get; set; }
        public ICollection<Details> Details { get; set; }
    }
}
