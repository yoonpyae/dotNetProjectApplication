﻿using dotNetProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace dotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EndpointSummary("Get all users")]
        [EndpointDescription("Get all users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
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

            _context.Users.Add(user);
            return Ok(_context.SaveChanges());
        }

        [HttpGet("{id}")]
        [EndpointSummary("Get By User Id")]
        public IActionResult GetUserById(string id)
        {
            var user = _context.Users.SingleOrDefault(y => y.UserId == id);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete User")]
        public IActionResult DeleteUser(string id)
        {
            var user = _context.Users.Find(id);

            var users = _context.Users.SingleOrDefault(y => y.UserId == id);
            if (users == null)
            {
                return BadRequest("User Not Found");
            }
            if (user != null)
                _context.Users.Remove(user);

            return Ok(_context.SaveChanges());
        }

        [HttpPut]
        [EndpointSummary("Update Users")]
        public IActionResult UpdateUser(string id, string newUser)
        {


            var users = _context.Users.SingleOrDefault(y => y.UserId == id);
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
