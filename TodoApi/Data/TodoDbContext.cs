using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Model;

namespace TodoApi.Data
{
    public class TodoDbContext : IdentityDbContext<IdentityUser , IdentityRole , string>
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<Todo> todos { get; set; }
        public DbSet<AuditLog> auditLogs { get; set; }
    }
}
