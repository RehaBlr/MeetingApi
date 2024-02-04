using MeetingApi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Business.MeetingRepo
{
    public interface IMeetingService : IBaseService
    {
        //Task<Meeting> GetMeetingByIdAsync(int id);
        //List<RestApiDemo.Domain.Entity.Meeting> GetAllMeetings();
        //Meeting AddMeeting(Meeting meeting);
        //Task UpdateMeetingAsync(Meeting meeting);
        //Task DeleteMeetingAsync(int id);

        List<Meeting> GetAllMeeting();
        Meeting GetMeetingById(int id);
        Meeting CreateMeeting(Meeting meeting);
        Meeting UpdateMeeting(Meeting meeting);
        void DeleteMeeting(int id);


    }
}
