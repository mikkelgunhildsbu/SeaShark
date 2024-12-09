using Microsoft.AspNetCore.Mvc;
using TrainingPlan.Service;
using TrainingPlan.Models;
using TrainingPlan.DTOs;
using Microsoft.EntityFrameworkCore;

namespace TrainingPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkoutPlan(int id)
        {
            var workoutPlan = await _workoutPlanService.GetWorkoutPlanAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            // Map domain model to DTO
            var dto = new WorkoutPlanDto
            {
                WorkoutPlanId = workoutPlan.WorkoutPlanId,
                WorkoutPlanName = workoutPlan.WorkoutPlanName,
                WorkoutIds = workoutPlan.Workouts?.Select(w => w.WorkoutId).ToList() ?? new List<int>()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkoutPlan([FromBody] WorkoutPlanDto workoutPlanDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to domain model
            var workoutPlan = new WorkoutPlan
            {
                WorkoutPlanName = workoutPlanDto.WorkoutPlanName
                // If you need to handle WorkoutIds, you'd need to load/create workouts here.
                // For simplicity, we're ignoring them or they can be handled at service level if needed.
            };

            var createdWorkoutPlan = await _workoutPlanService.CreateWorkoutPlanAsync(workoutPlan);

            // Map back to DTO
            var createdDto = new WorkoutPlanDto
            {
                WorkoutPlanId = createdWorkoutPlan.WorkoutPlanId,
                WorkoutPlanName = createdWorkoutPlan.WorkoutPlanName,
                WorkoutIds = createdWorkoutPlan.Workouts?.Select(w => w.WorkoutId).ToList() ?? new List<int>()
            };

            return Ok(createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkoutPlan(int id, [FromBody] WorkoutPlanDto workoutPlanDto)
        {
            if (id != workoutPlanDto.WorkoutPlanId || !ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to domain model
            var updatedWorkoutPlan = new WorkoutPlan
            {
                WorkoutPlanId = workoutPlanDto.WorkoutPlanId,
                WorkoutPlanName = workoutPlanDto.WorkoutPlanName
                // Again, handle WorkoutIds if necessary by loading workouts, etc.
            };

            var success = await _workoutPlanService.UpdateWorkoutPlanAsync(updatedWorkoutPlan);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutPlan(int id)
        {
            var success = await _workoutPlanService.DeleteWorkoutPlanAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
