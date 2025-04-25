using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Taskkss.Commands.Models
{
    public class AddTaskCommand : IRequest<Response<string>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public int ProjectId { get; set; }

        public string UserId { get; set; }


    }
}
