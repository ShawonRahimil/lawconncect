using System.ComponentModel.DataAnnotations.Schema;

namespace lawconncect.Models
{
    public class Documents
    {
        public int Id { get; set; }
        [Column("DocPath")]
        public string CaseDocument { get; set; }
        [NotMapped]
        public IFormFile Document { get; set; }
    }
}
