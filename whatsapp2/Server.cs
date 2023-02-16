﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace whatsapp2
{
    internal class Server
    {
        private List<Client> _clients;
        private IPAddress _IP = IPAddress.Parse("192.168.1.12");                //change me
        private int _PORT = 6666;                                               //change me
        private TcpListener _tcpListener;


        public Server()
        {
            _tcpListener = new TcpListener(_IP, _PORT);
            _clients = new List<Client>();
        }
        public Server(string ip, int port)
        {
            _tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            _clients = new List<Client>();
        }

        public async void Start()
        {
            _tcpListener.Start();
            MessageBox.Show("started");
            while(true) { 
                TcpClient tcpClient = await _tcpListener.AcceptTcpClientAsync();
                MessageBox.Show("a client connected!");
                _clients.Add(new Client(tcpClient));
            }
        }
        public void Stop()                              //work in progress
        {
            foreach(Client client in _clients)
            {
                client.EndClient();
            }
            _tcpListener.Stop();
        }
        public void Broadcast(string msg) { 
            foreach(Client client in _clients)
            {
                client._session._streamWriter.WriteLine(msg);
            }
        }

    }
}
