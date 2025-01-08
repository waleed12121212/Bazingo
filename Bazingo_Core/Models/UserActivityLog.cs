using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class UserActivityLog
    {
        public int ActivityID { get; set; } // Primary Key
        public string ActivityType { get; set; }
        public DateTime ActivityTime { get; set; }
        public string IPAddress { get; set; }
        public string DeviceInfo { get; set; }
        public string AdditionalInfo { get; set; }

        // Foreign Key
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
