﻿using BitSchool.Models;
using BitSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitSchool.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _dbContex;
        public CourseController() {
            _dbContex = new ApplicationDbContext();
        }
        // GET: Course
        [Authorize]

        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContex.Categories.ToList(),
                Heading= "Add Course"
                
            };

            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel) {
            if (!ModelState.IsValid) {
                viewModel.Categories = _dbContex.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course {
                LecturerID = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryID = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContex.Courses.Add(course);
            _dbContex.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContex.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Categories)
                .ToList();
            var viewModel = new CourseViewModel
            {
                UpcommingCoures = course,
                ShowAction=User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContex.Attendances
                .Where(f => f.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Categories)
                .ToList();
            var viewModel = new CourseViewModel
            {
                UpcommingCoures = course,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult Mine() {
            var userId = User.Identity.GetUserId();
            var courses = _dbContex.Courses
                .Where(c => c.LecturerID == userId && c.DateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(l => l.Categories)
                .ToList();
            return View(courses);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContex.Courses.Single(c =>c.Id==id && c.LecturerID == userId);
            var viewModel = new CourseViewModel
            {
                Categories = _dbContex.Categories.ToList(),
                Date = course.DateTime.ToString("M/d/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.CategoryID,
                Place = course.Place,
                Heading="Edit Course",
                Id=course.Id

            };
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _dbContex.Categories.ToList();
                return View("Create", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContex.Courses.Single(c => c.Id == viewModel.Id && c.LecturerID == userId);
            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.CategoryID = viewModel.Category;
            _dbContex.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}