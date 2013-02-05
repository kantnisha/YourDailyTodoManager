namespace DailyToDoManager.DataAccessLayer
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using DailyToDoManager.Entities;
    using System.Data.Entity.ModelConfiguration.Conventions;
    
    public class ToDoDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }
    }
}
