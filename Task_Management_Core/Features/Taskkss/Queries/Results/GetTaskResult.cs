namespace Task_Management_Core.Features.Taskkss.Queries.Results
{
    public class GetTaskResult
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }


        public int ProjectId { get; set; }
        public string UserId { get; set; }

        //  public virtual ICollection<Files> Files { get; set; }
    }
}
