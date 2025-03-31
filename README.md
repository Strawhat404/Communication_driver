C# TCP Client-Server Communication Driver
A sleek, modern TCP communication driver built in C#

Welcome to the C# TCP Client-Server Communication Driver—an elegant solution showcasing robust client-server interaction. This project mimics an industrial-grade communication driver, perfect for learning or integrating into real-world applications. With a multithreaded TCP server and a responsive client, it’s designed to handle commands and mock data exchange with finesse.

✨ Key Features
🚀 TCP Server
Listens Up: Monitors a configurable IP and port for incoming connections.
Multi-Client Ready: Handles multiple clients concurrently with multithreading magic.
Command Cruncher: Processes commands like GET_TEMP, GET_STATUS, and even INVALID_COMMAND.
Mock Responder: Fires back realistic responses (e.g., "Temperature: 25.3°C").
⚡ TCP Client
Command Sender: Connects seamlessly and dispatches predefined commands.
Response Viewer: Displays server replies right in your console.
🛡️ Robustness
Error-Proof: Gracefully handles network hiccups, bad commands, and surprises.
UTF-8 Encoding: Supports special characters like ° for pro-level responses.
Logging: Timestamped logs for crystal-clear traceability.
🛠️ Tech Stack
C# + .NET Core: Powering the core logic with modern efficiency.
TCP/IP & Sockets: The backbone of network communication.
Multithreading: Smooth, simultaneous client handling.
UTF-8: Because special characters deserve love too.
