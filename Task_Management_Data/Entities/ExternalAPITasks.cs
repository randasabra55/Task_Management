using System.ComponentModel.DataAnnotations.Schema;
using Task_Management_Data.Entities.Identity;

namespace Task_Management_Data.Entities
{
    public class ExternalAPITasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ExternalSource { get; set; } = "Google Tasks";
        public DateTime? DueDate { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string ExternalTaskId { get; set; }
        /*[ForeignKey("Taskss")]
        public int TaskId { get; set; }
        public virtual Taskss Taskss { get; set; }*/
    }
}
