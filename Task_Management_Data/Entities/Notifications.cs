using System.ComponentModel.DataAnnotations.Schema;
using Task_Management_Data.Entities.Identity;

namespace Task_Management_Data.Entities
{
    public class Notifications
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
