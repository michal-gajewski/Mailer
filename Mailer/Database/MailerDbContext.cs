using Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class MailerDbContext : DbContext
    {
        public MailerDbContext(DbContextOptions<MailerDbContext> options) : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailRecipient> EmailRecipients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasMany(c => c.Recipients)
                .WithOne(e => e.Email);

            base.OnModelCreating(modelBuilder);
        }
    }
}