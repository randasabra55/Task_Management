using System.ComponentModel.DataAnnotations.Schema;
using Task_Management_Data.Entities.Identity;

namespace Task_Management_Data.Entities
{
    public class Taskss
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }


        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Projects Project { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
