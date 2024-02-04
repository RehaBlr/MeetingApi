using Microsoft.EntityFrameworkCore;
using MeetingApi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Infrastructure.Context
{
    public class MeetingDbContext : DbContext
    {
        public MeetingDbContext()
        {
        }
        public MeetingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<UserMeeting> UserMeeting { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().ToTable("User", "APP");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).UseIdentityColumn(1000, 2);

            modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Surname).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Phone).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.ProfilePicture);
            modelBuilder.Entity<User>().Property(x => x.RecordStatus);
            

            #endregion

            #region Meeting
            modelBuilder.Entity<Meeting>().ToTable("Meeting", "APP");
            modelBuilder.Entity<Meeting>().HasKey(x => x.Id);
            modelBuilder.Entity<Meeting>().Property(x => x.MeetingName)
                    .IsRequired()
                    .HasMaxLength(250);
            modelBuilder.Entity<Meeting>().Property(x => x.StartDate).IsRequired();
            modelBuilder.Entity<Meeting>().Property(x => x.EndDate).IsRequired();
            modelBuilder.Entity<Meeting>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<Meeting>().Property(x => x.Document);
            modelBuilder.Entity<Meeting>().Property(x => x.RecordStatus);
            
            #endregion

            //#region UserMeeting
            //modelBuilder.Entity<UserMeeting>().ToTable("UserMeeting", "APP");
            //modelBuilder.Entity<UserMeeting>().HasKey(x => x.Id);
            //modelBuilder.Entity<UserMeeting>().Property(x => x.UserId)
            //        .IsRequired();
                    
            //modelBuilder.Entity<UserMeeting>().Property(x => x.MeetingId).IsRequired();
            

            //#endregion
        }
    }
}
