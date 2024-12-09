using System.Data.Common;

namespace TrainingPlan.Models
{
    public class Workout
    {
        public  int WorkoutId { get; set; }
        public string? WorkoutName { get; set; }
        public ICollection<WorkoutStep>? WorkoutSteps { get; set; } = new List<WorkoutStep>();
        public int Type { get; set; }
        public int WorkoutPlan { get; set;}
        public DateTime WorkoutDate { get; set; }

    }
}
