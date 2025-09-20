using Cosmos.Core.Memory;
using Cosmos.System;
using CosmosKernel1.Graphics;
using os;
using System;
using System.IO;
using Console = System.Console;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        public static bool runGui = false;
        public static string path = @"0:\";
        private int lastHeapCollect;

        protected override void BeforeRun()
        {
            Console.Clear();
            Command.Run("space");
            Console.WriteLine();
            Command.Run("info");

        }
        public static void LogError(string errorMessage)
        {
            try
            {
                string logFilePath = @"0:\error_log.txt";
                string logEntry = $"[{DateTime.Now}] {errorMessage}\n";
                if (!File.Exists(logFilePath))
                {
                    File.WriteAllText(logFilePath, logEntry);
                }
                else
                {
                    File.AppendAllText(logFilePath, logEntry);
                }
                Console.WriteLine($"Zapisano błąd do pliku: {logFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisywania do pliku logu: {ex.Message}");
            }
        }
        protected override void Run()
        {
            try
            {
                if (lastHeapCollect >= 20)
                {
                    Heap.Collect();
                    lastHeapCollect = 0;
                }
                else
                {
                    lastHeapCollect++;
                }
                if (runGui)
                {
                    GUI.Update();
                }
                else
                {
                    if (KeyboardManager.TryReadKey(out var key))
                    {
                        WriteMessage.writeOK($"[KEY EVENT] Key: {key.Key}, char: '{key.KeyChar}'");
                    }
                    else
                    {
                        WriteMessage.writeError("[KEY EVENT] keyboard not found");
                    }
                    Console.Write($"{path}>");
                    var command = Console.ReadLine();
                    Command.Run(command);
                }
            }
            catch (Exception ex)
            {
                runGui = false;
                WriteMessage.writeError("GUI crashed! Error details:");
                WriteMessage.writeError(ex.ToString());
            }
        }
    }
}