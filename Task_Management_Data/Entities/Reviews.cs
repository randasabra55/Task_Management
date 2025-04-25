using System.ComponentModel.DataAnnotations.Schema;
using Task_Management_Data.Entities.Identity;

namespace Task_Management_Data.Entities
{
    public class Reviews
    {
        public int Id { get; set; }
        public string Comment { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Taskss")]
        public int TaskId { get; set; }
        public virtual Taskss Taskss { get; set; }
    }
}
