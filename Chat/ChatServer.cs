// ChatServer
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatClient
{
    private static List<Socket> _clients = new List<Socket>();
    private static List<string> _usernames = new List<string>();

    public static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine("Сервер запущен...");

        while (true)
        {
            Socket clientSocket = server.AcceptSocket();
            Thread clientThread = new Thread(() => HandleClient(clientSocket));
            clientThread.Start();
        }
    }

    private static void HandleClient(Socket clientSocket)
    {
        byte[] buffer = new byte[1024];
        int bytesRead;
        
        // Получение логина
        bytesRead = clientSocket.Receive(buffer);
        string username = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
        
        lock (_clients)
        {
            _clients.Add(clientSocket);
            _usernames.Add(username);
            Broadcast($"{username} вошел в чат.");
        }
        
        // Обработка сообщений
        while (true)
        {
            try
            {
                bytesRead = clientSocket.Receive(buffer);
                if (bytesRead == 0) break; // Клиент отключился

                string message = $"{username}: {Encoding.UTF8.GetString(buffer, 0, bytesRead)}";
                Broadcast(message);
            }
            catch
            {
                break;
            }
        }

        // Удаление клиента
        lock (_clients)
        {
            _clients.Remove(clientSocket);
            _usernames.Remove(username);
            Broadcast($"{username} покинул чат.");
        }

        clientSocket.Close();
    }

    private static void Broadcast(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        lock (_clients)
        {
            foreach (var client in _clients)
            {
                client.Send(data);
            }
        }
    }
}