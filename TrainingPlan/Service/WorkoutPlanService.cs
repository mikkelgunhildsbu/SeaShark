using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingPlan.Data;
using TrainingPlan.Models;

namespace TrainingPlan.Service
{
    public class WorkoutPlanService: IWorkoutPlanService
    {
        private readonly TrainingPlanDbContext _context;

        public WorkoutPlanService(TrainingPlanDbContext context)
        {
            _context = context;
        }

        public async Task<WorkoutPlan> CreateWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlan.Add(workoutPlan);
            await _context.SaveChangesAsync();
            return workoutPlan;
        }

        public async Task<bool> DeleteWorkoutPlanAsync(int id)
        {

            var workoutPlan = await _context.WorkoutPlan.FindAsync(id);
            if (workoutPlan == null) return false;
            _context.WorkoutPlan.Remove(workoutPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WorkoutPlan> GetoWorkoutPlanByUserIdAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user.WorkoutPlan != null)
            {
                return user.WorkoutPlan;
            }
            else throw new Exception(" Wokrout Not Found");
        }

        public async Task<WorkoutPlan> GetWorkoutPlanAsync(int id)
        {
            return await _context.WorkoutPlan.FirstOrDefaultAsync(x => x.WorkoutPlanId == id);
        }

        public async Task<bool> UpdateWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            var existingWorkoutPlan = await _context.WorkoutPlan.FindAsync(workoutPlan.WorkoutPlanId);
            if (existingWorkoutPlan == null) return false;

            existingWorkoutPlan.WorkoutPlanName = workoutPlan.WorkoutPlanName;


            await _context.SaveChangesAsync();
            return true;
        }
    }
}
