using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Infrastructure.option;

namespace TaskManagement.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().HasData(
                       new User
                       {
                           Id = 1,
                           FullName = "Admin User",
                           Email = "admin@demo.com",
                           Role = UserRole.Admin,
                           PasswordHash = "SL8ER7WcFtIG1QmtPrT+747wyY6kYW1GtULa8ZRiviU="
                       },
                       new User
                       {
                           Id = 2,
                           FullName = "Manager User",
                           Email = "manager@demo.com",
                           Role = UserRole.Manager,
                           PasswordHash = "mGKVG/Vj9Yx824WXv62/coJZTFNIFS0Oy+7AvP7PztM="
                       },
                       new User
                       {
                           Id = 3,
                           FullName = "Employee User",
                           Email = "employee@demo.com",
                           Role = UserRole.Employee,
                           PasswordHash = "J1DYT9Uwu+P6MHMgldjmgmX+AmfHiGpH6LaYi5zCGqY="
                       }
                   ); 

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.Property(t => t.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(t => t.Description)
                  .HasMaxLength(1000);

            entity.Property(t => t.Status)
                  .IsRequired();

            entity.HasOne(t => t.AssignedToUser)
                  .WithMany(u => u.AssignedTasks)
                  .HasForeignKey(t => t.AssignedToUserId)
                  .OnDelete(DeleteBehavior.Restrict); 

          
            entity.HasOne(t => t.Team)
                  .WithMany(team => team.Tasks)
                  .HasForeignKey(t => t.TeamId)
                  .OnDelete(DeleteBehavior.Cascade); 
        }); 
        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(t => t.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(t => t.Description)
                  .HasMaxLength(500);
        });
         
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.FullName)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(u => u.PasswordHash)
                  .IsRequired();
        });
    }


}
