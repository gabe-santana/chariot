using MediatR;

namespace MMGTS.Application.Notifications
{
    public class ErrorNotification : INotification
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
    }
}
