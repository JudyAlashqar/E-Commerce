using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class TrackViewModel
    {
        [Required]
        public int Number { get; set; }
        public string Status { get; set; }
        public TrackViewModel()
        {
            Status = "";
        }

    }
}
