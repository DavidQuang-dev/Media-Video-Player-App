﻿using MediaApp.DAL;
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

        public TbUser Authenticated(string email, string password)
        {
            return _repo.GetOne(email, password);
        }
    }
}
