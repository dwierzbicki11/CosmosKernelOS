using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public static class ProcessManager
    {
        public static List<Process> Processes = new List<Process>();
        public static void Update() {
            foreach (Process process in Processes)
            {
                process.Run();
            }
        }
        public static void Start(Process process) { 
            Processes.Add(process);
            process.Start();
        }
        public static void Stop(Process process) { 
            Processes.Remove(process);
        }
    }
}
