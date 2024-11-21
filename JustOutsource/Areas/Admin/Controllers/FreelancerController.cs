using JustOutsource.DataAccess.Data;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using JustOutsource.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace JustOutsource.Areas.Admin.Controllers
{
    //   [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class FreelancerController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public FreelancerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Freelancer> freelancers = _unitOfWork.Freelancer.GetAll().ToList();
            
            return View(freelancers);
        }
        public IActionResult Upsert(int? id)
        {
            FreelancerVM freelancerVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                }),
                Freelancer = new Freelancer()
            };
            if(id == 0 || id == null)
            {
                //create
                return View(freelancerVM);
            }
            else
            {
                //update
                freelancerVM.Freelancer = _unitOfWork.Freelancer.Get(u=>u.Id == id);
                return View(freelancerVM);

            }
        }
        [HttpPost]
        public IActionResult Upsert(FreelancerVM freelancerVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Freelancer.Add(freelancerVM.Freelancer);
                _unitOfWork.Save();
                TempData["success"] = "Freelancer created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                freelancerVM.CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                });
                return View(freelancerVM);

            }
        }
        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Freelancer? freelancerFromDb = _unitOfWork.Freelancer.Get(u => u.Id == id);


            if (freelancerFromDb == null)
            {
                return NotFound();
            }
            return View(freelancerFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Freelancer? obj = _unitOfWork.Freelancer.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Freelancer.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Freelancer deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
