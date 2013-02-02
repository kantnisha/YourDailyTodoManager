using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DailyToDoManager.Entities;
using System.Data.Entity.Infrastructure;

namespace DailyToDoManager.DataAccessLayer
{
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
                    db.SaveChanges();
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
            taskToComplete.State = "1";
            if (taskToComplete != null)
            {
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return false;
                }
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
                db.SaveChanges();
                var taskUser = new TaskUser { TaskId = task.TaskId, UserId = user };
                db.TaskUsers.Add(taskUser);
                int i = db.SaveChanges();
                return true;
            }
            catch (DbUpdateException exp)
            {
                return false;
            }
        }
    }
}
