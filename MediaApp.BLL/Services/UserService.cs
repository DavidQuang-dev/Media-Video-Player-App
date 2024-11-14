using MediaApp.DAL;
using MediaApp.DAL.Entities;
using MediaApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.BLL.Services
{
    public class UserService
    {
        private UserRepository _repo = new();
        public bool CheckEmailExists(string email)
        {
            return _repo.EmailExists(email);
        }
        public void DeleteUser(TbUser user)
        {
            _repo.Delete(user);
        }
        public void UpdateUser(TbUser user)
        {
            _repo.Update(user);
        }
        public void CreateUser(TbUser user)
        {
            _repo.Create(user);
        }
        public TbUser Authenticated(string email, string password)
        {
            return _repo.GetOne(email, password);
        }
    }
}
