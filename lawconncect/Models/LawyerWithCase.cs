using System.ComponentModel.DataAnnotations.Schema;

namespace lawconncect.Models
{
    public class LawyerWithCase

    {
        public enum person
        {
            badhi,
            bibadhi
        }
        public int Id { get; set; }
        [ForeignKey("Case")]
        public int CaseId { get; set; }
        public Case Case{ get; set; }
        [ForeignKey("lawyer")]
        public int lawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
      

    }
}
