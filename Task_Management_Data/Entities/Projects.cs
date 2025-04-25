namespace Task_Management_Data.Entities
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }


        public virtual ICollection<Taskss> Tasksses { get; set; }
        public virtual ICollection<ProjectMembers> ProjectMembers { get; set; }

    }
}
