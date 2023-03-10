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
using PagedList;
using System.IO;

namespace WebProject.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AspNetRoles> _roleManager; 
        public UserController(UserManager<User> userManger, RoleManager<AspNetRoles> roleManager) : base()
        {
            _userManager = userManger;
            _roleManager = roleManager;
        }
        public static string msg = "";
        public async Task<IActionResult> Index(string sortOrder, string CurrentNameFilter, string CurrentAddressFilter
            ,string NameSearchString, string AddressSearchString, int? page)
        {
            ViewBag.msg = msg;
            msg = "";
            ViewBag.CurrentSort = sortOrder;
            var users = _uow.userRepo.getAll();
            ViewBag.SortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (NameSearchString != null || AddressSearchString != null)
            {
                page = 1;
            }
            else
            {
                NameSearchString = CurrentNameFilter;
                AddressSearchString = CurrentAddressFilter;
            }
            ViewBag.CurrentNameFilter = NameSearchString;
            ViewBag.CurrentAddressFilter = AddressSearchString;

            if (!String.IsNullOrEmpty(NameSearchString))
            {
                users = users.Where(o => o.FirstName.ToLower().Contains(NameSearchString.ToLower())
                || o.LastName.ToLower().Contains(NameSearchString.ToLower()));
            }
            if (!String.IsNullOrEmpty(AddressSearchString))
            {
                users = users.Where(o => o.Address.ToLower().Contains(AddressSearchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending((s => s.FirstName + " " + s.LastName));
                    break;
                default:
                    users = users.OrderBy((s => s.FirstName + " " + s.LastName));
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return await Task.Run(() => View(users.ToPagedList(pageNumber, pageSize)));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            User user = new User();
            UserViewModel uvm = new UserViewModel();
            uvm.roles = _roleManager.Roles.ToList();
            var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
            uvm.roles.Remove(role);
            return await Task.Run(() => View(uvm));
        }


        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel uvm)
        {
            var roles = _roleManager.Roles.ToList();
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, AppDbContext.Models.User>());
                var mapper = new Mapper(config);
                AppDbContext.Models.User a = mapper.Map<AppDbContext.Models.User>(uvm);
                a.UserName = a.FirstName + "_" + a.LastName;
                if (_uow.userRepo.checkEmailUnique(a.Email) != 0)
                {
                    ModelState.AddModelError("Email", "There is another registered user with this Email");
                    uvm.roles = roles;
                    var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                    uvm.roles.Remove(role);
                    return await Task.Run(() => View(uvm));
                }
                if (_uow.userRepo.checkUserNameUnique(a.UserName) != 0)
                {
                    ModelState.AddModelError("", "There is another registered user with this user name");
                    uvm.roles = roles;
                    var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                    uvm.roles.Remove(role);
                    return await Task.Run(() => View(uvm));
                }
                if (_uow.userRepo.checkPhoneUnique(a.Phone) != 0)
                {
                    ModelState.AddModelError("", "There is another registered user with this phone number");
                    uvm.roles = roles;
                    var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                    uvm.roles.Remove(role);
                    return await Task.Run(() => View(uvm));
                }
                IFormFile file = Request.Form.Files["Image"];
                if (file != null)
                {
                    a.Image = Path.GetFileName(file.FileName);
                    if (file.Length > 0)
                    {
                        string file_name = Path.GetFileName(file.FileName);
                        string path = "C:\\Users\\ASUS\\source\\WebProject\\WebProject\\wwwroot\\images\\" + file_name;
                        using (Stream fileStream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                }
                await _userManager.CreateAsync(a, uvm.Password);
                foreach (var item in uvm.rolesIds)
                {
                    await _userManager.AddToRoleAsync(a, roles.Find(r => r.Id == item).NormalizedName);
                }
                await _userManager.AddToRoleAsync(a, "Normal User");
               _uow.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                uvm.roles = roles;
                var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                uvm.roles.Remove(role);
                return await Task.Run(() => View(uvm));
            }
            msg = "added";
            return await Task.Run(() => RedirectToAction("Index"));
        }

        public async Task<IActionResult> Edit(int id)
        {
            AppDbContext.Models.User user = _uow.userRepo.getById(id);
            if (user == null)
            {
                return await Task.Run(() => RedirectToAction("Index"));
            }
            ViewBag.id = id;
            UserViewModelEdit uvm = new UserViewModelEdit();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AppDbContext.Models.User, UserViewModelEdit>());
            var mapper = new Mapper(config);
            mapper.Map(user, uvm);
            uvm.roles = _roleManager.Roles.ToList();
            var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
            uvm.roles.Remove(role);
            var roles = await _userManager.GetRolesAsync(user);
            uvm.rolesNames = roles.ToList();
            return await Task.Run(() => View(uvm));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModelEdit uvm)
        {
            var roles = _roleManager.Roles.ToList();
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(Request.Form["id"]);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModelEdit, AppDbContext.Models.User>());
                var mapper = new Mapper(config);
                mapper.Map(uvm,user);
                user.UserName = user.FirstName + "_" + user.LastName;
                int check = _uow.userRepo.checkEmailUnique(user.Email);
                if (check != 0 && check != user.Id)
                {
                    ModelState.AddModelError("Email", "There is another registered user with this Email");
                    uvm.roles = roles;
                    var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                    uvm.roles.Remove(role);
                    ViewBag.Id = Request.Form["id"];
                    return await Task.Run(() => View(uvm));
                }
                check = _uow.userRepo.checkUserNameUnique(user.UserName);
                if (check != 0 && check != user.Id)
                {
                    ModelState.AddModelError("", "There is another registered user with this user name");
                    uvm.roles = roles;
                    var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                    uvm.roles.Remove(role);
                    ViewBag.Id = Request.Form["id"];
                    return await Task.Run(() => View(uvm));
                }
                check = _uow.userRepo.checkPhoneUnique(user.Phone);
                if (check != 0 && check != user.Id)
                {
                    ModelState.AddModelError("", "There is another registered user with this phone number");
                    uvm.roles = roles;
                    var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                    uvm.roles.Remove(role);
                    ViewBag.Id = Request.Form["id"];
                    return await Task.Run(() => View(uvm));
                }
                IFormFile file = Request.Form.Files["Image"];
                if (file != null)
                {
                    user.Image = Path.GetFileName(file.FileName);
                    if (file.Length > 0)
                    {
                        string file_name = Path.GetFileName(file.FileName);
                        string path = "C:\\Users\\ASUS\\source\\WebProject\\WebProject\\wwwroot\\images\\" + file_name;
                        using (Stream fileStream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    user.Image = "";
                }
                foreach (var item in roles)
                {
                    if (await _userManager.IsInRoleAsync(user, item.NormalizedName))
                    {
                        await _userManager.RemoveFromRoleAsync(user, item.NormalizedName);
                    }
                }
                foreach (var item in uvm.rolesIds)
                {
                    await _userManager.AddToRoleAsync(user, roles.Find(r => r.Id == item).NormalizedName);
                }
                await _userManager.AddToRoleAsync(user, "Normal User");
                await _userManager.UpdateAsync(user);
                _uow.SaveChanges();
                
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                uvm.roles = _roleManager.Roles.ToList();
                var role = uvm.roles.Where(r => r.NormalizedName == "Normal User").FirstOrDefault();
                uvm.roles.Remove(role);
                ViewBag.Id = Request.Form["id"];
                return await Task.Run(() => View(uvm));
            }
            msg = "edited";
            return await Task.Run(() => RedirectToAction("Index"));
        }

        public async Task<IActionResult> Delete (int id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return await Task.Run(() => RedirectToAction("Index"));
            }
            int pId = _uow.userRepo.checkDelete(id);
            if(pId == 0)
            {
                var roles = _roleManager.Roles.ToList();
                foreach (var item in roles)
                {
                    if (await _userManager.IsInRoleAsync(user, item.NormalizedName))
                    {
                        await _userManager.RemoveFromRoleAsync(user, item.NormalizedName);
                    }
                }
                _uow.userRepo.Delete(id);
                _uow.SaveChanges();
                msg = "deleted";
                return RedirectToAction("Index");
            }
            msg = "NoDelete";
            return await Task.Run(() => RedirectToAction("Index"));
        }

        public async Task<IActionResult> Details(int id)
        {
            UserViewModel uvm = new UserViewModel();
            var user = _uow.userRepo.getById(id);
            if (user == null)
            {
                return await Task.Run(() => RedirectToAction("Index"));
            }
            var roles = await _userManager.GetRolesAsync(user);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AppDbContext.Models.User, UserViewModel>());
            var mapper = new Mapper(config);
            mapper.Map(user, uvm);
            uvm.rolesNames = roles.ToList();
            ViewBag.id = id;
            ViewBag.Email = user.Email;
            ViewBag.CreatedAt = user.CreatedAt;
            return await Task.Run(() => View(uvm));
        }
        
    }
}
