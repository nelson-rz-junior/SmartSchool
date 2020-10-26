using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SmartSchool.API.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task NewMessage(IHubContext<NotificationHub> hubContext, string method, string call, string message)
        {
            await hubContext.Clients.All.SendAsync(method, call, message);
        }
    }
}