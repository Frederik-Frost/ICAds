using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICAds.Data.Models
{
    public class UserModel
    {
        //[Key]
        //[Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        //[Column(TypeName = "varchar(80)")]
        public string Firstname { get; set; }
        //[Column(TypeName = "varchar(80)")]
        public string Lastname { get; set; }
        //[Column(TypeName = "varchar(120)")]
        public string Email { get; set; }
        //[Column(TypeName = "varchar(120)")]
        public byte[] PasswordHash { get; set; }
        //[Column(TypeName = "varchar(120)")]
        public byte[] PasswordSalt { get; set; }
        public bool Active { get; set; }
        public bool IsAdmin { get; set; }
    }
}

