using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BitSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace BitSchool.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Course> UpcommingCoures { get; set; }
        public bool ShowAction { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]
        public byte Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public DateTime GetDateTime() {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
        public string Heading { get; set; }
        public string Action
        {
            get { return (Id != 0) ? "Update" : "Create"; }
        }
        
    }
}