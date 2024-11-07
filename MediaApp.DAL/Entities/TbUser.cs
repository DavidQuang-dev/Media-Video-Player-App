using System;
using System.Collections.Generic;

namespace MediaApp.DAL.Entities;

public partial class TbUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }
}
