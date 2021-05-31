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
    }
}