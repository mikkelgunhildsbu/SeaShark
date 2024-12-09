namespace TrainingPlan.Models
{
    public class WorkoutStep
    {
        public int WorkoutStepId { get; set; }
        public string? WorkoutStepName { get; set; }
        public string? WorkoutStepDescription { get; set; }
        public int? WorkoutStepDuration { get; set; }
        public ICollection<Workout>? Workouts { get; set; } = new List<Workout>();
    }
}
