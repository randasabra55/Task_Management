using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Projectss.Commands.Models
{
    public class EditProjectCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
