using System;
using System.Collections.Generic;
using System.Text;
using AppDbContext.Models;

namespace AppDbContext.IRepos
{
    public interface IUserRepo : IBaseRepo<AppDbContext.Models.User>
    {
        public int checkDelete(int id);

        public int checkEmailUnique(string Email);
        public int checkUserNameUnique(string UserName);
        public int checkPhoneUnique(string PhoneNumber);
        public User getByUserName(string userName);
    }
}
