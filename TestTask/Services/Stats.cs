using TestTask.Data;

namespace TestTask.Services
{
    public class Stats : BackgroundService
    {
        private readonly LbContext _context;

        public Stats(LbContext context)
        {
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var issuedBooksCount = _context.Books.Count(b => b.IsIssued);
                var onlineUsersCount = _context.Users.Count(u => u.IsOnline);

                Console.WriteLine($"Issued Books: {issuedBooksCount}, Online Users: {onlineUsersCount}");

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
}
