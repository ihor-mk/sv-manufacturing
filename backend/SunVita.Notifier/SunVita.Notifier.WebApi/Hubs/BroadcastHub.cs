﻿using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SunVita.Core.Common.DTO.Live;

namespace SunVita.Notifier.WebApi.Hubs
{
    public class BroadcastHub : Hub
    {
        private static readonly Dictionary<string, string> ConnectedUsers = new();
        public void Connect(string email)
        {
            ConnectedUsers[email] = Context.ConnectionId;
        }

        public void Disconnect(string email)
        {
            ConnectedUsers.Remove(email);
        }
        public async Task SendLineUpdateMessage(LineUpdateDto msg)
        {
            if (Clients is not null)
            {
                await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(msg));
                await Clients.All.SendAsync("LineUpdate", JsonConvert.SerializeObject(msg));
            }
            else
            {
                await Console.Out.WriteLineAsync("There are no connected clients");
            }
        }
    }
}
