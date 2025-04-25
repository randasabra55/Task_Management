namespace Task_Management_Core.Features.Taskkss.Queries.Results
{
    public class GetAllTasksResult
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Source { get; set; }
    }
}
