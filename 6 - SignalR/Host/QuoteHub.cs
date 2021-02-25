using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Host
{
    public class QuoteHub : Hub
    {
        public string SendMessage(string user, string message)
        {
            Clients.All.SendAsync("ReceiveMessage", user, message);
            return $"[{Context.ConnectionId}]: {user} {message}";
        }
    }
}
