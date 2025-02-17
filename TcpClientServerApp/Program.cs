using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Run the server in a separate thread
            Thread serverThread = new Thread(() => StartServer());
            serverThread.Start();

            // Give the server some time to start
            Thread.Sleep(1000);

            // Run the client
            StartClient();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Application error: {ex.Message}");
        }
    }

    static void StartServer()
    {
        try
        {
            // Define the IP address and port for the server
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Localhost
            int port = 5000;

            // Create a TCP listener
            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();
            Console.WriteLine($"[{DateTime.Now}] Server started. Waiting for connections on {ipAddress}:{port}...");

            while (true)
            {
                try
                {
                    // Accept incoming client connections
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine($"[{DateTime.Now}] Client connected!");

                    // Handle the client in a separate thread
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now}] Server error while accepting client: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now}] Server error: {ex.Message}");
        }
    }

    static void HandleClient(TcpClient client)
    {
        try
        {
            // Get the network stream for reading/writing data
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                try
                {
                    // Convert the received data to a string
                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[{DateTime.Now}] Received: {request}");

                    // Process the request and send a response
                    string response = ProcessRequest(request);
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                    Console.WriteLine($"[{DateTime.Now}] Sent: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now}] Error handling client request: {ex.Message}");
                }
            }

            // Close the connection
            client.Close();
            Console.WriteLine($"[{DateTime.Now}] Client disconnected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now}] Error handling client: {ex.Message}");
        }
    }

    static string ProcessRequest(string request)
    {
        // Mock responses based on the command
        switch (request.Trim().ToUpper())
        {
            case "GET_TEMP":
                return "Temperature: 25.3°C";
            case "GET_STATUS":
                return "Status: Active";
            default:
                return "Unknown command";
        }
    }

    static void StartClient()
    {
        try
        {
            // Connect to the server
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            // Send commands to the server
            string[] commands = { "GET_TEMP", "GET_STATUS", "INVALID_COMMAND" };
            foreach (string command in commands)
            {
                try
                {
                    byte[] requestBytes = Encoding.UTF8.GetBytes(command);
                    stream.Write(requestBytes, 0, requestBytes.Length);
                    Console.WriteLine($"[{DateTime.Now}] Sent: {command}");

                    // Read the server's response
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[{DateTime.Now}] Received: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now}] Error sending/receiving data: {ex.Message}");
                }
            }

            // Close the connection
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now}] Client error: {ex.Message}");
        }
    }
}