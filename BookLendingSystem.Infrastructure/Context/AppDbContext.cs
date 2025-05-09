using BookLendingSystem.Domain.Entites;
using BookLendingSystem.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookLendingSystem.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookBorrowing> BookBorrowings => Set<BookBorrowing>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.BorrowedByUser)
                .WithMany()
                .HasForeignKey(b => b.BorrowedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
