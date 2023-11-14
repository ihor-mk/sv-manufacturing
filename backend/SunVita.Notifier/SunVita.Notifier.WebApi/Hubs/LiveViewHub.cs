using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SunVita.Core.Common.DTO.Live;

namespace SunVita.Notifier.WebApi.Hubs
{
    public class LiveViewHub : Hub
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
        public async Task SendLineUpdateMessage(ICollection<LiveViewCountsDto> msg)
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("LineUpdate", JsonConvert.SerializeObject(msg));
            }
            else
            {
                await Console.Out.WriteLineAsync("There are no connected clients");
            }
        }
    }
}
