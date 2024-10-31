using Microsoft.AspNetCore.Mvc;
using Q1.DTO;
using Q1.Models;


namespace Q1.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly PE_PRN_24SumB5Context _context;

        public ScheduleController(PE_PRN_24SumB5Context context)
        {
            _context = context;
        }

        [HttpPost("AddSchedule")]
        //[Route("")]
        public IActionResult AddSched(ScheduleDTO scheduleDTO)
        {
            if (scheduleDTO.EndDate > scheduleDTO.StartDate)
            {
                return Conflict("End date must be less than start date");
            }
            var rs = _context.Schedules.FirstOrDefault(x => x.TimeSlotId == scheduleDTO.TimeSlotId
            && x.RoomId == scheduleDTO.RoomId && x.StartDate == scheduleDTO.StartDate
            && x.EndDate == scheduleDTO.EndDate && x.MovieId != scheduleDTO.MovieId);

            if (rs != null) {
                return StatusCode(406, "The same schedule");
            } else
            {
                Schedule scheduleAdd = new Schedule();
                scheduleAdd.Movie = _context.Movies
                    .FirstOrDefault(x => x.Id == scheduleDTO.MovieId);
                scheduleAdd.Room = _context.Rooms
                    .FirstOrDefault(x => x.Id == scheduleDTO.RoomId);
                scheduleAdd.TimeSlot = _context.TimeSlots
                    .FirstOrDefault(x => x.Id == scheduleDTO.TimeSlotId);
                scheduleAdd.StartDate = scheduleDTO.StartDate;
                scheduleAdd.EndDate = scheduleDTO.EndDate;
                scheduleAdd.Note = scheduleDTO.Note;
                _context.Schedules.Add(scheduleAdd);
                _context.SaveChanges();
                return Ok("Add schedule successful");
            }
        }
    }
}
