using BitSchool.DTOs;
using BitSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitSchool.Controllers
{   [Authorize]
    public class AttendancesController : ApiController
    {
        
        private ApplicationDbContext _dbContet;
        public AttendancesController()
        {
            _dbContet = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContet.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
                return BadRequest("The Attendance already exists!");
            var attendace = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
        };
            _dbContet.Attendances.Add(attendace);
            _dbContet.SaveChanges();
            return Ok();
        }
    }
}
