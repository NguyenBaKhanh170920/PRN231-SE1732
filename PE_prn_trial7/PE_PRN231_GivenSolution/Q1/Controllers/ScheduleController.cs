using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly PE_PRN_24SumB5Context _context;
        public ScheduleController(PE_PRN_24SumB5Context context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("AddSchedule")]
        public IActionResult Add(ScheduleDTO scheduleDto)
        {
            try
            {
                if (scheduleDto.endDate < scheduleDto.startDate)
                {
                    return Conflict("Date error");
                }
                var list =_context.Schedules.ToList();
                foreach (var sche in list)
                {
                    if(sche.MovieId!=scheduleDto.movieId && sche.RoomId==scheduleDto.roomId && sche.TimeSlotId==scheduleDto.timeslotId
                        && sche.StartDate==scheduleDto.startDate&& sche.EndDate==scheduleDto.endDate)
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable, "Content type not acceptable.");
                    }
                }
                Schedule schedule = new Schedule()
                {
                    StartDate = scheduleDto.startDate,
                    EndDate = scheduleDto.endDate,
                    MovieId = scheduleDto.movieId,
                    RoomId = scheduleDto.roomId,
                    TimeSlotId = scheduleDto.timeslotId,
                };
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
                return Ok(schedule);
            }catch (Exception ex)
            {
                return Conflict();
            }
        }
       public class ScheduleDTO {

            public int movieId { get; set; }
            public int roomId { get; set; }
            public int timeslotId {  get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public string note {  get; set; }
        }
        
    }
}
