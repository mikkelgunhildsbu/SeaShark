using Microsoft.AspNetCore.Identity;
using TrainingPlan.Models;

namespace TrainingPlan.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public int WorkoutPlanId { get; set; }

        public WorkoutPlan WorkoutPlan { get; set; }

        public Role? UserRole { get; set; }
    }
}
