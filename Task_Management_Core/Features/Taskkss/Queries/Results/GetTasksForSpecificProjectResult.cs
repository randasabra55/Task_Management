

namespace Task_Management_Core.Features.Taskkss.Queries.Results
{
    public class GetTasksForSpecificProjectResult
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Filee> files { get; set; }
    }
    public class Filee
    {
        public string FileUrl { get; set; }
        public string FileName { get; set; }
    }
}
