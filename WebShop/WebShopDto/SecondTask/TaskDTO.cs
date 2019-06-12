using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDto.SecondTask
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public string Condition { get; set; }
        public string Result { get; set; }
        public string UserEmail { get; set; }
        public string ServerHostName { get; set; }
        public int? PercentageStatus { get; set; }
        public int? ServerId { get; set; }
    }
}
