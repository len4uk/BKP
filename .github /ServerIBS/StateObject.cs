using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerIBS
{
    // Объект состояния для асинхронного считывания клиентских данных  
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer 8k.  
        public const int BufferSize = 8192;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
}