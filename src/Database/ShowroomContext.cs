using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    internal class ShowroomContext: DbContext
    {
        public ShowroomContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Model> Models { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<EmailSubscriber> EmailSubscribers { get; set; }
        public DbSet<TelegramSubscriber> TelegramSubscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelegramSubscriber>().HasIndex(x => x.ChatId);

            base.OnModelCreating(modelBuilder);
        }
    }
}