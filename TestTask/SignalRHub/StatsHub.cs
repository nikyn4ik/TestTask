using Microsoft.AspNetCore.SignalR;
using TestTask.Data;

namespace TestTask.SignalRHub
{
    public class StatsHub : Hub
    {
        private readonly LbContext _context;

        public StatsHub(LbContext context)
        {
            _context = context;
        }

        public async Task SendStatsUpdate()
        {
            var stats = new
            {
                IssuedBooksCount = _context.Books.Count(b => b.IsIssued),
                OnlineUsersCount = _context.Users.Count(u => u.IsOnline)
            };

            await Clients.All.SendAsync("ReceiveStatsUpdate", stats);
        }
    }
}