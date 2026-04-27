using Microsoft.EntityFrameworkCore;
using PMS.Domain.Entities;
// A class that tells EF Core:" These are the tables I want in my database, and how they relate to each other."
namespace PMS.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } // Represent a table called Users for the User entity
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite Key for ProjectMember
        modelBuilder.Entity<ProjectMember>()
            .HasKey(pm => new { pm.UserId, pm.ProjectId }); // ProjectMember doesn't have an Id. So, we use (UserId + ProjectId) together as the unique key
                                                            // Because a user can only be a member of a project once, so the combination of UserId and ProjectId must be unique.

        // Relationships

        // Each Project Member has one User 
        // One user can have many Project Members
        // One to Many relationship between User and ProjectMember
        modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.User)
            .WithMany(u => u.ProjectMembers)
            .HasForeignKey(pm => pm.UserId);


        // Each Project Member has one Project
        // One Project can have many Project Members
        // One to Many relationship between Project and ProjectMember 
        // and many to many relationship between User and Project through ProjectMember
        modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.Project)
            .WithMany(p => p.Members)
            .HasForeignKey(pm => pm.ProjectId);

        // A TaskItem belongs to one Project, and a Project can have many TaskItems.
        // So, we set up a one-to-many relationship between TaskItem and Project.
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade); // If a Project is deleted, we want to delete all its TaskItems as well.

        // TaskItem can be assigned to one User
        // A User can have many assigned TaskItems.
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.AssignedUser)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedUserId)
            .OnDelete(DeleteBehavior.Restrict); // prevents cascade issues. 
                                                // If a user is deleted, we don't want to delete all their assigned tasks

        // Comment belongs to one TaskItem
        // A TaskItem can have many Comments
        // One to Many relationship between Comment and TaskItem
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.TaskItem)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade); // If a TaskItem is deleted, we want to delete all its comments as well.

        // By default, EF Core will store enums as integers in the database. This is fine, but it can make the data less readable when you look at it directly in the database.
        // By using HasConversion<string>(), we tell EF Core to store the enum values as their string representations instead of integers.
        // This way, if you look at the database, you'll see "Admin" or "Member" instead of 0 or 1.
        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Status)
            .HasConversion<string>();

        modelBuilder.Entity<ProjectMember>()
              .Property(pm => pm.Role)
              .HasConversion<string>();



        // We have already defined relationships in classes, but EF sometimes needs explict instructions when:
        // They are many to many relationships (like User <-> Project through ProjectMember)
        // You want control (like, cascading delete or composite key)
    }
}