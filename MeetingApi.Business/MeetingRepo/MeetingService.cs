using MeetingApi.Domain.Entity;
using MeetingApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Business.MeetingRepo
{
    public class MeetingService : BaseService, IMeetingService
    {
        private readonly MeetingDbContext _context;

        public MeetingService(MeetingDbContext context)
        {
            _context = context;
        }

        public Meeting CreateMeeting(Meeting meeting)
        {
            UserMeeting userMeeting = new UserMeeting();
            userMeeting.UserId = meeting.UserId;
            userMeeting.MeetingId = meeting.Id;

            _context.Meetings.Add(meeting);
            _context.SaveChanges();
            _context.UserMeeting.Add(userMeeting);
            _context.SaveChanges();

            return meeting;
        }

        public void DeleteMeeting(int id)
        {
            var deleteMeeting = GetMeetingById(id);
            _context.Meetings.Remove(deleteMeeting);
            _context.SaveChanges();

        }

        public List<Meeting> GetAllMeeting()
        {
            return _context.Meetings.ToList();
        }

        public Meeting GetMeetingById(int id)
        {

            return _context.Meetings.FirstOrDefault(x => x.Id == id);

        }

        public Meeting UpdateMeeting(Meeting meeting)
        {
            _context.Entry(meeting).State = EntityState.Modified;
              _context.SaveChanges();
            return meeting;
        }

        


        //{
        //    movie.IsActive = true;
        //    _context.Movies.Add(movie);
        //    await _context.SaveChangesAsync();
        //    return movie;
        //}
        //    public  Meeting AddMeeting(Meeting meeting)
        //    {
        //        meeting.RecordStatus = true;
        //        _context.Meetings.Add(meeting);
        //         _context.SaveChanges();

        //        return meeting;

        //        throw new NotImplementedException();
        //    }

        //    public Task DeleteMeetingAsync(int id)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public   List<RestApiDemo.Domain.Entity.Meeting> GetAllMeetings()
        //    {

        //        return  _context.Meetings.ToList();

        //    }

        //    public Task<Meeting> GetMeetingByIdAsync(int id)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public Task UpdateMeetingAsync(Meeting meeting)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
