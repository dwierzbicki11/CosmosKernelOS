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

            Console.WriteLine("Cosmos booted successfully. Running GUI...");
            //LogError("Test");
        }
        public static void LogError(string errorMessage)
        {
            //var disk = new Disk();
            
            try
            {
                // Ścieżka do pliku logu
                string logFilePath = @"0:\error_log.txt";

                // Dodaj datę i godzinę do komunikatu
                string logEntry = $"[{DateTime.Now}] {errorMessage}\n";

                // Sprawdź, czy plik istnieje
                if (!File.Exists(logFilePath))
                {
                    //File.Create(path);
                    File.WriteAllText(logFilePath, logEntry);
                }
                else
                {
                    File.AppendAllText(logFilePath, logEntry);
                }

                // Wyświetl komunikat w konsoli
                Console.WriteLine($"Zapisano błąd do pliku: {logFilePath}");
            }
            catch (Exception ex)
            {
                // Jeśli wystąpi błąd podczas zapisywania do pliku, wyświetl go w konsoli
                Console.WriteLine($"Błąd podczas zapisywania do pliku logu: {ex.Message}");
            }
        }
        protected override void Run()
        {
            try
            {
                if (lastHeapCollect >= 20) { 
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
                    Console.Write($"{path}>");
                    var command = Console.ReadLine();
                    Command.Run(command);
                }
            }
            catch (Exception ex)
            {
                // Wyłącz GUI i wyświetl błąd w konsoli
                runGui = false;
                WriteMessage.writeError("GUI crashed! Error details:");
                WriteMessage.writeError(ex.ToString()); // Wyświetl szczegóły wyjątku
            }
        }
    }
}