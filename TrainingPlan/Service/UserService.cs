using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingPlan.Data;
using TrainingPlan.Models;

namespace TrainingPlan.Services
{
    public class UserService : IUserService
    {
        private readonly TrainingPlanDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(TrainingPlanDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.User
                .Include(u => u.WorkoutPlan)
                .ToListAsync();
            }
            catch (System.Exception ex) 
            {
                _logger.LogError(ex, "Error fetching all users");
                throw;
            }
            
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.User
                    .FirstOrDefaultAsync(u => u.UserId == id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Etter fetching user with {id}"); 
                throw;
            }
           }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            { 
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error creating new user");
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                var existingUser = await _context.User.Include(u => u.WorkoutPlan).FirstOrDefaultAsync(u => u.UserId == user.UserId);
                if (existingUser == null) return false;

                // Update only provided fields
                if (user.UserName != null)
                    existingUser.UserName = user.UserName;

                if (user.Email != null)
                    existingUser.Email = user.Email;

                if (user.UserRole != null)
                    existingUser.UserRole = user.UserRole;

                if (user.WorkoutPlanId != 0 && user.WorkoutPlanId != existingUser.WorkoutPlanId)
                {
                    // Validate new WorkoutPlanId
                    var wpExists = await _context.WorkoutPlan.AnyAsync(wp => wp.WorkoutPlanId == user.WorkoutPlanId);
                    if (!wpExists)
                    {
                        throw new KeyNotFoundException($"WorkoutPlan with ID {user.WorkoutPlanId} does not exist.");
                    }
                    existingUser.WorkoutPlanId = user.WorkoutPlanId;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with ID {user.UserId}.");
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.User.FindAsync(id);
                if (user == null) return false;

                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with ID {id}.");
                throw;
            }
        }
    }
}
