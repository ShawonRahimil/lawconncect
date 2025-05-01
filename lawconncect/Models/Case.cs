using System.ComponentModel.DataAnnotations.Schema;

namespace lawconncect.Models
{
    public class Case
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
      

    }

}
