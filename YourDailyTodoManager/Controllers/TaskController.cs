namespace DailyToDoManager.Controllers
{
    using System.Web.Mvc;
    using DailyToDoManager.Attributes;
    using DailyToDoManager.DataAccessLayer;
    using DailyToDoManager.Entities;

    [Authorize]
    [NoCache]
    public class TaskController : ToDoBaseController
    {
        public TaskController(IToDoRepository repository)
            : base(repository)
        { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllTask()
        {
            var alltasks = repository.GetAllTasks(User.Identity.Name);
            return PartialView("AllTasks", alltasks);
        }

        [HttpPost]
        public ActionResult AddTask(string task)
        {
            
            if (!string.IsNullOrEmpty(task))
            {
                repository.AddTask(new Task { Todo = task }, User.Identity.Name);
            }

            return RedirectToAction("GetAllTask");
        }

        [HttpPost]
        public ActionResult DeleteTask(int? taskId)
        {
            if (taskId != null)
            {
                repository.DeleteTask(taskId, User.Identity.Name);
            }

            return RedirectToAction("GetAllTask");
        }

        [HttpPost]
        public ActionResult CompleteTask(int? taskId)
        {
            if (taskId != null)
            {
                repository.MarkTaskComplete(taskId, User.Identity.Name);
            }

            return RedirectToAction("GetAllTask");
        }

    }
}
