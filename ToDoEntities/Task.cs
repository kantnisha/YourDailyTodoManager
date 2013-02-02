namespace DailyToDoManager.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Task
    {
        public Task()
        {
            this.TaskUsers = new HashSet<TaskUser>();
        }
    
        [Key]
        public int TaskId { get; set; }
        public string Todo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string State { get; set; }
    
        public virtual ICollection<TaskUser> TaskUsers { get; set; }
    }
}
