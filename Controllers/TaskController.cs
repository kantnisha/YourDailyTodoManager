namespace DailyToDoManager.Controllers
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Web.Mvc;
    using DailyToDoManager.Attributes;

    [Authorize]
    [NoCache]
    public class TaskController : Controller
    {
        TododbEntities tasks = new TododbEntities();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllTask()
        {
            var alltasks = tasks.Tasks.Where(t => t.TaskUsers.FirstOrDefault().UserId == User.Identity.Name).OrderByDescending(t => t.CreatedDate).ToList();
            return PartialView("AllTasks", alltasks);
        }

        [HttpPost]
        public ActionResult AddTask(string task)
        {
            if (!string.IsNullOrEmpty(task))
            {
                var taskInput = new Task();
                taskInput.Todo = task;
                taskInput.State = "0";
                taskInput.CreatedDate = DateTime.Now;
                try
                {
                    tasks.Tasks.Add(taskInput);
                    tasks.SaveChanges();
                    var taskUser = new TaskUser { TaskId = taskInput.TaskId, UserId = User.Identity.Name };
                    tasks.TaskUsers.Add(taskUser);
                    int i = tasks.SaveChanges();
                }
                catch (DbUpdateException exp)
                { }
            }
            return RedirectToAction("GetAllTask");
        }

        [HttpPost]
        public ActionResult DeleteTask(int? taskId)
        {
            if (taskId != null)
            {
                var taskToDelete = tasks.Tasks.Where(t => t.TaskId == taskId).FirstOrDefault();
                var taskUserEntry = tasks.TaskUsers.Where(t => t.TaskId == taskId).FirstOrDefault();
                if (taskToDelete != null && taskUserEntry != null)
                {
                    try
                    {
                        tasks.TaskUsers.Remove(taskUserEntry);
                        tasks.Tasks.Remove(taskToDelete);
                        tasks.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    { 
                    }
                }
            }

            return RedirectToAction("GetAllTask");
        }

        [HttpPost]
        public ActionResult CompleteTask(int? taskId)
        {
            if (taskId != null)
            {
                var taskToComplete = tasks.Tasks.Where(t => t.TaskId == taskId).FirstOrDefault();
                taskToComplete.State = "1";
                if (taskToComplete != null)
                {
                    try
                    {
                        tasks.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                    }
                }
            }

            return RedirectToAction("GetAllTask");
        }

    }
}
