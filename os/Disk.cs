using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;

namespace os
{
    class Disk
    {
        private CosmosVFS disk;

        public Disk()
        {
            disk = new CosmosVFS();
            VFSManager.RegisterVFS(disk);
        }

        public void ShowDiskSpace()
        {
            var drives = VFSManager.GetVolumes();
            foreach (var drive in drives)
            {
                WriteMessage.writeInfo($"Drive: {drive.mName}");
                WriteMessage.writeInfo($"Total Size: {drive.mSize} bytes");
                Console.WriteLine();
            }
        }

        public void Format(int drive)
        {
            try
            {
                disk.Disks[drive].FormatPartition(0, "FAT32", true); // Poprawione: Użyj metody Format z obiektu CosmosVFS
                WriteMessage.writeOK($"Drive formatted successfully.");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error formatting drive: {ex.Message}");
            }
        }

        public void ListFiles(string path)
        {
            try
            {
                ListDirectory(path, 0); // Start with indentation level 0
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error listing files: {ex.Message}");
            }
        }

        private void ListDirectory(string path, int indentLevel)
        {
            var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path);

            // Display current directory name
            Console.WriteLine(new string(' ', indentLevel * 4) + Path.GetFileName(path));

            // Display files in the current directory
            foreach (var file in files)
            {
                Console.WriteLine(new string(' ', (indentLevel + 1) * 4) + Path.GetFileName(file));
            }

            // Recursively display subdirectories
            foreach (var dir in directories)
            {
                ListDirectory(dir, indentLevel + 1);
            }
        }

        public void CreateFile(string path)
        {
            try
            {
                var file = File.Create(path);
                file.Close();
                WriteMessage.writeOK($"File created: {path}");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error creating file: {ex.Message}");
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                WriteMessage.writeOK($"File deleted: {path}");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error deleting file: {ex.Message}");
            }
        }

        public void CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                WriteMessage.writeInfo($"Directory created: {path}");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error creating directory: {ex.Message}");
            }
        }

        public void DeleteDirectory(string path)
        {
            try
            {
                Directory.Delete(path, true); // true = delete recursively
                WriteMessage.writeOK($"Directory deleted: {path}");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error deleting directory: {ex.Message}");
            }
        }
    }
}