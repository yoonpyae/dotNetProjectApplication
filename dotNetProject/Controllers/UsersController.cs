using dotNetProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        [EndpointSummary("Get all users")]
        [EndpointDescription("Get all users")]
        public IActionResult GetUsers()
        {
            List<User> users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        [EndpointSummary("Create new user")]
        [EndpointDescription("Creating a new user")]
        public IActionResult PostCustomers([FromForm] User user)
        {
            if (_context.Users.Any(x => x.UserId == user.UserId))
            {
                return BadRequest("User Already Exist");
            }

            _ = _context.Users.Add(user);
            return Ok(_context.SaveChanges());
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get By User Id")]
        public IActionResult GetUserById(string id)
        {
            User? user = _context.Users.SingleOrDefault(y => y.UserId == id);
            return user == null ? BadRequest("User Not Found") : Ok(user);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete User")]
        public IActionResult DeleteUser(string id)
        {
            User? user = _context.Users.Find(id);

            User? users = _context.Users.SingleOrDefault(y => y.UserId == id);
            if (users == null)
            {
                return BadRequest("User Not Found");
            }
            if (user != null)
            {
                _ = _context.Users.Remove(user);
            }

            return Ok(_context.SaveChanges());
        }

        [HttpPut]
        [EndpointSummary("Update Users")]
        public IActionResult UpdateUser(string id, string newUser)
        {


            User? users = _context.Users.SingleOrDefault(y => y.UserId == id);
            if (users == null)
            {
                return BadRequest("User Not Found");
            }
            if (user is not null)
            {
                user.UserName = newUser;
            }

            return Ok(_context.SaveChanges());
        }
    }
}
