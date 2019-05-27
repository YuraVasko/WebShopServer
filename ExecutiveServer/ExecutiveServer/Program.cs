using ExecutiveServer.EF;
using Microsoft.Owin.Hosting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace ExecutiveServer
{
    class Program
    {

        private static string serverHostName = "http://localhost:7777";
        private static string loadBalancerHostName = "http://localhost:57297";
        private static EF.TaskService _taskService = new EF.TaskService();
        private static string workingStatus = "working";
        private const string inProgressTaskStatus = "inprogress";
        private const string listeningStatus = "listening";
        private const int maxTaskCount = 4;
        private static int currentTaskCount = 0;

        static void Main(string[] args)
        {
            
            WebApp.Start<StartService>(url : serverHostName);
            Timer Timer = new Timer(CheckServerState, null, 0, 5000);
            Console.WriteLine("Service with the following host address http://localhost:7777 has started");
            Console.WriteLine("//////");
            Console.WriteLine("Press any key to stop server");
            Console.ReadKey();
        }

        private static void CheckServerState(Object stateInfo)
        {
            DbConectionEntities context = new DbConectionEntities();
            var server = context.ServerCapacities.FirstOrDefault(s => s.ServerHostName == serverHostName);
            StartTask(server.ServerId.Value, context);        
        }

        private async static void StartTask(int serverId, DbConectionEntities context)
        {
            if (currentTaskCount < maxTaskCount)
            {
                HttpClient client = new HttpClient();
                var taskId = await client.GetStringAsync(loadBalancerHostName + "/get/task/for/server/" + serverId);
                var task = context.Tasks.Find(int.Parse(taskId));
                if (task != null)
                {
                    currentTaskCount++;
                    Console.WriteLine($"Server working on task number {taskId}");
                    task.Status = inProgressTaskStatus;
                    context.SaveChanges();
                    _taskService.StartTask(int.Parse(taskId));
                    currentTaskCount--;
                }
            }
        }
    }
}
