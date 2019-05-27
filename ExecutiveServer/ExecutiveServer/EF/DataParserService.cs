using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutiveServer.EF
{
    class DataParserService
    {
        public void GetMatrixesFromTaskCondition(Task task, out List<List<int>> A, out List<List<int>> B)
        {
            string[] dataMatrixes = task.Condition.Split('*');
            A = GetMatrixDataFromString(dataMatrixes[0]);
            B = GetMatrixDataFromString(dataMatrixes[1]);
        }

        public List<List<int>> GetMatrixDataFromString(string condition)
        {
            string[] rows = condition.Split('|');

            List<List<int>> result = new List<List<int>>();

            foreach (var row in rows)
            {
                string[] numbers = row.Split(',');
                List<int> numbersRow = new List<int>();
                foreach (var number in numbers)
                {
                    numbersRow.Add(int.Parse(number));
                }
                result.Add(numbersRow);
            }
            return result;
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
                if (result != string.Empty)
                {
                    result = result.Substring(0, result.Length - 1);
                    result += '|';
                }
            });
            return result;
        }

    }
}
