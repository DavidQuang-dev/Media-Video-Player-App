using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaApp.DAL.Entities;

[Table("tb_user")]
public partial class TbUser
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("user_name")]
    [MaxLength(255)]
    public string? UserName { get; set; }

    [Column("email")]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Column("password")]
    [MaxLength(255)]
    public string? Password { get; set; }

    [Column("role")]
    [MaxLength(50)]
    public string? Role { get; set; }
}
