﻿using JustOutsource.Data;
using JustOutsource.Models;
using JustOutsource.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JustOutsource.Controllers
{
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _db;
        public JobController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var jobs = _db.Jobs.Include(f => f.Category).ToList();
            return View(jobs);
        }
        public IActionResult Create()
        {
            JobVM jobVM = new()
            {
                CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                }),
                Job = new Job()
            };
            return View(jobVM);
        }
        [HttpPost]
        public IActionResult Create(JobVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Jobs.Add(obj.Job);
                _db.SaveChanges();
                TempData["success"] = "Job added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                obj.CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var jobFromDb = _db.Jobs.Find(id);
            if (jobFromDb == null)
            {
                return NotFound();
            }
            var jobVM = new JobVM
            {
                Job = jobFromDb,
                CategoryList = _db.Categories.Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.Id.ToString()
                })
            };
            return View(jobVM);
        }
        [HttpPost]
        public IActionResult Edit(JobVM obj)
        {

            if (ModelState.IsValid)
            {
                _db.Jobs.Update(obj.Job);
                _db.SaveChanges();
                TempData["success"] = "Job updated successfully";
                return RedirectToAction("Index");
            }
            obj.CategoryList = _db.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()
            });
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var jobFromDb = _db.Jobs.Find(id);
            if (jobFromDb == null)
            {
                return NotFound();
            }
            var jobVM = new JobVM
            {
                Job = jobFromDb,
                CategoryList = _db.Categories.Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.Id.ToString()
                })
            };
            return View(jobVM);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Job? obj = _db.Jobs.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Jobs.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Job deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
