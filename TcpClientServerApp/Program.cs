using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Run the server in a separate thread
        Thread serverThread = new Thread(() => StartServer());
        serverThread.Start();

        // Give the server some time to start
        Thread.Sleep(1000);

        // Run the client
        StartClient();
    }

    static void StartServer()
    {
        // Define the IP address and port for the server
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Localhost
        int port = 5000;

        // Create a TCP listener
        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();
        Console.WriteLine("Server started. Waiting for connections...");

        while (true)
        {
            // Accept incoming client connections
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected!");

            // Handle the client in a separate thread
            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    static void HandleClient(TcpClient client)
    {
        // Get the network stream for reading/writing data
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            // Convert the received data to a string
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received: {request}");

            // Process the request and send a response
            string response = ProcessRequest(request);
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);
            Console.WriteLine($"Sent: {response}");
        }

        // Close the connection
        client.Close();
        Console.WriteLine("Client disconnected.");
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
        // Connect to the server
        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();

        // Send commands to the server
        string[] commands = { "GET_TEMP", "GET_STATUS", "INVALID_COMMAND" };
        foreach (string command in commands)
        {
            byte[] requestBytes = Encoding.UTF8.GetBytes(command);
            stream.Write(requestBytes, 0, requestBytes.Length);
            Console.WriteLine($"Sent: {command}");

            // Read the server's response
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received: {response}");
        }

        // Close the connection
        client.Close();
    }
}