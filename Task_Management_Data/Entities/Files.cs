using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management_Data.Entities
{
    public class Files
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }


        [ForeignKey("Taskss")]
        public int? TaskId { get; set; }
        public virtual Taskss? Taskss { get; set; }
    }
}
