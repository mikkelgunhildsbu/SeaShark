namespace TrainingPlan.Models
{
    public class WorkoutPlan
    {
        public int WorkoutPlanId { get; set; }
        public string? WorkoutPlanName { get; set; }
        
        public ICollection<Workout>? Workouts { get; set; } = new List<Workout>();

    }
}
