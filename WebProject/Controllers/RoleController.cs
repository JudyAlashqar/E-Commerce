using AppDbContext.Models;
using AppDbContext.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebProject.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : BaseController
    {
        public static string msg = "";
        public IActionResult Index()
        {
            ViewBag.msg = msg;
            msg = "";
            return View(_uow.roleRepo.getAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            User user = new User();
            RoleViewModel avm = new RoleViewModel();
            return View(avm);
        }


        [HttpPost]
        public IActionResult Add(RoleViewModel avm)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<RoleViewModel, AppDbContext.Models.AspNetRoles>());
                var mapper = new Mapper(config);
                AppDbContext.Models.AspNetRoles a = mapper.Map<AppDbContext.Models.AspNetRoles>(avm);
                if (_uow.roleRepo.checkUnique(a.Name) != 0)
                {
                    ModelState.AddModelError("Name", "Role Name must be unique");
                    return View(avm);
                }
                a.NormalizedName = a.Name;
                _uow.roleRepo.Add(a);
                _uow.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View(avm);
            }
            msg = "added";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            AppDbContext.Models.AspNetRoles role = _uow.roleRepo.getById(id);
            ViewBag.id = id;
            RoleViewModel avm = new RoleViewModel();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AppDbContext.Models.AspNetRoles, RoleViewModel>());
            var mapper = new Mapper(config);
            mapper.Map(role, avm);
            return View(avm);
        }


        [HttpPost]
        public IActionResult Edit(RoleViewModel avm)
        {
            if (ModelState.IsValid)
            {
                AppDbContext.Models.AspNetRoles role= _uow.roleRepo.getById(Convert.ToInt32(Request.Form["id"]));
                var config = new MapperConfiguration(cfg => cfg.CreateMap<RoleViewModel, AppDbContext.Models.AspNetRoles>());
                var mapper = new Mapper(config);
                mapper.Map(avm,role);
                int check = _uow.roleRepo.checkUnique(role.Name);
                if (check != 0 && check != role.Id)
                {
                    ModelState.AddModelError("Name", "Attribute Name must be unique");
                    return View(avm);
                }
                role.NormalizedName = avm.Name;
                _uow.roleRepo.Edit(role);
                _uow.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View(avm);
            }
            msg = "edited";
            return RedirectToAction("Index");
        }

        public IActionResult Delete (int id)
        {
            int pId = _uow.roleRepo.checkDelete(id);
            if(pId == 0)
            {
                _uow.roleRepo.Delete(id);
                _uow.SaveChanges();
                msg = "deleted";
                return RedirectToAction("Index");
            }
            msg = "NoDelete";
            return View("Index", _uow.roleRepo.getAll());

        }

        public IActionResult Details(int id)
        {
            var s = _uow.roleRepo.getById(id);
            ViewBag.id = id;
            return View(s);
        }
        
    }
}
