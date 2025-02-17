# C# TCP Client-Server Communication Driver
This project is a C# TCP Client-Server Application that mimics a simple communication driver. It demonstrates how to implement a basic TCP server and client in C# for industrial data exchange.The server listens for incoming connections, processes commands, and sends mock responses, while the client sends predefined commands and displays the server's responses.

Features
TCP Server:

Listens for incoming client connections on a specified IP address and port.

Handles multiple clients simultaneously using multithreading.

Processes commands like GET_TEMP, GET_STATUS, and INVALID_COMMAND.

Sends mock responses (e.g., temperature, status) back to the client.

TCP Client:

Connects to the server and sends predefined commands.

Displays the server's responses in the console.

Error Handling:

Robust error handling for network issues, invalid commands, and unexpected errors.

UTF-8 Encoding:

Supports special characters like ° in responses (e.g., Temperature: 25.3°C).

Logging:

Timestamps for all log messages for better traceability.

Technologies Used
C# and .NET Core for the application logic.

TCP/IP and Sockets for network communication.

Multithreading to handle multiple clients simultaneously.

UTF-8 Encoding for proper handling of special characters.

How to Set Up and Run the Project
Prerequisites
.NET SDK:

Download and install the latest .NET SDK 

Visual Studio Code (Optional):

Download and install VS Code.

Install the C# extension for VS Code.

Git:

Install Git
Steps to Run the Project
Clone the Repository:

bash
Copy
git clone https://github.com/strawhat404/Communication_driver.git
cd Communication_driver
Build the Project:

dotnet build
Run the Application:

dotnet run
Observe the Output:

The server will start and wait for connections.

The client will connect to the server, send commands, and display responses.

