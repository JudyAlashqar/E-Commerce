using AppDbContext.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : BaseController
    {
        public TrackController()
        {
        }


        [HttpGet("{id}")]
        public TrackViewModel TrackOrder(int id)
        {
            var order = _uow.orderRepo.getById(id);
            TrackViewModel tvm = new TrackViewModel();
            tvm.Number = id;
            if (order == null)
            {
                tvm.Status = "There is No Requested Order With This Number";
            }
            else
            {
                tvm.Status = order.Status;
            }
            return tvm;
        }
       
    }
}
