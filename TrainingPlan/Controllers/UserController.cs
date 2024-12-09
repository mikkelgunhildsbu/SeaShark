using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlan.Models;
using TrainingPlan.Services;
using TrainingPlan.DTOs;

namespace TrainingPlan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            // Map domain users to UserReadDto
            var userDtos = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                WorkoutPlanId = u.WorkoutPlanId
            });

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = MapToDto(user);
            
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map UserCreateDto to domain User
            var user = new User
            {
                UserName = userCreateDto.UserName,
                Email = userCreateDto.Email,
                WorkoutPlanId = userCreateDto.WorkoutPlanId
            };

            var createdUser = await _userService.CreateUserAsync(user);

            var createdUserDto = MapToDto(createdUser);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.UserId }, createdUserDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId || !ModelState.IsValid)
                return BadRequest(ModelState);

            // Map UserUpdateDto to domain User

            var userToUpdate = MapToDto(user);
            

            var success = await _userService.UpdateUserAsync(user);
            if (!success)
                return NotFound();

            return Ok(userToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        #region Privat Helper Funcitons 

        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                UserRole = user.UserRole,
                WorkoutPlanId = user.WorkoutPlanId
            };
        }

        #endregion
    }
}
