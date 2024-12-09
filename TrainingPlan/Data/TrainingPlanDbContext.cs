using Microsoft.EntityFrameworkCore;
using TrainingPlan.Models;

namespace TrainingPlan.Data
{
    public class TrainingPlanDbContext : DbContext
    {
        public TrainingPlanDbContext(DbContextOptions<TrainingPlanDbContext> options) : base(options)
        {
        }
        public DbSet<Workout> Workout { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlan { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WorkoutStep> WorkoutStep { get; set; }

        



    }


}
