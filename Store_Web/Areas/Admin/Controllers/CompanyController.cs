using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Store.DataAccess.Repository;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Models.ViewModels;
using Store.Utility;
using System.Collections.Generic;

namespace Store_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class  CompanyController : Controller
    {
        private readonly IUnitOfWork _db;

        public CompanyController(IUnitOfWork db)
        {
            _db = db;
          
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _db.Company.GetAll().ToList();

            return View(objCompanyList);
        }

        //upsert    update and insert  use one fucn to do two task insert and update 
        public IActionResult Upsert(int? id)

        {
            if (id == null || id == 0)
            {
                Company companys = new Company();
                return View(companys);
            }
            else
            {
                Company? companysFromDb = _db.Company.Get(u => u.Id == id);
                return View(companysFromDb);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Company obj)
        {
  
           
                if (obj.Id == 0)
                {
                   
                    _db.Company.Add(obj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _db.Company.Update(obj);
                    TempData["success"] = "Company updated successfully";
                }
                _db.Save();

                return RedirectToAction("Index");
        }

        // use api to sort table 
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll() {
            
            List<Company> objCompanyList = _db.Company.GetAll().ToList();
            
            return Json(new {data= objCompanyList });
        }
        #endregion

        [HttpDelete]
        public IActionResult Delete(int? id) {
            var compantsToBeDelete = _db.Company.Get(u => u.Id == id);
            if (compantsToBeDelete == null)
            {
                return Json( new { success = false,message="Error while deleting" });
            }

            
            _db.Company.Delete(compantsToBeDelete);
            _db.Save();
            return Json(new { success = true, message = " delete successfully" });


        }
    }
}
