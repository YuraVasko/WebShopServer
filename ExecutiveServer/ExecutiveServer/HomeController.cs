using System.Net.Http;
using System.Web.Http;

namespace ExecutiveServer
{
    public class HomeController : ApiController
    {
        EF.TaskService _taskService = new EF.TaskService();
        public string Get()
        {
            return "Hello World!";
        }

        [HttpGet]
        public HttpResponseMessage StartTask(int taskId)
        {
            _taskService.StartTask(taskId);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
