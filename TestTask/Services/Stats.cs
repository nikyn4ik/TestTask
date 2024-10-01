using Microsoft.AspNetCore.SignalR;
using TestTask.Data;
using TestTask.SignalRHub;

namespace TestTask.Services
{
    public class Stats : BackgroundService
    {
        private readonly LbContext _context;
        private readonly IHubContext<StatsHub> _hubContext;

        public Stats(LbContext context, IHubContext<StatsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var issuedBooksCount = _context.Books.Count(b => b.IsIssued);
                var onlineUsersCount = _context.Users.Count(u => u.IsOnline);

                Console.WriteLine($"Issued Books: {issuedBooksCount}, Online Users: {onlineUsersCount}");

                var stats = new
                {
                    IssuedBooksCount = issuedBooksCount,
                    OnlineUsersCount = onlineUsersCount
                };

                await _hubContext.Clients.All.SendAsync("ReceiveStatsUpdate", stats);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
