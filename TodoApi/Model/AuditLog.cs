using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace TodoApi.Model
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public string IpAddress { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public IdentityUser User { get; set; }
    }
}
