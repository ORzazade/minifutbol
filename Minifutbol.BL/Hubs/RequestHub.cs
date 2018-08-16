using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Minifutbol.BL.Models.CustomerRequest;

namespace Minifutbol.BL.Hubs
{
    public class RequestHub:Hub
    {
        public static ConcurrentDictionary<string,object> ConnectedUsers=new ConcurrentDictionary<string, object>();

        public override Task OnConnected()
        {
            Clients.Caller.Test("Wellcome");
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            object userName;
            
            ConnectedUsers.TryRemove(Context.ConnectionId, out userName);
            return base.OnDisconnected(stopCalled);
        }

        public void Received(List<RequestViewModel> at)
        {
            Clients.Caller.Test(at);
        }

        public void LogIn(string username)
        {
            ConnectedUsers.AddOrUpdate(Context.ConnectionId, username, (k, v) => username);
        }
    }
}
