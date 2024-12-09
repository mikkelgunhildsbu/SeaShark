using TrainingPlan.Models; 

namespace TrainingPlan.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Role? UserRole { get; set; }
        public int WorkoutPlanId { get; set; }
    }

}
