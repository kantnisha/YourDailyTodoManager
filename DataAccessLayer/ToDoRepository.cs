namespace DailyToDoManager.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using DailyToDoManager.Entities;

    public class ToDoRepository:IToDoRepository
    {
        ToDoDbContext db = new ToDoDbContext();

        public IEnumerable<Task> GetAllTasks(string user)
        {
            return db.Tasks
                .Where(t => t.TaskUsers.FirstOrDefault().UserId == user)
                .OrderByDescending(t => t.CreatedDate).ToList();
        }

        public Entities.Task GetTaskById(int? id, string user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTask(int? id, string user)
        {
            var taskToDelete = db.Tasks.Where(t => t.TaskId == id).FirstOrDefault();
            var taskUserEntry = db.TaskUsers.Where(t => t.TaskId == id).FirstOrDefault();
            if (taskToDelete != null && taskUserEntry != null)
            {
                try
                {
                    db.TaskUsers.Remove(taskUserEntry);
                    db.Tasks.Remove(taskToDelete);
                    
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return false;
                }
            }

            return false;
        }

        public bool MarkTaskComplete(int? id, string user)
        {
            var taskToComplete = db.Tasks.Where(t => t.TaskId == id).FirstOrDefault();
            
            if (taskToComplete != null)
            {
                taskToComplete.State = "1";
                return true;
            }

            return false;
        }

        public bool AddTask(Task task, string user)
        {
            task.State = "0";
            task.CreatedDate = DateTime.Now;
            try
            {
                db.Tasks.Add(task);
                var taskUser = new TaskUser { TaskId = task.TaskId, UserId = user };
                db.TaskUsers.Add(taskUser);
                return true;
            }
            catch (DbUpdateException exp)
            {
                return false;
            }
        }

        public void SaveDb()
        {
            this.db.SaveChanges();
        }
    }
}
