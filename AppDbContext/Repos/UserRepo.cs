using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AppDbContext.Reops
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo(ProjectContext db) : base(db)
        {
        }
        public int checkDelete(int id)
        {
            return _db.CartItem.Where(ca => ca.CartId == id).Select(pav => pav.Id).Count()
                + _db.Order.Where(p => p.CustomerId == id).Select(p => p.Id).Count();
        }
        public int checkEmailUnique(string Email)
        {
            return _db.User.Where(ca => ca.Email == Email).Select(pav => pav.Id).FirstOrDefault();
        }

        public int checkUserNameUnique(string UserName)
        {
            return _db.User.Where(ca => ca.UserName == UserName).Select(pav => pav.Id).FirstOrDefault();
        }
        public int checkPhoneUnique(string phone)
        {
            return _db.User.Where(ca => ca.Phone == phone).Select(pav => pav.Id).FirstOrDefault();
        }
        public User getByUserName(string userName)
        {
            return _db.User.Where(ca => ca.UserName == userName).FirstOrDefault();
        }
    }
}
