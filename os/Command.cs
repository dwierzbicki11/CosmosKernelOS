using CosmosKernel1;
using Cosmos.System.Network;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DNS;
using Cosmos.System.Network.IPv4.TCP;
using System;
using System.Text;
using Cosmos.HAL;
using System.IO;
using Cosmos.System.Network.Config;

namespace os
{
    public static class Command
    {
        static Disk disk = new Disk();
        public static void Run(string command)
        {
            string[] words = command.Split(' ');
            if (words.Length > 0)
            {
                switch (words[0].ToLower())
                {
                    case "viewlog":
                        ViewLog();
                        break;
                    case "gui":
                        Boot.onBoot();
                        break;
                    case "info":
                        ShowSystemInfo();
                        break;
                    case "space":
                        ShowDiskSpace();
                        break;
                    case "format":
                        if (words.Length > 1)
                        {
                            FormatDisk(Convert.ToInt32(words[1]));
                        }
                        else
                        {
                            WriteMessage.writeError("Usage: format <drive>");
                            for (int i = 0; i < disk.disk.Disks.Count; i++)
                            {
                                WriteMessage.writeInfo($"Drive {i}: {disk.disk.Disks[i].Size}");
                            }
                        }
                        break;
                    case "ping":
                        if (words.Length > 1)
                        {
                            Ping(words[1]);
                        }
                        else
                        {
                            WriteMessage.writeError("Usage: ping <host>");
                        }
                        break;
                    case "ip":
                        ShowIPAddress();
                        break;
                    case "dns":
                        if (words.Length > 1)
                        {
                            ResolveDNS(words[1]);
                        }
                        else
                        {
                            WriteMessage.writeError("Usage: dns <hostname>");
                        }
                        break;
                    case "download":
                        if (words.Length > 2)
                        {
                            //DownloadFile(words[1], words[2]);
                        }
                        else
                        {
                            WriteMessage.writeError("Usage: download <url> <save_path>");
                        }
                        break;
                    case "ls":
                        if (words.Length > 1)
                        {
                            ListFiles(words[1]);
                        }
                        else
                        {
                            ListFiles(@"0:\");
                        }
                        break;
                    case "mkdir":
                        if (words.Length > 1)
                        {
                            CreateDirectory(words[1]);
                        }
                        else
                        {
                            WriteMessage.writeError("Usage: mkdir <path>");
                        }
                        break;
                    case "rmdir":
                        if (words.Length > 1)
                        {
                            DeleteDirectory(words[1]);
                        }
                        else
                        {
                            WriteMessage.writeError("Usage: rmdir <path>");
                        }
                        break;
                    case "help":
                        ShowHelp();
                        break;
                    default:
                        WriteMessage.writeError("Unknown command: " + words[0]);
                        break;
                }
            }
        }
        private static void ViewLog()
        {
            try
            {
                string logFilePath = @"C:\error_log.txt";
                if (File.Exists(logFilePath))
                {
                    string logContent = File.ReadAllText(logFilePath);
                    Console.WriteLine("Zawartość pliku error_log.txt:");
                    Console.WriteLine(logContent);
                }
                else
                {
                    Console.WriteLine("Plik error_log.txt nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytywania pliku logu: {ex.Message}");
            }
        }
        private static void ShowSystemInfo()
        {
            ulong speed = Cosmos.Core.CPU.GetCPUUptime();
            string speedmsg="";
            if (speed < 1000) speedmsg = speed.ToString() + "Hz";
            if (speed > 1000 && speed < 1000000) speedmsg = (speed/1000) + "KHz";
            if (speed > 1000000 && speed < 1000000000) speedmsg = (speed/1000000) + "MHz";
            if (speed > 1000000000 && speed < 1000000000000) speedmsg = (speed/ 1000000000) + "GHz";
            if (speed > 1000000000000 && speed < 1000000000000000) speedmsg = (speed/ 1000000000000) + "THz";

            WriteMessage.writeInfo("System Information:");
            WriteMessage.writeInfo("OS Name: MyOS");
            WriteMessage.writeInfo("Version: 1.0");
            WriteMessage.writeInfo("Available RAM: " + Cosmos.Core.CPU.GetAmountOfRAM() + " MB");
            WriteMessage.writeInfo($"Processor: \n\t---Name-{Cosmos.Core.CPU.GetCPUBrandString()} \n\t---Speed-{speedmsg}");
            
        }

        private static void ShowDiskSpace()
        {
            disk.ShowDiskSpace();
        }

        private static void FormatDisk(int drive)
        {
            disk.Format(drive);
        }

        private static void Ping(string host)
        {
            try
            {
                var dnsClient = new DnsClient();
                dnsClient.Connect(new Address(8, 8, 8, 8)); 
                dnsClient.SendAsk(host);
                var address = dnsClient.Receive();

                var ping = new ICMPClient();
                ping.Connect(address);
                WriteMessage.writeOK($"Ping reply from {address}");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error pinging {host}: {ex.Message}");
            }
        }

        private static void ShowIPAddress()
        {
            try
            {
                var networkDevice = NetworkDevice.Devices[0].Name;
                var network = NetworkDevice.GetDeviceByName(networkDevice);
                if (networkDevice != null)
                {
                    WriteMessage.writeInfo($"IP Address: {networkDevice}");
                    WriteMessage.writeInfo($"Mac Adress: {network.MACAddress}");
                    WriteMessage.writeInfo($"Is ready {network.Ready}");
                }
                else
                {
                    WriteMessage.writeError("No network device found.");
                }
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error getting IP address: {ex.Message}");
            }
        }

        private static void ResolveDNS(string hostname)
        {
            try
            {
                var dnsClient = new DnsClient();
                dnsClient.Connect(new Address(8, 8, 8, 8)); 
                dnsClient.SendAsk(hostname);
                var address = dnsClient.Receive();
                WriteMessage.writeOK($"Resolved {hostname} to {address}");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error resolving DNS: {ex.Message}");
            }
        }

        private static void DownloadFile(string url, string savePath)
        {
            try
            {
                // Parse URL
                var uri = new Uri(url);
                var host = uri.Host;
                var path = uri.PathAndQuery;

                // Resolve host to IP address using DnsClient
                var dnsClient = new DnsClient();
                dnsClient.Connect(new Address(8, 8, 8, 8)); // Google DNS
                dnsClient.SendAsk(host);
                var address = dnsClient.Receive();

                // Create TCP client
                var client = new TcpClient(address, 80); // HTTP port

                // Send HTTP GET request
                var request = $"GET {path} HTTP/1.1\r\nHost: {host}\r\nConnection: close\r\n\r\n";
                client.Send(Encoding.ASCII.GetBytes(request));

                // Receive response
                EndPoint source = new EndPoint(address, 80);
                var response = client.Receive(ref source);
                var responseText = Encoding.ASCII.GetString(response);

                // Extract body (skip headers)
                var bodyStart = responseText.IndexOf("\r\n\r\n") + 4;
                var body = responseText.Substring(bodyStart);

                // Save to file
                var fs = new FileStream(savePath, FileMode.Create);
                fs.Write(Encoding.ASCII.GetBytes(body), 0, body.Length);
                fs.Close();

                WriteMessage.writeOK($"File downloaded to {savePath}");
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Error downloading file: {ex.Message}");
            }
        }

        private static void ListFiles(string path)
        {
            disk.ListFiles(path);
        }

        private static void CreateDirectory(string path)
        {
            
            disk.CreateDirectory(path);
        }

        private static void DeleteDirectory(string path)
        {
            
            disk.DeleteDirectory(path);
        }

        private static void ShowHelp()
        {
            WriteMessage.writeInfo("Available commands:");
            WriteMessage.writeInfo("  gui         - Start GUI");
            WriteMessage.writeInfo("  info        - Show system information");
            WriteMessage.writeInfo("  space       - Show disk space");
            WriteMessage.writeInfo("  format <drive> - Format a drive");
            WriteMessage.writeInfo("  ping <host> - Ping a host");
            WriteMessage.writeInfo("  ip          - Show IP address");
            WriteMessage.writeInfo("  dns <hostname> - Resolve DNS");
            WriteMessage.writeInfo("  download <url> <save_path> - Download a file");
            WriteMessage.writeInfo("  ls <path>   - List directory contents");
            WriteMessage.writeInfo("  mkdir <path> - Create a directory");
            WriteMessage.writeInfo("  rmdir <path> - Delete a directory");
            WriteMessage.writeInfo("  help        - Show this help message");
        }
    }
}