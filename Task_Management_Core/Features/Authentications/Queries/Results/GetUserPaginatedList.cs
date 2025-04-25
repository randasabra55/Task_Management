namespace Task_Management_Core.Features.Authentications.Queries.Results
{
    public class GetUserPaginatedList
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
