using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShopDto.SecondTask;

namespace WebShopAPI.SecondTask
{
    public class BalancerService
    {
        SecondTaskContext _context = new SecondTaskContext();

        private const string toDoTaskStatus = "todo";
        private const string canceledTaskStatus = "canceled";
        private const string pausedTaskStatus = "paused";

        public int AddNewTask(TaskDTO task)
        {
            int serverId = GetServerIdOfServerWithTheLargestCapacity();

            var taskModel = new Task
            {
                Condition = task.Condition,
                ElementsCalculated = 0,
                PercentageStatus = 0,
                Result = string.Empty,
                Status = toDoTaskStatus,
                UserEmail = task.UserEmail,
                ServerId = serverId
            };
            _context.Tasks.Add(taskModel);
            
            var server = _context.ServerCapacities.FirstOrDefault(sc => serverId == sc.ServerId);
            server.TaskCount++;
            _context.SaveChanges();

            return taskModel.Id;
        }

        public void CancelTask(int id, string userName)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.UserEmail == userName && t.Id == id);
            if(task != null)
            {
                var server = _context.ServerCapacities.FirstOrDefault(sc => task.ServerId == sc.ServerId);

                server.TaskCount = task.Status != canceledTaskStatus ? server.TaskCount - 1 : server.TaskCount;
                task.Status = canceledTaskStatus;
                task.PercentageStatus = 0;

                _context.SaveChanges();
            }
        }

        public void PauseTask(int id, string userName)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.UserEmail == userName && t.Id == id);
            if (task != null)
            {
                var server = _context.ServerCapacities.FirstOrDefault(sc => task.ServerId == sc.ServerId);

                server.TaskCount = task.Status != pausedTaskStatus ? server.TaskCount - 1 : server.TaskCount;
                task.Status = pausedTaskStatus;
                _context.SaveChanges();
            }
        }

        public void ResumeTask(int id, string userName)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.UserEmail == userName && t.Id == id);
            var server = _context.ServerCapacities.FirstOrDefault(t => t.ServerHostName == t.ServerHostName);
            if (task != null)
            {
                task.Status = toDoTaskStatus;
                server.TaskCount++;
                _context.SaveChanges();
            }
        }

        public int GetTaskStatus(int id, string userName)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.UserEmail == userName && t.Id == id);
            return task.PercentageStatus.Value;
        }

        public List<int> GetUserTaskIds(string userName)
        {
            return _context.Tasks.Where(t => t.UserEmail == userName).Select(t=>t.Id).ToList();
        }

        public TaskDTO GetTaskInfo(int id, string userName)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.UserEmail == userName && t.Id == id);
            var server = _context.ServerCapacities.FirstOrDefault(f => f.ServerId == task.ServerId);
            return new TaskDTO
            {
                Condition = task.Condition,
                Result = task.Result,
                ServerId = task.ServerId,
                ServerHostName = server.ServerHostName,
                TaskId = task.Id,
                UserEmail = task.UserEmail,
                PercentageStatus = task.PercentageStatus
            };
        }

        public int GetTaskForServer(int serverId)
        {
            var server = _context.Tasks.FirstOrDefault(t => t.ServerId == serverId && (t.Status.ToLower() == toDoTaskStatus));
            return server!= null ? server.Id : 0;
        }

        public List<TaskDTO> GetUserTasksInfo(string userName)
        {
            return _context.Tasks.Where(t => t.UserEmail == userName)
                .Select(task => new TaskDTO
                {
                    Condition = task.Condition,
                    Result = task.Result,
                    ServerHostName = task.ServerId.ToString(),
                    TaskId = task.Id,
                    UserEmail = task.UserEmail,
                    PercentageStatus = task.PercentageStatus
                }).ToList();
        }

        public void GenerateAndCalculateRandomMatrixes(string userName, int n)
        {
            string condition = string.Empty;

            var fisrtMatrix = GetRandomMatrix(n);
            var secondMatrix = GetRandomMatrix(n);

            condition = WriteMatrixToString(fisrtMatrix) + '*' + WriteMatrixToString(secondMatrix);

            int serverId = GetServerIdOfServerWithTheLargestCapacity();

            _context.Tasks.Add(new Task
            {
                Condition = condition,
                ElementsCalculated = 0,
                PercentageStatus = 0,
                Result = string.Empty,
                Status = toDoTaskStatus,
                UserEmail = userName,
                ServerId = serverId
            });

            var server = _context.ServerCapacities.FirstOrDefault(sc => serverId == sc.ServerId);
            server.TaskCount++;
            _context.SaveChanges();
        }

        public List<List<int>> GetRandomMatrix(int n)
        {
            List<List<int>> matrix = new List<List<int>>();

            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                matrix.Add(new List<int>());
                for (int j = 0; j < n; j++)
                {
                    matrix[i].Add(random.Next(0, 100));
                }
            }

            return matrix;
        }

        public string WriteMatrixToString(List<List<int>> resultMatrix)
        {
            string result = string.Empty;
            resultMatrix.ForEach(r =>
            {
                r.ForEach(element =>
                {
                    result += element.ToString() + ",";
                });
                int lastComaIndex = result.LastIndexOf(',');
                result = result.Substring(0, result.Length - 1);
                result += '|';
            });
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        public int GetServerIdOfServerWithTheLargestCapacity()
        {
            var server = _context.ServerCapacities.OrderBy(sc => sc.TaskCount).FirstOrDefault();
            return server.Id;
        }
    }
}