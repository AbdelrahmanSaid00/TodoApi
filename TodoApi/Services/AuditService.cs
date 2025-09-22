using TodoApi.Data;
using TodoApi.interfaces;
using TodoApi.Model;

namespace TodoApi.Services
{
    public class AuditService : IAuditService
    {
        private readonly TodoDbContext _context;
        public AuditService(TodoDbContext context)
        {
            _context = context;
        }
        public async Task LogActionAsync(string userId, string action, string details, string ipAddress)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Details = details,
                IpAddress = ipAddress
            };
            _context.auditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
