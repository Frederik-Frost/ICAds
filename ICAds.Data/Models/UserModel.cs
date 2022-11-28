using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ICAds.Data.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public bool Active { get; set; }
        public bool IsAdmin { get; set; }
        public string OrganizationId { get; set; }

        [JsonIgnore]
        public virtual OrganizationModel Organization { get; set; }
    }
}

