using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShopAPI.Identity;
using WebShopAPI.SecondTask;
using WebShopDto.SecondTask;

namespace WebShopAPI.Controllers
{
    public class MatrixMultiplyController : ApiController
    {
        BalancerService balancerService = new BalancerService();
        CustomJWTFormat _jwtFormat = new CustomJWTFormat();

        [HttpGet]
        [Route("add/random/task")]
        public HttpResponseMessage GenarateAndMultiplyRandomMatrixes(int n)
        {
            var userName = GetCurrentUserName();
            balancerService.GenerateAndCalculateRandomMatrixes(userName, n);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet]
        [Route("add/task")]
        public HttpResponseMessage MultiplyMatrixes(string condition)
        {
            var userName = GetCurrentUserName();
            var task = new TaskDTO
            {
                Condition = condition,
                UserEmail = userName
            };
            int taskId = balancerService.AddNewTask(task);
            var response = Request.CreateResponse(HttpStatusCode.OK, taskId);
            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("cancel/task")]
        public HttpResponseMessage CancelTask(int taskId)
        {
            var userName = GetCurrentUserName();
            balancerService.CancelTask(taskId, userName);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet]
        [Route("pause/task")]
        public HttpResponseMessage PauseTask(int taskId)
        {
            var userName = GetCurrentUserName();
            balancerService.PauseTask(taskId, userName);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet]
        [Route("resume/task")]
        public HttpResponseMessage ResumeTask(int taskId)
        {
            var userName = GetCurrentUserName();
            balancerService.ResumeTask(taskId, userName);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [Authorize]
        [HttpGet]
        [Route("details/task/{taskId}")]
        public HttpResponseMessage GetTaskInfo(int taskId)
        {
            var userName = GetCurrentUserName();
            var task = balancerService.GetTaskInfo(taskId, userName);
            var response = Request.CreateResponse<TaskDTO>(HttpStatusCode.OK, task);
            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("percentage/task/{taskId}")]
        public HttpResponseMessage GetTaskPercentage(int taskId)
        {
            var userName = GetCurrentUserName();
            int percentage = balancerService.GetTaskStatus(taskId, userName);
            var response = Request.CreateResponse<int>(HttpStatusCode.OK, percentage);
            return response;
        }

        private string GetCurrentUserName()
        {
            string encoded = Request.Headers.Authorization.Parameter;
            object currentUserName;
            _jwtFormat.GetDataFromEncodedTocken(encoded).Payload.TryGetValue("id", out currentUserName);
            return currentUserName as string;
        }

        [HttpGet]
        [Route("get/task/for/server/{serverId}")]
        public IHttpActionResult GetTaskForServer(int serverId)
        {
            int taskId =balancerService.GetTaskForServer(serverId);
            return Ok<int>(taskId);
        }

        [Authorize]
        [HttpGet]
        [Route("get/tasks")]
        public HttpResponseMessage GetUserTasks()
        {
            var userName = GetCurrentUserName();
            var taskIds = balancerService.GetUserTaskIds(userName);
            var response = Request.CreateResponse<List<int>>(HttpStatusCode.OK, taskIds);
            return response;
        }
    }
}
