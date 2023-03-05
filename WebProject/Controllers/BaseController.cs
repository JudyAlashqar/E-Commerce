using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDbContext.UoW;
using Microsoft.AspNetCore.Hosting;
using System.Web.Http.ExceptionHandling;

namespace WebProject.Controllers
{
    public class BaseController : Controller
    {
        protected IUoW _uow;

        public BaseController()
        {
            _uow = new UoW(new AppDbContext.Models.ProjectContext());

        }
    }
}
