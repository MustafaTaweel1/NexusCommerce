using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Utility;

namespace Store_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;

        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                Category category =new Category();
                return View(category);
            }
            else
            {
                Category? categoryFromDb = _db.Category.Get(u => u.Id == id);
                return View(categoryFromDb);
            }

         
      
        }
        [HttpPost]
        public IActionResult Upsert(Category obj)
        {
     
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _db.Category.Add(obj);
                   
                    TempData["success"] = "Category created successfully";
                   
                }
                else
                {
                    _db.Category.Update(obj);
                    TempData["success"] = "Category updated successfully";
                }
                _db.Save();
                return RedirectToAction("Index");

            }
            return View();

        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Category.Delete(obj);
            _db.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
