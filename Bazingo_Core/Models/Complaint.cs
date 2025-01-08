using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Complaint
    {
        public int ComplaintID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public string Description { get; set; }
        public ComplaintStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum ComplaintStatus
    {
        Open,
        InProgress,
        Resolved
    }

}
