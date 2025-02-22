using System.ComponentModel.DataAnnotations;

namespace Linqgroupbymethod
{
    public class Records
    {
        [Key]
        public int RecordID { get; set; }
        public int StudentID { get; set; } // Foreign Key
        public int Tamil { get; set; }
        public int English { get; set; }
        public int Maths { get; set; }
        public int Science { get; set; }
        public int Social { get; set; }
    }
}
