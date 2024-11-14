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
        public bool EmailExists(string email) { 
            _context = new(); 
            return _context.TbUsers.Any(x => x.Email.ToLower() == email.Trim().ToLower()); }
        public TbUser GetOne(string email, string password)
        {
            _context = new();
            return _context.TbUsers.FirstOrDefault(x => x.Email.ToLower() == email.Trim().ToLower() && x.Password == password);
        }
        public void Delete(TbUser user)
        {
            _context = new();
            _context.TbUsers.Remove(user);
            _context.SaveChanges();
        }
        public void Update(TbUser user)
        {
            _context = new();
            _context.TbUsers.Update(user);
            _context.SaveChanges();
        }
        public void Create(TbUser user)
        {
            _context = new();
            _context.TbUsers.Add(user);
            _context.SaveChanges();
        }
    }
}
