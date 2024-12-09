using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlan.Models;

namespace TrainingPlan.Service
{
    public interface IWorkoutPlanService
    {
        Task<WorkoutPlan> GetWorkoutPlanAsync(int id);
        Task<WorkoutPlan> GetoWorkoutPlanByUserIdAsync(int id);
        Task<WorkoutPlan> CreateWorkoutPlanAsync(WorkoutPlan workoutPlan);
        Task<bool> UpdateWorkoutPlanAsync(WorkoutPlan workoutPlan);
        Task<bool> DeleteWorkoutPlanAsync(int id);
    }
}
