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

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IUnitOfWork db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _db.Product.GetAll(includeProperties:"Category").ToList();

            return View(objProductList);
        }

        //upsert    update and insert  use one fucn to do two task insert and update 
        public IActionResult Upsert(int? id)

        {
            //viewBag use to see name in list 
            //ViewBag.Product_CategoryList = Product_CategoryList;
            ProductVM productVM = new()
            {
                Product_CategoryList = _db.Category.GetAll().Select(U => new SelectListItem
                {
                    Text = U.Name,
                    Value = U.Id.ToString(),


                }),
                product = new Product()
            };

            if (id == null || id == 0)
            {

                //Create
                return View(productVM);
            }
            else
            {
                //Update
                productVM.product = _db.Product.Get(u => u.Id == id);
                return View(productVM);

            }


        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            string wwwRootpath = _environment.WebRootPath;

            if (ModelState.IsValid)
            {
                // add images 
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwRootpath, @"images/product");

                    if(!string.IsNullOrEmpty(obj.product.ImageUrl)) {

                        //delete old image
                        var oldImagePath=Path.Combine(wwwRootpath,obj.product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);    
                        }

                    }
                    using (var fileStream = new FileStream(Path.Combine(ProductPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.product.ImageUrl = @"\images\product\" + filename;
                }
                if (obj.product.Id == 0)
                {
                    obj.product.Release_Date = DateTime.Now;
                   
                    _db.Product.Add(obj.product);
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    //.ToString("MM/dd/yyyy hh:mm tt")
                    obj.product.Release_Date = DateTime.Now;
                    _db.Product.Update(obj.product);
                    TempData["success"] = "Product updated successfully";
                }
                _db.Save();

                return RedirectToAction("Index");

            }
            else
            {

                ProductVM productVM = new()
                {
                    Product_CategoryList = _db.Category.GetAll().Select(U => new SelectListItem
                    {
                        Text = U.Name,
                        Value = U.Id.ToString(),


                    }),
                    product = new Product()
                };
                return View(productVM);
            }
        }

        // use api to sort table 
        #region API CALLS 
        [HttpGet]
        public IActionResult GetAll() {
            
            List<Product> objProductList = _db.Product.GetAll(includeProperties: "Category").ToList();
            
            return Json(new {data=objProductList});
        }
        #endregion

        [HttpDelete]
        public IActionResult Delete(int? id) {
            var productsToBeDelete = _db.Product.Get(u => u.Id == id);
            if (productsToBeDelete==null)
            {
                return Json( new { success = false,message="Error while deleting" });
            }
            var oldImagePath = Path.Combine(_environment.WebRootPath, productsToBeDelete.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _db.Product.Delete(productsToBeDelete);
            _db.Save();
            return Json(new { success = true, message = " delete successfully" });


        }
    }
}
