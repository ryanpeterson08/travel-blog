﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlogAgain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlogAgain.Controllers
{
    public class PersonsController : Controller
    {
        private TravelBlogAgainContext db = new TravelBlogAgainContext();

        public IActionResult Details(int id)
        {
            var thisPerson = db.Persons.FirstOrDefault(persons => persons.PersonId == id);
            var experiences = db.Experience_Person
                .Include(exp_per => exp_per.Experience)
                .Where(e_p => e_p.PersonId == id)
                .ToList();
            ViewBag.Experiences = experiences;
            return View(thisPerson);
        }

        public IActionResult Create()
        {
            ViewBag.ExpId = new SelectList(db.Experiences, "ExpId", "Activity");
            return View();
        }

        public IActionResult Create(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
            return RedirectToAction("Index", "Locations");
        }
    }
}