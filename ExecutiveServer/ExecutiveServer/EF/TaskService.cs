using System;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveServer.EF
{
    class TaskService
    {
        private const string hostName = "http://localhost:7777";
        private const string workingStatus = "working";
        private const string listeningStatus = "listening";
        private const string canceledTaskStatus = "canceled";
        private const string inProgressTaskStatus = "inprogress";
        private const string pausedTaskStatus = "paused";
        private const string toDoTaskStatus = "todo";
        private const string completedTaskStatus = "completed";

        DataParserService _dataParserSeervice = new DataParserService();
        public TaskService()
        {
            
        }

        public void StartTask(int taskId)
        {
            var task = GetTaskById(taskId);
            if (task != default(object))
            {
                List<List<int>> firstMatrix;
                List<List<int>> secondMatrix;
                _dataParserSeervice.GetMatrixesFromTaskCondition(task, out firstMatrix, out secondMatrix);
                var result = MultiplyMatrixes(firstMatrix, secondMatrix, firstMatrix.Count, task);
                SaveCalculatedElements(result, task.Id);
            }
        }

        public EF.Task GetTaskById(int id)
        {
            DbConectionEntities _context = new DbConectionEntities();
            var task = _context.Tasks.FirstOrDefault(t=>t.Id == id);
            if(task != default(object))
            {
                var serverInfo = _context.ServerCapacities.FirstOrDefault(sc => sc.ServerId == task.ServerId);
                if (serverInfo == null || serverInfo.ServerHostName == hostName)
                {
                    return task;
                }
            }
            return null;
        }

        public void SaveCalculatedElements(List<List<int>> resultMatrix, int taskId)
        {
            DbConectionEntities _context = new DbConectionEntities();
            var task = _context.Tasks.Find(taskId);
            string result = _dataParserSeervice.WriteMatrixToString(resultMatrix);
            task.Result = task.ElementsCalculated.HasValue && task.ElementsCalculated.Value != 0 ? task.Result + result : result;
            _context.SaveChanges();
        }

        public List<List<int>> MultiplyMatrixes(List<List<int>> A, List<List<int>> B, int N, EF.Task task)
        {
            List<List<int>> result = new List<List<int>>();

            int elementsCalculated = task.ElementsCalculated.HasValue ? task.ElementsCalculated.Value : 0;
            bool isCanceled = false;

            //var server = _context.ServerCapacities.FirstOrDefault(s => s.ServerHostName == hostName);
            //server.ServerStatus = workingStatus;

            int taskId = task.Id;

            int startRow = task.ElementsCalculated.HasValue ? task.ElementsCalculated.Value / N : 0;
            elementsCalculated = startRow * N;

            for(int i=0;i<startRow;i++)
            {
                result.Add(new List<int>());
            }
            for (int i = startRow; i < N && !isCanceled; i++)
            {
                result.Add(new List<int>(N));
                for (int j = 0; j < N && !isCanceled; j++)
                {
                    DbConectionEntities _context = new DbConectionEntities();
                    result[i].Add(0);
                    var tempTask = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
                    if (tempTask != default(object) && tempTask.Status.ToLower() == inProgressTaskStatus)
                    {
                        for (int k = 0; k < N; k++)
                        {
                            result[i][j] += A[i][k] * B[k][j];
                        }
                        elementsCalculated++;
                        tempTask.PercentageStatus =  Convert.ToInt32(Math.Ceiling(((elementsCalculated * 1.0) / (N * N) ) * 100));
                        tempTask.ElementsCalculated = elementsCalculated;
                        if(elementsCalculated == N*N)
                        {
                            tempTask.Status = completedTaskStatus;
                            var updatedContext = new DbConectionEntities();
                            var server = updatedContext.ServerCapacities.FirstOrDefault(s => s.ServerHostName == hostName);
                            server.TaskCount--;
                            updatedContext.SaveChanges();
                        }
                        _context.SaveChanges();
                    }
                    else
                    {
                        if (tempTask.Status.ToLower() == pausedTaskStatus)
                        {
                            tempTask.ElementsCalculated = (elementsCalculated / N) * N;
                        }
                        isCanceled = true;
                        //server.ServerStatus = "listening";
                        _context.SaveChanges();
                        break;
                    }
                }
            }
            return result;
        }
    }
}
