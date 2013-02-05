using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyToDoManager.DataAccessLayer;

namespace DailyToDoManager.Controllers
{
    public class ToDoBaseController : Controller
    {
        protected IToDoRepository repository;
        public ToDoBaseController(IToDoRepository todoRepository)
        {
            this.repository = todoRepository;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.repository.SaveDb();
            base.OnActionExecuted(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}
