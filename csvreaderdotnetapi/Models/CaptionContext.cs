using Microsoft.EntityFrameworkCore;


namespace csvreaderdotnetapi.Models
{
    public class CaptionContext : DbContext
    {
        public CaptionContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Caption> Captions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caption>().HasData(new Caption
            {
                captionId = 1,
                imageName = "tempImg.jpg",
                commentNumber = 1,
                comment = "This is a test seeding data entry"
            });
        }
    }
}
