using TrainingPlan.Models;

namespace TrainingPlan.DTOs
{
    public class WorkoutPlanDto
    {
        public int WorkoutPlanId { get; set; }
        public string? WorkoutPlanName { get; set; }

        public List<int> WorkoutIds { get; set; } = new List<int>();

    }
}
