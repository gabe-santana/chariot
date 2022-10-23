using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMGTS.Application.Notifications
{
    public class ErrorNotification : INotification
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
    }
}
