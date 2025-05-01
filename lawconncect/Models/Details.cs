using System.ComponentModel.DataAnnotations.Schema;

namespace lawconncect.Models
{
    public class Details
    {
        public int Id { get; set; }


        public DateTime HieringDate { get; set; }
        public DateTime NextHieringDate { get; set; }
        public string Hiering { get; set; }
        public string Comments { get; set; }
        [ForeignKey("Documents")]
        public int DocumentsId { get; set; }
        public Documents Documents { get; set; }

        [ForeignKey("LawyerWithCase")]
        public int LawyerWithCaseId { get; set; }
        public LawyerWithCase LawyerWithCase { get; set; }

        [ForeignKey("Master")]
        public int MasterId { get; set; }
        public Master Master { get; set; }

    }
}
