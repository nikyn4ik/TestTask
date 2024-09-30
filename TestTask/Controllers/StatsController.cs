using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly LbContext _context;

        public StatsController(LbContext context)
        {
            _context = context;
        }

        // GET: api/Stats/Books
        [HttpGet("Books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Stats/Users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // POST: api/Stats/AddBook
        [HttpPost("AddBook")]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBooks", new { id = book.Id }, book);
        }

        // POST: api/Stats/AddUser
        [HttpPost("AddUser")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }
    }
}
