namespace DailyToDoManager.DataAccessLayer
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using DailyToDoManager.Entities;
    
    public class ToDoDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }
    }
}
