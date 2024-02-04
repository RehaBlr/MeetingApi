using MeetingApi.Business.MeetingRepo;
using MeetingApi.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace MeetingApi.Controllers
{
    [Route("meeting/[controller]/[action]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<Meeting> GetMeetings()
        {
            var meetings = _meetingService.GetAllMeeting();
            return meetings;
        }

        [HttpGet("{id}")]
        public IActionResult GetMeetingById(int id)
        {
            var meeting = _meetingService.GetMeetingById(id);
            if (meeting == null)
            {
                return NotFound(); 
            }
            return Ok(meeting); 
        }

        [HttpPost]
        public IActionResult CreateMeeting2(Meeting meeting)
        {
            _meetingService.CreateMeeting(meeting);
            return CreatedAtAction(nameof(GetMeetingById), new { id = meeting.Id }, meeting);
        }

        [HttpPost]
        public Meeting CreateMeeting(Meeting meeting)
        {
            _meetingService.CreateMeeting(meeting);

            return meeting;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            var existingMeeting = _meetingService.GetMeetingById(id);
            if (existingMeeting == null)
            {
                return NotFound(); 
            }
            _meetingService.DeleteMeeting(id);
            return NoContent(); 
        }

       
        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return BadRequest();
            }

             _meetingService.UpdateMeeting(meeting);
            return NoContent();
        }

    }
}
