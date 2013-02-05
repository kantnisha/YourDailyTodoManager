using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DailyToDoManager.Entities;

namespace DailyToDoManager.DataAccessLayer
{
    public interface IToDoRepository
    {
        IEnumerable<Task> GetAllTasks(string user);
        Task GetTaskById(int? id, string user);
        bool DeleteTask(int? id, string user);
        bool MarkTaskComplete(int? id, string user);
        bool AddTask(Task task, string user);
        void SaveDb();
    }
}
