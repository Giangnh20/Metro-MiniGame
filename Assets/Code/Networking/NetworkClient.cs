using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SocketIO;
using UnityEngine;


namespace Code.Networking
{
    public class NetworkClient : SocketIOComponent
    {
        public override void Start()
        {
            base.Start();
            this.setupEvents();
        }

        public override void Update()
        {
            base.Update();
        }

        private void setupEvents()
        {
            On("open", OnConnected);
            On("disconnected", OnDisconnected);
        }

        private void OnDisconnected(SocketIOEvent obj)
        {
            Debug.Log("Disconected");
        }

        private void OnConnected(SocketIOEvent obj)
        {
            Debug.Log("Connected");
        }
    }
}