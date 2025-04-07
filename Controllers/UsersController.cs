using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers() => Ok(UserStore.Users);

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = UserStore.Users.FirstOrDefault(u => u.Id == id);
                return user == null ? NotFound() : Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        //[HttpGet("{id}")]
        //public IActionResult GetUser(int id)
        //{
        //    var user = UserStore.Users.FirstOrDefault(u => u.Id == id);
        //    return user == null ? NotFound() : Ok(user);
        //}

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            user.Id = UserStore.Users.Count + 1;
            UserStore.Users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);

            if (string.IsNullOrWhiteSpace(user.Name) || !user.Email.Contains("@"))
                return BadRequest("Invalid name or email.");

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = UserStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = UserStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            UserStore.Users.Remove(user);
            return NoContent();
        }
    }

}
