using MediaApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApp.DAL.Repositories
{
    public class UserRepository
    {
        private VideoMediaPlayerContext _context;
        public TbUser GetOne(string email, string password)
        {
            _context = new();
            return _context.TbUsers.FirstOrDefault(x => x.Email.ToLower() == email.Trim().ToLower() && x.Password == password);
        }
    }
}
