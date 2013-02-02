namespace DailyToDoManager.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class TaskUser
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }
        [Key]
        public int TaskUserMapId { get; set; }
    
        public virtual Task Task { get; set; }
    }
}
