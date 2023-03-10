using AppDbContext.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class HomeController : BaseController
    {
        public static string msg = "";
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            CartViewModel cvm = new CartViewModel();
            cvm.products = _uow.productRepo.getAll();
            if (HttpContext.Session.GetInt32("CartId") != null)
                cvm.cartItems = _uow.cartItemRepo.ViewCart((int)HttpContext.Session.GetInt32("CartId"));
            return View(cvm);
        }

        [Authorize]
        public IActionResult MyAccount(string message)
        {
            ViewBag.msg = msg;
            ViewBag.message = message;
            msg = "";
            var u = _uow.userRepo.getByUserName(User.Identity.Name);
            if (u == null)
            {
                _signInManager.SignOutAsync();
                return RedirectToAction("Index");
            }
            int id = u.Id;
            var user = _uow.userRepo.getById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, MyAccountViewModel>());
            var mapper = new Mapper(config);
            MyAccountViewModel avm = mapper.Map<MyAccountViewModel>(user);
            ViewBag.Email = user.Email;
            ViewBag.CreatedAt = user.CreatedAt;
            return View(avm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditMyAccount()
        {
            var u = _uow.userRepo.getByUserName(User.Identity.Name);
            int id = u.Id;
            var user = _uow.userRepo.getById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, MyAccountViewModel>());
            var mapper = new Mapper(config);
            MyAccountViewModel avm = mapper.Map<MyAccountViewModel>(user);
            return View(avm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditMyAccount(MyAccountViewModel avm)
        {
            if (ModelState.IsValid)
            {
                var user = _uow.userRepo.getByUserName(User.Identity.Name);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MyAccountViewModel, User>());
                var mapper = new Mapper(config);
                mapper.Map(avm, user);
                user.UserName = avm.FirstName + "_" + avm.LastName;
                user.NormalizedUserName = user.UserName.ToUpper();
                int check = _uow.userRepo.checkUserNameUnique(user.UserName);
                if (check != 0 && check != user.Id)
                {
                    ModelState.AddModelError("", "There is another registered user with this user name");
                    return View(avm);
                }
                check = _uow.userRepo.checkPhoneUnique(user.Phone);
                if (check != 0 && check != user.Id)
                {
                    ModelState.AddModelError("", "There is another registered user with this phone number");
                    return View(avm);
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
                _uow.userRepo.Edit(user);
                _uow.SaveChanges();
            }
            else
            {
                ViewBag.Id = Request.Form["id"];
                return View(avm);
            }
            msg = "edited";
            return RedirectToAction("MyAccount");
        }

        [HttpGet]
        public IActionResult TrackMyOrder()
        {
            TrackViewModel tvm = new TrackViewModel();
            return View(tvm);
        }

        [HttpPost]
        public async Task<IActionResult> TrackMyOrder(TrackViewModel tvm)
        {
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44316/api/Track/" + tvm.Number))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tvm = JsonConvert.DeserializeObject<TrackViewModel>(apiResponse);
                }
            }
            return await Task.Run(() => View("TrackOrder", tvm));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
